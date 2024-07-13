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
        const int PRESSED = 128;
        static readonly string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        readonly Mem mem = new Mem();
        readonly RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\THQ\Barnyard", true);
        readonly Stopwatch stopWatch = new Stopwatch();
        float xPos, zPos, yPos, lastXPos, lastZPos, lastYPos;
        long lastHorizontalTime, lastVerticalTime;

        public BarnyardTrainer()
        {
            InitializeComponent();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            outGameTimer.Start();
            stopWatch.Start();

            controllerCheckBox.Checked = registry.GetValue("ControllerEnabled").ToString() == "1";
            windowedCheckBox.Checked = registry.GetValue("RealForcedWindowed").ToString() == "1";
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
            if (!mem.WriteMemory(address, "float", value.ToString()) && errorMessage != "")
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void WriteBytes(string address, string value, string errorMessage = "")
        {
            if (!mem.WriteMemory(address, "bytes", value) && errorMessage != "")
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Nop(string address, int byteCount, string errorMessage = "")
        {
            string value = "";
            for (int i = 0; i < byteCount; i++)
            {
                value += "90 ";
            }
            if (!mem.WriteMemory(address, "bytes", value.TrimEnd()) && errorMessage != "")
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DisplayMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        bool IsOnFoot()
        {
            return mem.ReadByte(Addresses.isOnFoot) == 1;
        }

        bool IsSprinting()
        {
            return IsOnFoot() ? mem.ReadByte(Addresses.isSprintingFoot) == 1 : mem.ReadByte(Addresses.isSprintingBike) == 4;
        }

        float GetXPos()
        {
            return IsOnFoot() ? mem.ReadFloat(Addresses.xPos) : mem.ReadFloat(Addresses.bikeXPos);
        }

        float GetYPos()
        {
            return IsOnFoot() ? -mem.ReadFloat(Addresses.yPos) : -mem.ReadFloat(Addresses.bikeYPos);
        }

        float GetZPos()
        {
            return IsOnFoot() ? mem.ReadFloat(Addresses.zPos) : mem.ReadFloat(Addresses.bikeZPos);
        }

        double CalculateHorizontalSpeed()
        {
            xPos = GetXPos();
            zPos = GetZPos();
            float deltaX = xPos - lastXPos;
            float deltaZ = zPos - lastZPos;

            long time = stopWatch.ElapsedMilliseconds;
            double speed = Math.Round(Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ) / (time - lastHorizontalTime) * 1000, 1);

            lastHorizontalTime = time;
            lastXPos = xPos;
            lastZPos = zPos;

            return speed;
        }

        double CalculateVerticalSpeed()
        {
            yPos = GetYPos();
            long time = stopWatch.ElapsedMilliseconds;
            double speed = Math.Round((yPos - lastYPos) / (time - lastVerticalTime) * 1000, 1);

            lastVerticalTime = time;
            lastYPos = yPos;

            return speed;
        }

        void UpdateInfoPage()
        {
            bool isSprinting = IsSprinting();
            float stamina = IsOnFoot() ? mem.ReadFloat(Addresses.stamina) : mem.ReadFloat(Addresses.bikeStamina) * 5;
            int staminaPercent = (int)(stamina * 20);
            double timeUntilFull = Math.Round((5 - stamina) * (IsOnFoot() ? 4 : 3), 2);

            dayLabel.Text = "Day:  " + days[mem.ReadByte(Addresses.day)];
            horizontalSpeedLabel.Text = "Horizontal Speed:  " + CalculateHorizontalSpeed() + " m/s";
            verticalSpeedLabel.Text = "Vertical Speed:  " + CalculateVerticalSpeed() + " m/s";
            positionLabel.Text = "Position:  X: " + GetXPos() + ",  Y: " + GetYPos() + ",  Z: " + GetZPos();
            staminaLabel.Text = "Stamina:  " + staminaPercent + "%";
            if (!isSprinting && !staminaCheckBox.Checked)
            {
                staminaLabel.Text += "  (" + timeUntilFull + "s until full)";
            }
            staminaBar.Width = staminaPercent;
            onFootCheckBox.Checked = IsOnFoot();
            sprintingCheckBox.Checked = isSprinting;
        }

        void ApplyMovementStats()
        {
            WriteFloat(Addresses.walkSpeed, float.Parse(walkTextBox.Text), "Error applying walk speed");
            WriteFloat(Addresses.runSpeed, float.Parse(runTextBox.Text), "Error applying run speed");
            WriteFloat(Addresses.sprintSpeed, float.Parse(sprintTextBox.Text), "Error applying sprint speed");
            WriteFloat(Addresses.acceleration, float.Parse(accTextBox.Text), "Error applying acceleration");
            WriteFloat(Addresses.deceleration, float.Parse(decTextBox.Text), "Error applying deceleration");
            WriteFloat(Addresses.jumpForce, float.Parse(jumpTextBox.Text), "Error applying jump height");
        }

        void ToggleCameraZoom(bool enable)
        {
            if (enable)
            {
                WriteBytes(Addresses.cameraZoom, "89 96 88 00 00 00", "Error enabling camera zoom");
                WriteBytes(Addresses.cameraZoomIn, "89 01", "Error enabling camera zoom in");
                WriteBytes(Addresses.cameraZoomOut, "89 8E 88 00 00 00", "Error enabling camera zoom out");
            }
            else
            {
                Nop(Addresses.cameraZoom, 6, "Error disabling camera zoom");
                Nop(Addresses.cameraZoomIn, 2, "Error disabling camera zoom in");
                Nop(Addresses.cameraZoomOut, 6, "Error disabling camera zoom out");
            }
        }
        #endregion

        #region Timers
        void InGameTimer_Tick(object sender, EventArgs e)
        {
            if (mem.GetProcIdFromName("Barnyard.exe") == 0) // If barnyard closes
            {
                statusText.Text = "Disconnected";
                flightTimer.Stop();
                inGameTimer.Stop();
                outGameTimer.Start();
            }

            if (staminaCheckBox.Checked)
            {
                if (IsOnFoot())
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
                if (IsOnFoot())
                {
                    WriteFloat(Addresses.cameraTargettedDistance, 0.01f);
                    WriteFloat(Addresses.opacity, 0f);
                }
            }

            if (IsOnFoot())
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
            else // Restore because this causes issues while on a bike
            {
                WriteBytes(Addresses.minPitch, "89 01");
                WriteBytes(Addresses.maxPitch, "89 11");
                WriteBytes(Addresses.waterCollision, "89 0E");
            }

            if (tabControl.SelectedTab.Name == "infoPage")
            {
                UpdateInfoPage();
            }
        }

        void FlightTimer_Tick(object sender, EventArgs e)
        {
            if (mem.ReadByte(Addresses.space) == PRESSED)
            {
                WriteFloat(Addresses.yPos, mem.ReadFloat(Addresses.yPos) - 0.2f);
            }
            if (mem.ReadByte(Addresses.leftControl) == PRESSED)
            {
                WriteFloat(Addresses.yPos, mem.ReadFloat(Addresses.yPos) + 0.2f);
            }
        }
        #endregion

        #region Checkboxes
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
            ToggleCameraZoom(zoomCheckBox.Checked);
            WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error zooming out camera");
        }

        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstPersonCheckBox.Checked)
            {
                zoomCheckBox.Enabled = false;

                ToggleCameraZoom(true);
                WriteFloat(Addresses.cameraTargettedDistance, 0.01f, "Error enabling first person");
                Nop(Addresses.opacityChangeOne, 2, "Error changing player opacity");
                Nop(Addresses.opacityChangeTwo, 2, "Error changing player opacity");
            }
            else
            {
                zoomCheckBox.Enabled = true;

                if (!zoomCheckBox.Checked)
                {
                    ToggleCameraZoom(false);
                }
                WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error disabling first person");
                WriteBytes(Addresses.opacityChangeOne, "D9 11", "Error changing player opacity");
                WriteBytes(Addresses.opacityChangeTwo, "89 01", "Error changing player opacity");
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
        #endregion

        #region Buttons
        void MovementRevertButton_Click(object sender, EventArgs e)
        {
            walkTextBox.Text = "1.5";
            runTextBox.Text = "4";
            sprintTextBox.Text = "7";
            accTextBox.Text = "10";
            decTextBox.Text = "30";
            jumpTextBox.Text = "8.5";
            ApplyMovementStats();
        }

        void MovementApplyButton_Click(object sender, EventArgs e)
        {
            ApplyMovementStats();
        }

        void PositionRefreshButton_Click(object sender, EventArgs e)
        {
            xPosTextBox.Text = GetXPos().ToString();
            yPosTextBox.Text = GetYPos().ToString();
            zPosTextBox.Text = GetZPos().ToString();
        }

        void PositionApplyButton_Click(object sender, EventArgs e)
        {
            if (IsOnFoot())
            {
                WriteFloat(Addresses.xPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                WriteFloat(Addresses.yPos, -float.Parse(yPosTextBox.Text), "Error applying Y position");
                WriteFloat(Addresses.zPos, float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
            else
            {
                WriteFloat(Addresses.bikeXPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                WriteFloat(Addresses.bikeYPos, -float.Parse(yPosTextBox.Text), "Error applying Y position");
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
            DisplayMessage("'Space' to fly up and 'Left Control' to fly down.");
        }

        void PitchInfoButton_Click(object sender, EventArgs e)
        {
            DisplayMessage("Allows you to look higher/lower. Note that it is completely unclamped so weird behaviour can occur if you look too high/low.");
        }

        void SpeedInfoButton_Click(object sender, EventArgs e)
        {
            DisplayMessage("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or pressing WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.");
        }
        #endregion
    }
}