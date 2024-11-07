using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Barnyard_Trainer
{
    public partial class BarnyardTrainer : Form
    {
        #region Initialisation
        // Constants
        const int PRESSED = 128;
        static readonly string[] DAYS = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        // Objects
        readonly RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\THQ\Barnyard", true);
        readonly Stopwatch stopWatch = new Stopwatch();

        // Variables
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
        #endregion

        #region Timers
        void OutGameTimer_Tick(object sender, EventArgs e)
        {
            if (Memory.IsBarnyardOpen())
            {
                Memory.OpenProcess();
                statusText.Text = "Connected";
                outGameTimer.Stop();
                inGameTimer.Start();
            }
        }

        void InGameTimer_Tick(object sender, EventArgs e)
        {
            if (!Memory.IsBarnyardOpen())
            {
                statusText.Text = "Disconnected";
                flightTimer.Stop();
                inGameTimer.Stop();
                outGameTimer.Start();
            }
            else
            {
                UpdateConstantAddresses();
                if (tabControl.SelectedTab.Name == "infoPage")
                    UpdateInfoPage();
            }
        }

        void FlightTimer_Tick(object sender, EventArgs e)
        {
            if (Memory.ReadByte(Addresses.space) == PRESSED)
                Memory.WriteFloat(Addresses.yPos, Memory.ReadFloat(Addresses.yPos) - 0.2f);
            if (Memory.ReadByte(Addresses.leftControl) == PRESSED)
                Memory.WriteFloat(Addresses.yPos, Memory.ReadFloat(Addresses.yPos) + 0.2f);
        }
        #endregion

        #region Checkboxes
        void FlightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flightCheckBox.Checked)
            {
                Memory.Nop(Addresses.gravity, 3, "Error disabling gravity");
                gravityCheckBox.Enabled = false;
                flightTimer.Start();
            }
            else
            {
                if (!gravityCheckBox.Checked)
                    Memory.WriteBytes(Addresses.gravity, "D9 59 1C", "Error enabling gravity");
                gravityCheckBox.Enabled = true;
                flightTimer.Stop();
            }
        }

        void NoclipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noclipCheckBox.Checked)
            {
                Memory.WriteFloat(Addresses.radius, 0.01f, "Error changing player radius");
                Memory.Nop(Addresses.noClip, 2, "Error enabling no-clip");
            }
            else
            {
                Memory.WriteFloat(Addresses.radius, 0.55f, "Error changing player radius");
                Memory.WriteBytes(Addresses.noClip, "89 11", "Error disabling no-clip");
            }
        }

        void GravityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gravityCheckBox.Checked)
                Memory.Nop(Addresses.gravity, 3, "Error disabling gravity");
            else
                Memory.WriteBytes(Addresses.gravity, "D9 59 1C", "Error enabling gravity");
        }

        void LadderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ladderCheckBox.Checked)
            {
                Memory.WriteFloat(Addresses.minClimbSpeed, 15f, "Error enabling fast ladder");
                Memory.WriteFloat(Addresses.maxClimbSpeed, 15f, "Error enabling fast ladder");
            }
            else
            {
                Memory.WriteFloat(Addresses.minClimbSpeed, 1f, "Error enabling fast ladder");
                Memory.WriteFloat(Addresses.maxClimbSpeed, 1.3f, "Error enabling fast ladder");
            }
        }

        void SquirtDelayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtDelayCheckBox.Checked)
                Memory.WriteFloat(Addresses.squirtDelay, 0f, "Error disabling squirt delay");
            else
                Memory.WriteFloat(Addresses.squirtDelay, 0.5f, "Error restoring squirt delay");
        }

        void SquirtGlassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtGlassesCheckBox.Checked)
                Memory.WriteBytes(Addresses.cantSquirt, "0x00", "Error enabling squirt without glasses");
            else
                Memory.WriteBytes(Addresses.cantSquirt, "0x01", "Error disabling squirt without glasses");
        }

        void ZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCameraZoom(zoomCheckBox.Checked);
            Memory.WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error zooming out camera");
        }

        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstPersonCheckBox.Checked)
            {
                zoomCheckBox.Enabled = false;

                ToggleCameraZoom(true);
                Memory.WriteFloat(Addresses.cameraTargettedDistance, 0.01f, "Error enabling first person");
                Memory.Nop(Addresses.opacityChangeOne, 2, "Error changing player opacity");
                Memory.Nop(Addresses.opacityChangeTwo, 2, "Error changing player opacity");
            }
            else
            {
                zoomCheckBox.Enabled = true;

                if (!zoomCheckBox.Checked)
                    ToggleCameraZoom(false);
                Memory.WriteFloat(Addresses.cameraTargettedDistance, 5f, "Error disabling first person");
                Memory.WriteBytes(Addresses.opacityChangeOne, "D9 11", "Error changing player opacity");
                Memory.WriteBytes(Addresses.opacityChangeTwo, "89 01", "Error changing player opacity");
            }
        }

        void ControllerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            registry.SetValue("ControllerEnabled", controllerCheckBox.Checked);
        }

        void WindowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            registry.SetValue("RealForcedWindowed", windowedCheckBox.Checked);
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
                Memory.WriteFloat(Addresses.xPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                Memory.WriteFloat(Addresses.yPos, -float.Parse(yPosTextBox.Text), "Error applying Y position");
                Memory.WriteFloat(Addresses.zPos, float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
            else
            {
                Memory.WriteFloat(Addresses.bikeXPos, float.Parse(xPosTextBox.Text), "Error applying X position");
                Memory.WriteFloat(Addresses.bikeYPos, -float.Parse(yPosTextBox.Text), "Error applying Y position");
                Memory.WriteFloat(Addresses.bikeZPos, float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
        }

        void MoneyApplyButton_Click(object sender, EventArgs e)
        {
            Memory.WriteBytes(Addresses.money, Memory.IntStringTo4Bytes(moneyTextBox.Text), "Error applying money");
            Memory.WriteBytes(Addresses.moneyHud, Memory.IntStringTo4Bytes(moneyTextBox.Text), "Error applying money HUD");
        }

        void ItemsApplyButton_Click(object sender, EventArgs e)
        {
            Memory.WriteBytes(Addresses.itemCount, Memory.IntStringTo4Bytes(itemsTextBox.Text), "Error applying item count");
        }

        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            Memory.WriteFloat(Addresses.fov, (float)fovTrackBar.Value / 100, "Error changing FOV");
        }

        void FlightInfoButton_Click(object sender, EventArgs e)
        {
            Messages.DisplayMessage("'Space' to fly up and 'Left Control' to fly down.");
        }

        void PitchInfoButton_Click(object sender, EventArgs e)
        {
            Messages.DisplayMessage("Allows you to look higher/lower. Note that it is completely unclamped so weird behaviour can occur if you look too high/low.");
        }

        void SpeedInfoButton_Click(object sender, EventArgs e)
        {
            Messages.DisplayMessage("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or pressing WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.");
        }
        #endregion

        #region Custom Functions
        void UpdateConstantAddresses()
        {
            if (staminaCheckBox.Checked)
            {
                if (IsOnFoot())
                    Memory.WriteFloat(Addresses.stamina, 5f);
                else
                    Memory.WriteFloat(Addresses.bikeStamina, 1f);
            }

            if (milkCheckBox.Checked)
                Memory.WriteFloat(Addresses.milk, 5f);

            if (firstPersonCheckBox.Checked)
            {
                if (IsOnFoot())
                {
                    Memory.WriteFloat(Addresses.cameraTargettedDistance, 0.01f);
                    Memory.WriteFloat(Addresses.opacity, 0f);
                }
            }

            if (IsOnFoot())
            {
                if (clampCheckBox.Checked)
                {
                    Memory.Nop(Addresses.minPitch, 2);
                    Memory.Nop(Addresses.maxPitch, 2);
                }
                else
                {
                    Memory.WriteBytes(Addresses.minPitch, "89 01");
                    Memory.WriteBytes(Addresses.maxPitch, "89 11");
                }

                if (waterCheckBox.Checked)
                    Memory.Nop(Addresses.waterCollision, 2);
                else
                    Memory.WriteBytes(Addresses.waterCollision, "89 0E");
            }
            else // Restore because this causes issues while on a bike
            {
                Memory.WriteBytes(Addresses.minPitch, "89 01");
                Memory.WriteBytes(Addresses.maxPitch, "89 11");
                Memory.WriteBytes(Addresses.waterCollision, "89 0E");
            }
        }

        void UpdateInfoPage()
        {
            bool isSprinting = IsSprinting();
            float stamina = IsOnFoot() ? Memory.ReadFloat(Addresses.stamina) : Memory.ReadFloat(Addresses.bikeStamina) * 5;
            int staminaPercent = (int)(stamina * 20);
            double timeUntilFull = Math.Round((5 - stamina) * (IsOnFoot() ? 4 : 3), 2);

            // Text
            dayLabel.Text = "Day:  " + DAYS[Memory.ReadByte(Addresses.day)];
            horizontalSpeedLabel.Text = "Horizontal Speed:  " + CalculateHorizontalSpeed() + " m/s";
            verticalSpeedLabel.Text = "Vertical Speed:  " + CalculateVerticalSpeed() + " m/s";
            positionLabel.Text = "Position:  X: " + GetXPos() + ",  Y: " + GetYPos() + ",  Z: " + GetZPos();
            staminaLabel.Text = "Stamina:  " + staminaPercent + "%";
            if (!isSprinting && !staminaCheckBox.Checked)
                staminaLabel.Text += "  (" + timeUntilFull + "s until full)";
            staminaBar.Width = staminaPercent;

            // Checkboxes
            onFootCheckBox.Checked = IsOnFoot();
            sprintingCheckBox.Checked = isSprinting;
        }

        void ApplyMovementStats()
        {
            Memory.WriteFloat(Addresses.walkSpeed, float.Parse(walkTextBox.Text), "Error applying walk speed");
            Memory.WriteFloat(Addresses.runSpeed, float.Parse(runTextBox.Text), "Error applying run speed");
            Memory.WriteFloat(Addresses.sprintSpeed, float.Parse(sprintTextBox.Text), "Error applying sprint speed");
            Memory.WriteFloat(Addresses.acceleration, float.Parse(accTextBox.Text), "Error applying acceleration");
            Memory.WriteFloat(Addresses.deceleration, float.Parse(decTextBox.Text), "Error applying deceleration");
            Memory.WriteFloat(Addresses.jumpForce, float.Parse(jumpTextBox.Text), "Error applying jump height");
        }

        void ToggleCameraZoom(bool enable)
        {
            if (enable)
            {
                Memory.WriteBytes(Addresses.cameraZoom, "89 96 88 00 00 00", "Error enabling camera zoom");
                Memory.WriteBytes(Addresses.cameraZoomIn, "89 01", "Error enabling camera zoom in");
                Memory.WriteBytes(Addresses.cameraZoomOut, "89 8E 88 00 00 00", "Error enabling camera zoom out");
            }
            else
            {
                Memory.Nop(Addresses.cameraZoom, 6, "Error disabling camera zoom");
                Memory.Nop(Addresses.cameraZoomIn, 2, "Error disabling camera zoom in");
                Memory.Nop(Addresses.cameraZoomOut, 6, "Error disabling camera zoom out");
            }
        }

        bool IsOnFoot()
        {
            return Memory.ReadByte(Addresses.isOnFoot) == 1;
        }

        bool IsSprinting()
        {
            return IsOnFoot() ? Memory.ReadByte(Addresses.isSprintingFoot) == 1 : Memory.ReadByte(Addresses.isSprintingBike) == 4;
        }

        float GetXPos()
        {
            return IsOnFoot() ? Memory.ReadFloat(Addresses.xPos) : Memory.ReadFloat(Addresses.bikeXPos);
        }

        float GetYPos()
        {
            return IsOnFoot() ? -Memory.ReadFloat(Addresses.yPos) : -Memory.ReadFloat(Addresses.bikeYPos);
        }

        float GetZPos()
        {
            return IsOnFoot() ? Memory.ReadFloat(Addresses.zPos) : Memory.ReadFloat(Addresses.bikeZPos);
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
        #endregion
    }
}