namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    internal class WinFormsScreenProvider : IScreenProvider
    {
        public Rectangle WorkingArea => Screen.PrimaryScreen.WorkingArea;
    }
}
