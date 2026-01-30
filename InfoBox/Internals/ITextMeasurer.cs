namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    internal interface ITextMeasurer
    {
        Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags);
    }
}
