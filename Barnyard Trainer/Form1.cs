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
                    MessageBox.Show(errorMessage, "Error!");
                }
            }
        }

        void WriteBytes(string address, string value, string errorMessage = "")
        {
            if (!mem.WriteMemory(address, "bytes", value))
            {
                if (errorMessage != "")
                {
                    MessageBox.Show(errorMessage, "Error!");
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
                    MessageBox.Show(errorMessage, "Error!");
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
                WriteFloat(Addresses.cameraTargettedDistance, 0.01f);
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
            }
            else // (restore because this causes issues while on a bike)
            {
                WriteBytes(Addresses.minPitch, "89 01");
                WriteBytes(Addresses.maxPitch, "89 11");
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

        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            WriteFloat(Addresses.fov, (float)fovTrackBar.Value / 100, "Error changing FOV");
        }

        void NoclipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noclipCheckBox.Checked)
            {
                WriteFloat(Addresses.radius, 0.01f, "Error enabling no-clip");
            }
            else
            {
                WriteFloat(Addresses.radius, 0.55f, "Error disabling no-clip");
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

        void SquirtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtCheckBox.Checked)
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
            }
            else
            {
                zoomCheckBox.Enabled = true;
                if (!zoomCheckBox.Checked)
                {
                    DisableCameraZoom(false);
                }
                WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error disabling first person");
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

        void InfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.", "NOTE");
        }
        #endregion
    }
}