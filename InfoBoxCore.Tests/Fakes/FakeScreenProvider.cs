using System.Drawing;
using InfoBox.Internals;

namespace InfoBoxCore.Tests.Fakes
{
    internal class FakeScreenProvider : IScreenProvider
    {
        public Rectangle WorkingArea { get; set; } = new Rectangle(0, 0, 1920, 1080);
    }
}
