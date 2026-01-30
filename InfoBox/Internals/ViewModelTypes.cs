namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    internal struct ButtonDefinition
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }

    internal struct CheckBoxConfiguration
    {
        public string Text { get; set; }
        public bool Visible { get; set; }
        public bool Checked { get; set; }
        public ContentAlignment TextAlign { get; set; }
        public ContentAlignment CheckAlign { get; set; }
    }

    internal struct WindowStyleConfiguration
    {
        public Color BarsBackColor { get; set; }
        public Color FormBackColor { get; set; }
        public FormBorderStyle BorderStyle { get; set; }
        public bool TitleLabelVisible { get; set; }
        public bool AdjustPanelTop { get; set; }
        public bool RemoveSideBorder { get; set; }
    }

    internal struct IconConfiguration
    {
        public bool IconPanelVisible { get; set; }
        public Image IconImage { get; set; }
        public bool ShowFormIcon { get; set; }
        public Icon FormIcon { get; set; }
    }

    internal struct AutoCloseTickResult
    {
        public bool ShouldClose { get; set; }
        public bool ShouldStopTimer { get; set; }
        public string ButtonName { get; set; }
        public int ButtonIndex { get; set; }
        public InformationBoxResult DirectResult { get; set; }
        public bool UseDirectResult { get; set; }
        public string UpdatedButtonText { get; set; }
    }
}
