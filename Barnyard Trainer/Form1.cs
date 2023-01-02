using Memory;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Barnyard_Trainer
{
    public partial class BarnyardTrainer : Form
    {
        #region Variables / Initialisation
        readonly string moneyAddress = "Barnyard.exe+0x003B493C,20";
        readonly string moneyHudAddress = "Barnyard.exe+0x003B493C,1C";
        readonly string itemCountAddress = "Barnyard.exe+0037E344,3C,50,530";
        readonly string xPosAddress = "Barnyard.exe+0037E344,3C,1C,20,18";
        readonly string yPosAddress = "Barnyard.exe+0037E344,3C,1C,20,1C";
        readonly string zPosAddress = "Barnyard.exe+0037E344,3C,1C,20,20";
        readonly string fovAddress = "0x007822ac";
        readonly string staminaAddress = "Barnyard.exe+0x003B493C,28";
        readonly string milkAddress = "Barnyard.exe+0x0037E344,3C,70,C4";
        readonly string cantSquirtAddress = "0x007810e4";
        readonly string cameraZoomAddress = "Barnyard.exe+0x63DF9";
        readonly string cameraZoomInAddress = "Barnyard.exe+0x63E50";
        readonly string cameraZoomOutAddress = "Barnyard.exe+63DEB";
        readonly string cameraTargettedDistanceAddress = "Barnyard.exe+0x003822E0,68,88";

        Mem mem = new Mem();

        public BarnyardTrainer()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            outGameTimer.Start();
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
        #endregion

        #region Main Code
        void InGameTimer_Tick(object sender, EventArgs e)
        {
            if (mem.GetProcIdFromName("Barnyard.exe") == 0) // If barnyard closed
            {
                statusText.Text = "Disconnected";
                inGameTimer.Stop();
                outGameTimer.Start();
            }

            if (staminaCheckBox.Checked)
            {
                mem.WriteMemory(staminaAddress, "float", "5");
            }
            if (milkCheckBox.Checked)
            {
                mem.WriteMemory(milkAddress, "float", "5");
            }
            if (firstPersonCheckBox.Checked)
            {
                mem.WriteMemory(cameraTargettedDistanceAddress, "float", "0.01");
            }
        }

        void MoneyApplyButton_Click(object sender, EventArgs e)
        {
            Write(moneyAddress, IntStringTo4Bytes(moneyTextBox.Text), "bytes", "Error applying money");
            Write(moneyHudAddress, IntStringTo4Bytes(moneyTextBox.Text), "bytes", "Error applying money HUD");
        }

        void ItemsApplyButton_Click(object sender, EventArgs e)
        {
            Write(itemCountAddress, IntStringTo4Bytes(itemsTextBox.Text), "bytes", "Error applying item count");
        }

        void PositionRefreshButton_Click(object sender, EventArgs e)
        {
            xPosTextBox.Text = mem.ReadFloat(xPosAddress).ToString();
            yPosTextBox.Text = (-1f*mem.ReadFloat(yPosAddress)).ToString();
            zPosTextBox.Text = mem.ReadFloat(zPosAddress).ToString();
        }

        void PositionApplyButton_Click(object sender, EventArgs e)
        {
            Write(xPosAddress, xPosTextBox.Text, "float", "Error applying X position");
            Write(yPosAddress, (-1f * float.Parse(yPosTextBox.Text)).ToString(), "float", "Error applying Y position");
            Write(zPosAddress, zPosTextBox.Text, "float", "Error applying Z position");
        }

        void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            Write(fovAddress, ((float)fovTrackBar.Value / 100).ToString(), "float", "Error changing FOV");
        }

        void SquirtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(squirtCheckBox.Checked)
            {
                Write(cantSquirtAddress, "0x00", "byte", "Error enabling squirt without glasses");
            }
            else
            {
                Write(cantSquirtAddress, "0x01", "byte", "Error disabling squirt without glasses");
            }
        }

        void ZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisableCameraZoom(zoomCheckBox.Checked);
            Write(cameraTargettedDistanceAddress, "5", "float", "Error zooming out camera");
        }

        void DisableCameraZoom(bool disabled)
        {
            if (disabled)
            {
                Nop(cameraZoomAddress, 6, "Error disabling camera zoom");
                Nop(cameraZoomInAddress, 2, "Error disabling camera zoom in");
                Nop(cameraZoomOutAddress, 6, "Error disabling camera zoom out");
            }
            else
            {
                Write(cameraZoomAddress, "89 96 88 00 00 00", "bytes", "Error enabling camera zoom");
                Write(cameraZoomInAddress, "89 01", "bytes", "Error enabling camera zoom in");
                Write(cameraZoomOutAddress, "89 8E 88 00 00 00", "bytes", "Error enabling camera zoom out");
            }
        }

        void FirstPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(firstPersonCheckBox.Checked)
            {
                zoomCheckBox.Enabled = false;
                DisableCameraZoom(true);
                Write(cameraTargettedDistanceAddress, "0.01", "float", "Error enabling first person");
            }
            else
            {
                zoomCheckBox.Enabled = true;
                if(!zoomCheckBox.Checked)
                {
                    DisableCameraZoom(false);
                }
                Write(cameraTargettedDistanceAddress, "5", "float", "Error disabling first person");
            }
        }
        #endregion
    }
}
