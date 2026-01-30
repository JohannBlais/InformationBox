using System.Drawing;
using System.Windows.Forms;
using InfoBox.Internals;

namespace InfoBoxCore.Tests.Fakes
{
    internal class FakeTextMeasurer : ITextMeasurer
    {
        public int WidthPerChar { get; set; } = 8;
        public int LineHeight { get; set; } = 16;

        public Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Size(0, LineHeight);
            }

            return new Size(text.Length * WidthPerChar, LineHeight);
        }
    }
}
