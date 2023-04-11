using Memory;
using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Barnyard_Trainer
{
    public partial class BarnyardTrainer : Form
    {
        #region Initialisation
        Mem mem = new Mem();
        RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\THQ\Barnyard", true);
        Stopwatch stopWatch = new Stopwatch();
        float xPos, zPos, lastXPos, lastZPos;
        long lastTime;

        public BarnyardTrainer()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            outGameTimer.Start();
            stopWatch.Start();

            if (registry.GetValue("ControllerEnabled").ToString() == "1")
            {
                controllerCheckBox.Checked = true;
            }
            if (registry.GetValue("RealForcedWindowed").ToString() == "1")
            {
                windowedCheckBox.Checked = true;
            }
        }

        private void OutGameTimer_Tick(object sender, EventArgs e)
        {
            int PID = mem.GetProcIdFromName("Barnyard.exe");
            if (PID > 0)
            {
                statusText.Text = "Connected";
                outGameTimer.Stop();
                mem.OpenProcess(PID);
                inGameTimer.Start();
            }
        }
        #endregion

        #region Custom Functions
        // Converts an integer contained in a string into 4 hexadecimal bytes (in a string)
        string IntStringTo4Bytes(string intString)
        {
            return string.Join(" ", BitConverter.GetBytes(int.Parse(intString)).Select(b => "0x" + b.ToString("X2")));
        }

        void WriteFloat(string address, float value, string errorMessage = "")
        {
            if (!mem.WriteMemory(address, "float", value.ToString()))
            {
                if (errorMessage != "")
                {
                    MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void WriteBytes(string address, string value, string errorMessage = "")
        {
            if (!mem.WriteMemory(address, "bytes", value))
            {
                if (errorMessage != "")
                {
                    MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void Nop(string address, int bytes, string errorMessage = "")
        {
            string value = "";
            for (int i = 0; i < bytes; i++)
            {
                value += "90 ";
            }
            if (!mem.WriteMemory(address, "bytes", value.TrimEnd()))
            {
                if (errorMessage != "")
                {
                    MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool OnFoot()
        {
            return mem.ReadByte(Addresses.isOnFoot) == 1;
        }
        #endregion

        #region Main Code
        void DisableCameraZoom(bool disabled)
        {
            if (disabled)
            {
                Nop(Addresses.cameraZoom, 6, "Error disabling camera zoom");
                Nop(Addresses.cameraZoomIn, 2, "Error disabling camera zoom in");
                Nop(Addresses.cameraZoomOut, 6, "Error disabling camera zoom out");
            }
            else
            {
                WriteBytes(Addresses.cameraZoom, "89 96 88 00 00 00", "Error enabling camera zoom");
                WriteBytes(Addresses.cameraZoomIn, "89 01", "Error enabling camera zoom in");
                WriteBytes(Addresses.cameraZoomOut, "89 8E 88 00 00 00", "Error enabling camera zoom out");
            }
        }

        void InGameTimer_Tick(object sender, EventArgs e)
        {
            // If barnyard closes
            if (mem.GetProcIdFromName("Barnyard.exe") == 0)
            {
                statusText.Text = "Disconnected";
                flightTimer.Stop();
                inGameTimer.Stop();
                outGameTimer.Start();
            }

            // Speedometer
            if (OnFoot())
            {
                xPos = mem.ReadFloat(Addresses.xPos);
                zPos = mem.ReadFloat(Addresses.zPos);
            }
            else
            {
                xPos = mem.ReadFloat(Addresses.bikeXPos);
                zPos = mem.ReadFloat(Addresses.bikeZPos);
            }
            long time = stopWatch.ElapsedMilliseconds;
            double speed = Math.Round(Math.Sqrt(Math.Pow(xPos - lastXPos, 2) + Math.Pow(zPos - lastZPos, 2)) / (time - lastTime) * 1000);
            speedLabel.Text = "Horizontal Speed: " + speed + " m/s";
            lastTime = time;
            lastXPos = xPos;
            lastZPos = zPos;

            if (staminaCheckBox.Checked)
            {
                if (OnFoot())
                {
                    WriteFloat(Addresses.stamina, 5f);
                }
                else
                {
                    WriteFloat(Addresses.bikeStamina, 1f);
                }
            }
            if (milkCheckBox.Checked)
            {
                WriteFloat(Addresses.milk, 5f);
            }
            if (firstPersonCheckBox.Checked)
            {
                if (OnFoot())
                {
                    WriteFloat(Addresses.cameraTargettedDistance, 0.01f);
                    WriteFloat(Addresses.opacity, 0f);
                }
            }
            if (OnFoot())
            {
                if (clampCheckBox.Checked)
                {
                    Nop(Addresses.minPitch, 2);
                    Nop(Addresses.maxPitch, 2);
                }
                else
                {
                    WriteBytes(Addresses.minPitch, "89 01");
                    WriteBytes(Addresses.maxPitch, "89 11");
                }
                if (waterCheckBox.Checked)
                {
                    Nop(Addresses.waterCollision, 2);
                }
                else
                {
                    WriteBytes(Addresses.waterCollision, "89 0E");
                }
            }
            else // (restore because this causes issues while on a bike)
            {
                WriteBytes(Addresses.minPitch, "89 01");
                WriteBytes(Addresses.maxPitch, "89 11");
                WriteBytes(Addresses.waterCollision, "89 0E");
            }
        }

        void FlightTimer_Tick(object sender, EventArgs e)
        {
            if (mem.ReadByte(Addresses.space) == 128)
            {
                WriteFloat(Addresses.yPos, mem.ReadFloat(Addresses.yPos) - 0.2f);
            }
            if (mem.ReadByte(Addresses.leftControl) == 128)
            {
                WriteFloat(Addresses.yPos, mem.ReadFloat(Addresses.yPos) + 0.2f);
            }
        }

        void FlightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flightCheckBox.Checked)
            {
                Nop(Addresses.gravity, 3, "Error disabling gravity");
                gravityCheckBox.Enabled = false;
                flightTimer.Start();
            }
            else
            {
                if(!gravityCheckBox.Checked)
                {
                    WriteBytes(Addresses.gravity, "D9 59 1C", "Error enabling gravity");
                }
                gravityCheckBox.Enabled = true;
                flightTimer.Stop();
            }
        }

        void NoclipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noclipCheckBox.Checked)
            {
                WriteFloat(Addresses.radius, 0.01f, "Error changing player radius");
                Nop(Addresses.noClip, 2, "Error enabling no-clip");
            }
            else
            {
                WriteFloat(Addresses.radius, 0.55f, "Error changing player radius");
                WriteBytes(Addresses.noClip, "89 11", "Error disabling no-clip");
            }
        }

        void GravityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gravityCheckBox.Checked)
            {
                Nop(Addresses.gravity, 3, "Error disabling gravity");
            }
            else
            {
                WriteBytes(Addresses.gravity, "D9 59 1C", "Error enabling gravity");
            }
        }

        void LadderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ladderCheckBox.Checked)
            {
                WriteFloat(Addresses.minClimbSpeed, 15f, "Error enabling fast ladder");
                WriteFloat(Addresses.maxClimbSpeed, 15f, "Error enabling fast ladder");
            }
            else
            {
                WriteFloat(Addresses.minClimbSpeed, 1f, "Error enabling fast ladder");
                WriteFloat(Addresses.maxClimbSpeed, 1.3f, "Error enabling fast ladder");
            }
        }

        void SquirtDelayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtDelayCheckBox.Checked)
            {
                WriteFloat(Addresses.squirtDelay, 0f, "Error disabling squirt delay");
            }
            else
            {
                WriteFloat(Addresses.squirtDelay, 0.5f, "Error restoring squirt delay");
            }
        }

        void SquirtGlassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtGlassesCheckBox.Checked)
            {
                WriteBytes(Addresses.cantSquirt, "0x00", "Error enabling squirt without glasses");
            }
            else
            {
                WriteBytes(Addresses.cantSquirt, "0x01", "Error disabling squirt without glasses");
            }
        }

        void ZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisableCameraZoom(zoomCheckBox.Checked);
            WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error zooming out camera");
        }

        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstPersonCheckBox.Checked)
            {
                zoomCheckBox.Enabled = false;

                DisableCameraZoom(true);
                WriteFloat(Addresses.cameraTargettedDistance, 0.01f, "Error enabling first person");
                Nop(Addresses.opacityChange1, 2, "Error changing player opacity");
                Nop(Addresses.opacityChange2, 2, "Error changing player opacity");

                //Nop(Addresses.firstPeronBike, 2);
            }
            else
            {
                zoomCheckBox.Enabled = true;

                if (!zoomCheckBox.Checked)
                {
                    DisableCameraZoom(false);
                }
                WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error disabling first person");
                WriteBytes(Addresses.opacityChange1, "D9 11", "Error changing player opacity");
                WriteBytes(Addresses.opacityChange2, "89 01", "Error changing player opacity");

                //WriteBytes(Addresses.firstPeronBike, "D8 01");
            }
        }

        void ControllerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (controllerCheckBox.Checked)
            {
                registry.SetValue("ControllerEnabled", 1);
            }
            else
            {
                registry.SetValue("ControllerEnabled", 0);
            }
        }

        void WindowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (windowedCheckBox.Checked)
            {
                registry.SetValue("RealForcedWindowed", 1);
            }
            else
            {
                registry.SetValue("RealForcedWindowed", 0);
            }
        }

        void MovementApplyButton_Click(object sender, EventArgs e)
        {
            WriteFloat(Addresses.walkSpeed, float.Parse(walkTextBox.Text), "Error applying walk speed");
            WriteFloat(Addresses.runSpeed, float.Parse(runTextBox.Text), "Error applying run speed");
            WriteFloat(Addresses.sprintSpeed, float.Parse(sprintTextBox.Text), "Error applying sprint speed");
            WriteFloat(Addresses.acceleration, float.Parse(accTextBox.Text), "Error applying acceleration");
            WriteFloat(Addresses.deceleration, float.Parse(decTextBox.Text), "Error applying deceleration");
            WriteFloat(Addresses.jumpForce, float.Parse(jumpTextBox.Text), "Error applying jump height");
        }

        void PositionRefreshButton_Click(object sender, EventArgs e)
        {
            if (OnFoot())
            {
                xPosTextBox.Text = mem.ReadFloat(Addresses.xPos).ToString();
                yPosTextBox.Text = (-1f * mem.ReadFloat(Addresses.yPos)).ToString();
                zPosTextBox.Text = mem.ReadFloat(Addresses.zPos).ToString();
            }
            else
            {
                xPosTextBox.Text = mem.ReadFloat(Addresses.bikeXPos).ToString();
                yPosTextBox.Text = (-1f * mem.ReadFloat(Addresses.bikeYPos)).ToString();
                zPosTextBox.Text = mem.ReadFloat(Addresses.bikeZPos).ToString();
            }
        }

        void PositionApplyButton_Click(object sender, EventArgs e)
        {
            if (OnFoot())
            {
                WriteFloat(Addresses.xPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                WriteFloat(Addresses.yPos, -1f * float.Parse(yPosTextBox.Text), "Error applying Y position");
                WriteFloat(Addresses.zPos, float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
            else
            {
                WriteFloat(Addresses.bikeXPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                WriteFloat(Addresses.bikeYPos, -1f * float.Parse(yPosTextBox.Text), "Error applying Y position");
                WriteFloat(Addresses.bikeZPos, float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
        }

        void MoneyApplyButton_Click(object sender, EventArgs e)
        {
            WriteBytes(Addresses.money, IntStringTo4Bytes(moneyTextBox.Text), "Error applying money");
            WriteBytes(Addresses.moneyHud, IntStringTo4Bytes(moneyTextBox.Text), "Error applying money HUD");
        }

        void ItemsApplyButton_Click(object sender, EventArgs e)
        {
            WriteBytes(Addresses.itemCount, IntStringTo4Bytes(itemsTextBox.Text), "Error applying item count");
        }

        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            WriteFloat(Addresses.fov, (float)fovTrackBar.Value / 100, "Error changing FOV");
        }

        void FlightInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("'Space' to fly up and 'Left Control' to fly down.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void SpeedInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or pressing WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void BuiltinCheatsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Barnyard has multiple cheats that are already in the game. They are enabled by holding the 'TAB' key while typing codes.

The below codes can be entered on the main menu:
SHOWMESTUFF: Unlock all gallery items
FUNANDGAMES: Unlock all antics
    THATSCALLEDCOWTIPPING: Unlock Cow Tipping
    HANGINGWITHOTIS: Unlock Chasing Chicks
    DOGSPLAYINGPOOL: Unlock Pool
    TIMETOGOTOWORK: Unlock Mud Jumpers
    ILOVEPUMPKINS: Unlock Vegetable Patch Defender
    COWSONBIKES: Unlock Bike Race
    OHMILKME: Unlock Milk Bar

The below codes can be entered while in the game:
IWANTSOME: Get 10 of everything
GIMMESOMELOVE: Get $9999 Gopher Bucks", "Built-in Cheats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}