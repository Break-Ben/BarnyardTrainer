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
            if (Addresses.inputs["space"].ReadByte() == PRESSED)
                Addresses.values["yPos"].Write(Addresses.values["yPos"].ReadFloat() - 0.2f);
            if (Addresses.inputs["leftControl"].ReadByte() == PRESSED)
                Addresses.values["yPos"].Write(Addresses.values["yPos"].ReadFloat() + 0.2f);
        }
        #endregion

        #region Checkboxes
        // General Cheats
        void FlightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flightCheckBox.Checked)
            {
                Addresses.instructions["gravity"].Nop("Error disabling gravity");
                gravityCheckBox.Enabled = false;
                flightTimer.Start();
            }
            else
            {
                if (!gravityCheckBox.Checked)
                    Addresses.instructions["gravity"].Restore("Error enabling gravity");
                gravityCheckBox.Enabled = true;
                flightTimer.Stop();
            }
        }

        void NoclipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noclipCheckBox.Checked)
            {
                Addresses.unitValues["radius"].Write(0.01f, "Error changing player radius");
                Addresses.instructions["noClip"].Nop("Error enabling no-clip");
            }
            else
            {
                Addresses.unitValues["radius"].Revert("Error changing player radius");
                Addresses.instructions["noClip"].Restore("Error disabling no-clip");
            }
        }

        void GravityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gravityCheckBox.Checked)
                Addresses.instructions["gravity"].Nop("Error disabling gravity");
            else
                Addresses.instructions["gravity"].Restore("Error enabling gravity");
        }

        void LadderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ladderCheckBox.Checked)
            {
                Addresses.unitValues["minClimbSpeed"].Write(15f, "Error enabling fast ladder");
                Addresses.unitValues["maxClimbSpeed"].Write(15f, "Error enabling fast ladder");
            }
            else
            {
                Addresses.unitValues["minClimbSpeed"].Revert("Error disabling fast ladder");
                Addresses.unitValues["maxClimbSpeed"].Revert("Error disabling fast ladder");
            }
        }

        void SquirtDelayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtDelayCheckBox.Checked)
                Addresses.unitValues["squirtDelay"].Write(0f, "Error disabling squirt delay");
            else
                Addresses.unitValues["squirtDelay"].Revert("Error restoring squirt delay");
        }

        void SquirtGlassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (squirtGlassesCheckBox.Checked)
                Addresses.values["cantSquirt"].Write("0x00", "Error enabling squirt without glasses");
            else
                Addresses.values["cantSquirt"].Write("0x01", "Error disabling squirt without glasses");
        }

        void ControllerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            registry.SetValue("ControllerEnabled", controllerCheckBox.Checked);
        }

        void WindowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            registry.SetValue("RealForcedWindowed", windowedCheckBox.Checked);
        }

        // Camera Settings
        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            zoomCheckBox.Enabled = !firstPersonCheckBox.Checked;
            ToggleCameraZoom(!firstPersonCheckBox.Checked && !zoomCheckBox.Checked);
            ToggleCloseCamera(firstPersonCheckBox.Checked);
        }

        void ClampCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (clampCheckBox.Checked)
            {
                Addresses.values["minPitch"].Write(-60f, "Error setting minimum pitch");
                Addresses.values["maxPitch"].Write(90f, "Error setting maximum pitch");
                Addresses.instructions["maxPitchResetOne"].Nop("Error setting maximum pitch");
                Addresses.instructions["maxPitchResetTwo"].Nop("Error setting maximum pitch");
            }
            else
            {
                Addresses.values["minPitch"].Revert("Error setting minimum pitch");
                Addresses.values["maxPitch"].Revert("Error setting maximum pitch");
                Addresses.instructions["maxPitchResetOne"].Restore("Error setting maximum pitch");
                Addresses.instructions["maxPitchResetTwo"].Restore("Error setting maximum pitch");
            }
        }

        void ZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCameraZoom(!zoomCheckBox.Checked);
            if(!firstPersonCheckBox.Checked && zoomCheckBox.Checked)
                Addresses.values["camTargetDistance"].Write(GetCamMaxDistance(), "Error zooming out camera");
        }
        #endregion

        #region Buttons
        // General Cheats
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
                Addresses.values["xPos"].Write(float.Parse(xPosTextBox.Text), "Error applying X position");
                Addresses.values["yPos"].Write(float.Parse(yPosTextBox.Text), "Error applying Y position");
                Addresses.values["zPos"].Write(float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
            else
            {
                Addresses.values["bikeXPos"].Write(float.Parse(xPosTextBox.Text), "Error applying X position");
                Addresses.values["bikeYPos"].Write(float.Parse(yPosTextBox.Text), "Error applying Y position");
                Addresses.values["bikeZPos"].Write(float.Parse(zPosTextBox.Text), "Error applying Z position");
            }
        }

        void MoneyApplyButton_Click(object sender, EventArgs e)
        {
            Addresses.values["money"].Write(Memory.IntStringTo4Bytes(moneyTextBox.Text), "Error applying money");
            Addresses.values["moneyHud"].Write(Memory.IntStringTo4Bytes(moneyTextBox.Text), "Error applying money HUD");
        }

        void ItemsApplyButton_Click(object sender, EventArgs e)
        {
            Addresses.values["itemCount"].Write(Memory.IntStringTo4Bytes(itemsTextBox.Text), "Error applying item count");
        }

        void FlightInfoButton_Click(object sender, EventArgs e)
        {
            Messages.DisplayMessage("'Space' to fly up and 'Left Control' to fly down.");
        }

        void SpeedInfoButton_Click(object sender, EventArgs e)
        {
            Messages.DisplayMessage("Walk speed is when slightly moving the joystick, run speed is when fully moving the joystick or pressing WASD, and sprint speed is while holding shift and moving. All speeds are in m/s.");
        }

        // Camera Settings
        void FovRevertButton_Click(object sender, EventArgs e)
        {
            fovTrackBar.Value = 100;
            Addresses.values["fov"].Revert("Error changing FOV");
        }

        void DistanceRevertButton_Click(object sender, EventArgs e)
        {
            distanceLabel.Text = "(5m)";
            camDistanceTrackBar.Value = 50;
            Addresses.values["camMaxDistance"].Revert("Error changing camera distance");
        }
        #endregion

        #region Track Bars
        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            Addresses.values["fov"].Write(fovTrackBar.Value / 100f, "Error changing FOV");
        }

        void CamDistanceTrackBar_Scroll(object sender, EventArgs e)
        {
            float distance = GetCamMaxDistance();
            distanceLabel.Text = "(" + distance + "m)";
            Addresses.values["camMaxDistance"].Write(distance, "Error changing camera distance");
        }
        #endregion

        #region Custom Functions
        void UpdateConstantAddresses()
        {
            // Infinite stamina
            if (staminaCheckBox.Checked)
            {
                if (IsOnFoot())
                    Addresses.values["stamina"].Write(5f);
                else
                    Addresses.values["bikeStamina"].Write(1f);
            }

            // Infinite ammo
            if (milkCheckBox.Checked)
                Addresses.values["milk"].Write(5f);

            // Water collisions
            if (waterCheckBox.Checked)
            {
                if (!IsOnFoot())
                    Addresses.instructions["waterCollision"].Nop();
                else // Restore because this causes issues while on a bike
                    Addresses.instructions["waterCollision"].Restore();
            }
        }

        void UpdateInfoPage()
        {
            bool isSprinting = IsSprinting();
            float stamina = IsOnFoot() ? Addresses.values["stamina"].ReadFloat() : Addresses.values["bikeStamina"].ReadFloat() * 5;
            int staminaPercent = (int)(stamina * 20);
            double timeUntilFull = Math.Round((5 - stamina) * (IsOnFoot() ? 4 : 3), 2);

            float time = Addresses.values["time"].ReadFloat() / 50f;
            int hour = (int)time;
            int minute = (int)((time - hour) * 60);

            // Text
            dayLabel.Text = "Day/Time:  " + DAYS[Addresses.values["day"].ReadByte()] + ", " + new DateTime(1, 1, 1, hour, minute, 0).ToString("hh:mm tt");
            horizontalSpeedLabel.Text = "Horizontal Velocity:  " + CalculateHorizontalVelocity() + " m/s";
            verticalSpeedLabel.Text = "Vertical Velocity:  " + CalculateVerticalVelocity() + " m/s";
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
            Addresses.unitValues["walkSpeed"].Write(float.Parse(walkTextBox.Text), "Error applying walk speed");
            Addresses.unitValues["runSpeed"].Write(float.Parse(runTextBox.Text), "Error applying run speed");
            Addresses.unitValues["sprintSpeed"].Write(float.Parse(sprintTextBox.Text), "Error applying sprint speed");
            Addresses.unitValues["acceleration"].Write(float.Parse(accTextBox.Text), "Error applying acceleration");
            Addresses.unitValues["deceleration"].Write(float.Parse(decTextBox.Text), "Error applying deceleration");
            Addresses.unitValues["jumpForce"].Write(float.Parse(jumpTextBox.Text), "Error applying jump height");
        }

        void ToggleCameraZoom(bool enable)
        {
            if (enable)
            {
                Addresses.instructions["camZoom"].Restore("Error enabling camera zoom");
                Addresses.instructions["camZoomIn"].Restore("Error enabling camera zoom in");
                Addresses.instructions["camZoomOut"].Restore("Error enabling camera zoom out");
                Addresses.instructions["camReset"].Restore("Error enabling camera reset");
                Addresses.instructions["bikeCamDistanceReset"].Restore("Error enabling camera reset");
                Addresses.instructions["bikeCamHeightReset"].Restore("Error enabling camera reset");
            }
            else
            {
                Addresses.instructions["camZoom"].Nop("Error disabling camera zoom");
                Addresses.instructions["camZoomIn"].Nop("Error disabling camera zoom in");
                Addresses.instructions["camZoomOut"].Nop("Error disabling camera zoom out");
                Addresses.instructions["camReset"].Nop("Error disabling camera reset");
                Addresses.instructions["bikeCamDistanceReset"].Nop("Error disabling camera reset");
                Addresses.instructions["bikeCamHeightReset"].Nop("Error disabling camera reset");
            }
        }

        void ToggleCloseCamera(bool enable)
        {
            if (enable)
            {
                Addresses.values["camTargetDistance"].Write(0.01f, "Error enabling first person");
                Addresses.values["bikeCamTargetDistance"].Write(0.1f, "Error setting bike camera targetted distance");
                Addresses.values["bikeCamTargetHeight"].Write(0f, "Error setting bike camera targetted height");
                Addresses.instructions["opacityChangeOne"].Nop("Error changing player opacity");
                Addresses.instructions["opacityChangeTwo"].Nop("Error changing player opacity");
            }
            else
            {
                Addresses.values["camTargetDistance"].Write(GetCamMaxDistance(), "Error enabling first person");
                Addresses.values["bikeCamTargetDistance"].Write(4f, "Error setting bike camera targetted distance");
                Addresses.values["bikeCamTargetHeight"].Write(0.8f, "Error setting bike camera targetted height");
                Addresses.instructions["opacityChangeOne"].Restore("Error changing player opacity");
                Addresses.instructions["opacityChangeTwo"].Restore("Error changing player opacity");
            }
        }

        bool IsOnFoot()
        {
            return Addresses.values["isOnFoot"].ReadByte() == 1;
        }

        bool IsSprinting()
        {
            return IsOnFoot() ? Addresses.values["isSprintingFoot"].ReadByte() == 1 : Addresses.values["isSprintingBike"].ReadByte() == 4;
        }

        float GetXPos()
        {
            return IsOnFoot() ? Addresses.values["xPos"].ReadFloat() : Addresses.values["bikeXPos"].ReadFloat();
        }

        float GetYPos()
        {
            return IsOnFoot() ? Addresses.values["yPos"].ReadFloat() : Addresses.values["bikeYPos"].ReadFloat();
        }

        float GetZPos()
        {
            return IsOnFoot() ? Addresses.values["zPos"].ReadFloat() : Addresses.values["bikeZPos"].ReadFloat();
        }

        double CalculateHorizontalVelocity()
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

        double CalculateVerticalVelocity()
        {
            yPos = GetYPos();
            long time = stopWatch.ElapsedMilliseconds;
            double speed = Math.Round((yPos - lastYPos) / (time - lastVerticalTime) * 1000, 1);

            lastVerticalTime = time;
            lastYPos = yPos;

            return speed;
        }

        float GetCamMaxDistance()
        {
            return Math.Max(0.1f, camDistanceTrackBar.Value / 10f);
        }
        #endregion
    }
}