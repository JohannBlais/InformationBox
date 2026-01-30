namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    internal class WinFormsTextMeasurer : ITextMeasurer
    {
        public Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
        {
            return TextRenderer.MeasureText(text, font, proposedSize, flags);
        }
    }
}
