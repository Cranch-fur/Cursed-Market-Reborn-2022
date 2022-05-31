using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public static class Messaging
    {
        public static void ShowMessage(string message,  
            MessageBoxButtons messageButtons = MessageBoxButtons.OK, 
                MessageBoxIcon messageType = MessageBoxIcon.None, 
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            MessageBox.Show(message, Globals.SelfExecutableName, messageButtons, messageType, defaultButton);
        }

        public static DialogResult ShowDialog(string message,
            MessageBoxButtons messageButtons = MessageBoxButtons.OK,
                MessageBoxIcon messageType = MessageBoxIcon.None,
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(message, Globals.SelfExecutableName, messageButtons, messageType, defaultButton);
        }
    }
}
