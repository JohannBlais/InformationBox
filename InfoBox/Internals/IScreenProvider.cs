namespace InfoBox.Internals
{
    using System.Drawing;

    internal interface IScreenProvider
    {
        Rectangle WorkingArea { get; }
    }
}
