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

        void Write(string address, string value, string type, string errorMessage = "Error")
        {
            if (!mem.WriteMemory(address, type, value))
            {
                MessageBox.Show(errorMessage, "Error!");
            }
        }

        void Nop(string address, int bytes, string errorMessage = "Error")
        {
            string value = "";
            for (int i = 0; i < bytes; i++)
            {
                value += "90 ";
            }
            if (!mem.WriteMemory(address, "bytes", value.TrimEnd()))
            {
                MessageBox.Show(errorMessage, "Error!");
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
                Write(Addresses.cameraZoom, "89 96 88 00 00 00", "bytes", "Error enabling camera zoom");
                Write(Addresses.cameraZoomIn, "89 01", "bytes", "Error enabling camera zoom in");
                Write(Addresses.cameraZoomOut, "89 8E 88 00 00 00", "bytes", "Error enabling camera zoom out");
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
            if(OnFoot())
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
                if(OnFoot())
                {
                    mem.WriteMemory(Addresses.stamina, "float", "5");
                }
                else
                {
                    mem.WriteMemory(Addresses.bikeStamina, "float", "1");
                }
            }
            if (milkCheckBox.Checked)
            {
                mem.WriteMemory(Addresses.milk, "float", "5");
            }
            if (firstPersonCheckBox.Checked)
            {
                mem.WriteMemory(Addresses.cameraTargettedDistance, "float", "0.01");
            }
            if(OnFoot())
            {
                if (clampCheckBox.Checked)
                {
                    Nop(Addresses.minPitch, 2, "Error disabling minimum pitch");
                    Nop(Addresses.maxPitch, 2, "Error disabling maximum pitch");
                }
                else
                {
                    Write(Addresses.minPitch, "89 01", "bytes", "Error enabling minimum pitch");
                    Write(Addresses.maxPitch, "89 11", "bytes", "Error enabling maximum pitch");
                }
            }
            else // (restore because this causes issues while on a bike)
            {
                Write(Addresses.minPitch, "89 01", "bytes", "Error enabling minimum pitch");
                Write(Addresses.maxPitch, "89 11", "bytes", "Error enabling maximum pitch");
            }
        }

        void MoneyApplyButton_Click(object sender, EventArgs e)
        {
            Write(Addresses.money, IntStringTo4Bytes(moneyTextBox.Text), "bytes", "Error applying money");
            Write(Addresses.moneyHud, IntStringTo4Bytes(moneyTextBox.Text), "bytes", "Error applying money HUD");
        }

        void ItemsApplyButton_Click(object sender, EventArgs e)
        {
            Write(Addresses.itemCount, IntStringTo4Bytes(itemsTextBox.Text), "bytes", "Error applying item count");
        }

        void PositionRefreshButton_Click(object sender, EventArgs e)
        {
            if(OnFoot())
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
            if(OnFoot())
            {
                Write(Addresses.xPos, xPosTextBox.Text, "float", "Error applying X position");
                Write(Addresses.yPos, (-1f * float.Parse(yPosTextBox.Text)).ToString(), "float", "Error applying Y position");
                Write(Addresses.zPos, zPosTextBox.Text, "float", "Error applying Z position");
            }
            else
            {
                Write(Addresses.bikeXPos, xPosTextBox.Text, "float", "Error applying X position");
                Write(Addresses.bikeYPos, (-1f * float.Parse(yPosTextBox.Text)).ToString(), "float", "Error applying Y position");
                Write(Addresses.bikeZPos, zPosTextBox.Text, "float", "Error applying Z position");
            }
        }

        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            Write(Addresses.fov, ((float)fovTrackBar.Value / 100).ToString(), "float", "Error changing FOV");
        }

        void NoclipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noclipCheckBox.Checked)
            {
                Write(Addresses.radius, "0.01", "float", "Error enabling no-clip");
            }
            else
            {
                Write(Addresses.radius, "0.55", "float", "Error disabling no-clip");
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
                Write(Addresses.gravity, "D9 59 1C", "bytes", "Error enabling gravity");
            }
        }

        void SquirtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtCheckBox.Checked)
            {
                Write(Addresses.cantSquirt, "0x00", "byte", "Error enabling squirt without glasses");
            }
            else
            {
                Write(Addresses.cantSquirt, "0x01", "byte", "Error disabling squirt without glasses");
            }
        }

        void ZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisableCameraZoom(zoomCheckBox.Checked);
            Write(Addresses.cameraTargettedDistance, "5", "float", "Error zooming out camera");
        }

        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstPersonCheckBox.Checked)
            {
                zoomCheckBox.Enabled = false;
                DisableCameraZoom(true);
                Write(Addresses.cameraTargettedDistance, "0.01", "float", "Error enabling first person");
            }
            else
            {
                zoomCheckBox.Enabled = true;
                if (!zoomCheckBox.Checked)
                {
                    DisableCameraZoom(false);
                }
                Write(Addresses.cameraTargettedDistance, "5", "float", "Error disabling first person");
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
            Write(Addresses.walkSpeed, walkTextBox.Text, "float", "Error applying walk speed");
            Write(Addresses.runSpeed, runTextBox.Text, "float", "Error applying run speed");
            Write(Addresses.sprintSpeed, sprintTextBox.Text, "float", "Error applying sprint speed");
            Write(Addresses.acceleration, accTextBox.Text, "float", "Error applying acceleration");
            Write(Addresses.deceleration, decTextBox.Text, "float", "Error applying deceleration");
            Write(Addresses.jumpForce, jumpTextBox.Text, "float", "Error applying jump height");
        }

        void InfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.", "NOTE");
        }
        #endregion
    }
}