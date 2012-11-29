using System.Windows.Forms;

namespace StroMoHab_Objects.Forms
{
    /// <summary>
    /// Draws a MessageBox that always stays on top
    /// </summary>
    static public class TopMostMessageBox
    {
        /// <summary>
        /// Draws a MessageBox that always stays on top with an an OK button
        /// </summary>
        static public DialogResult Show(string message)
        {
            return Show(message, string.Empty, MessageBoxButtons.OK);
        }
        /// <summary>
        /// Draws a MessageBox that always stays on top with a title and an OK button
        /// </summary>
        static public DialogResult Show(string message, string title)
        {
            return Show(message, title, MessageBoxButtons.OK);
        }
        /// <summary>
        /// Draws a MessageBox that always stays on top with a tile and selectable button
        /// </summary>
        static public DialogResult Show(string message, string title,
            MessageBoxButtons buttons)
        {
            // Create a host form that is a TopMost window which will be the 

            // parent of the MessageBox.

            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the 

            // visible screen and make it as small as possible

            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10,
                rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost

            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            // Finally show the MessageBox with the form just created as its owner

            DialogResult result = MessageBox.Show(topmostForm, message, title,
                buttons);
            topmostForm.Dispose(); // clean it up all the way


            return result;
        }

        /// <summary>
        /// Draws a MessageBox that always stays on top with a title, selectable buttons and an icon
        /// </summary>
        static public DialogResult Show(string message, string title,
            MessageBoxButtons buttons,MessageBoxIcon icon)
        {
            // Create a host form that is a TopMost window which will be the 

            // parent of the MessageBox.

            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the 

            // visible screen and make it as small as possible

            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10,
                rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost

            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            // Finally show the MessageBox with the form just created as its owner

            DialogResult result = MessageBox.Show(topmostForm, message, title,
                buttons,icon);
            topmostForm.Dispose(); // clean it up all the way


            return result;
        }
    }
   

}
