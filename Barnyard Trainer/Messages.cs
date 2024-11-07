using System.Windows.Forms;

namespace Barnyard_Trainer
{
    public static class Messages
    {
        public static void DisplayMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void DisplayError(string message)
        {
            MessageBox.Show(message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}