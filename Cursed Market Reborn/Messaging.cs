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

        public static void ShowNotify(string title,
            string message,
                System.Drawing.Icon notifyIcon,
                    ToolTipIcon tooltipIcon = ToolTipIcon.None)
        {
            NotifyIcon winNotify = new NotifyIcon();
            winNotify.Visible = true;
            winNotify.Icon = notifyIcon;


            winNotify.BalloonTipTitle = title;
            winNotify.BalloonTipText = message;
            winNotify.BalloonTipIcon = tooltipIcon;

            winNotify.ShowBalloonTip(3000);

            winNotify.BalloonTipClosed += (sender, e) => winNotify.Dispose();
        }
    }
}
