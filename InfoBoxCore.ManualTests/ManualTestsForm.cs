using InfoBox;

namespace InfoBoxCore.ManualTests
{
    public partial class ManualTestsForm : Form
    {
        public ManualTestsForm()
        {
            InitializeComponent();
        }

        private void btnTestFixedWidthEightPoints_Click(object sender, EventArgs e)
        {
            var testString = "         My Program's Help Window" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Command Line Arguments                 What the argument does" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "-h, --help                             Display this help window" + Environment.NewLine;
            testString += "-v, --version                          Display version information" + Environment.NewLine;
            testString += "-l, --log                              Display log file" + Environment.NewLine;
            testString += "-d, --display                          Display variables" + Environment.NewLine;
            testString += "-a, --arguments                        Display command line arguments" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Line 11\nLine 12\nLine 13\nLine 14\nLine 15\nLine 16\nLine 17\nLine 18\nLine 19\nLine 20\n";
            testString += "Line 21\nLine 22\nLine 23\nLine 24\nLine 25\nLine 26\nLine 27\nLine 28\nLine 29\nLine 30\n";
            testString += "Line 31\nLine 32\nLine 33\nLine 34\nLine 35\nLine 36\nLine 37\nLine 38\nLine 39\nLine 40\n";
            testString += "Line 41\nLine 42\nLine 43\nLine 44\nLine 45\nLine 46\nLine 47\nLine 48\nLine 49\nLine 50\n";
            testString += "Line 51\nLine 52\nLine 53\nLine 54\nLine 55\nLine 56\nLine 57\nLine 58\nLine 59\nLine 60";

            InformationBox.Show(testString,
                 title: "My Program's Help Window Command Line Help",
                 icon: InformationBoxIcon.Information,
                 titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                 autoSizeMode: InformationBoxAutoSizeMode.FitToText,
                 fontParameters: new FontParameters(new System.Drawing.Font("Courier New", 8.25F))
             );
        }

        private void btnTestFixedWidthFourPoints_Click(object sender, EventArgs e)
        {
            var testString = "         My Program's Help Window" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Command Line Arguments                 What the argument does" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "-h, --help                             Display this help window" + Environment.NewLine;
            testString += "-v, --version                          Display version information" + Environment.NewLine;
            testString += "-l, --log                              Display log file" + Environment.NewLine;
            testString += "-d, --display                          Display variables" + Environment.NewLine;
            testString += "-a, --arguments                        Display command line arguments" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Line 11\nLine 12\nLine 13\nLine 14\nLine 15\nLine 16\nLine 17\nLine 18\nLine 19\nLine 20\n";
            testString += "Line 21\nLine 22\nLine 23\nLine 24\nLine 25\nLine 26\nLine 27\nLine 28\nLine 29\nLine 30\n";
            testString += "Line 31\nLine 32\nLine 33\nLine 34\nLine 35\nLine 36\nLine 37\nLine 38\nLine 39\nLine 40\n";
            testString += "Line 41\nLine 42\nLine 43\nLine 44\nLine 45\nLine 46\nLine 47\nLine 48\nLine 49\nLine 50\n";
            testString += "Line 51\nLine 52\nLine 53\nLine 54\nLine 55\nLine 56\nLine 57\nLine 58\nLine 59\nLine 60";

            InformationBox.Show(testString,
                 title: "My Program's Help Window Command Line Help",
                 icon: InformationBoxIcon.Information,
                 titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                 autoSizeMode: InformationBoxAutoSizeMode.FitToText,
                 fontParameters: new FontParameters(new System.Drawing.Font("Courier New", 4.25F))
             );
        }

        private void btnTestFixedWidthTwelvePoints_Click(object sender, EventArgs e)
        {
            var testString = "         My Program's Help Window" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Command Line Arguments                 What the argument does" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "-h, --help                             Display this help window" + Environment.NewLine;
            testString += "-v, --version                          Display version information" + Environment.NewLine;
            testString += "-l, --log                              Display log file" + Environment.NewLine;
            testString += "-d, --display                          Display variables" + Environment.NewLine;
            testString += "-a, --arguments                        Display command line arguments" + Environment.NewLine;
            testString += "" + Environment.NewLine;
            testString += "Line 11\nLine 12\nLine 13\nLine 14\nLine 15\nLine 16\nLine 17\nLine 18\nLine 19\nLine 20\n";
            testString += "Line 21\nLine 22\nLine 23\nLine 24\nLine 25\nLine 26\nLine 27\nLine 28\nLine 29\nLine 30\n";
            testString += "Line 31\nLine 32\nLine 33\nLine 34\nLine 35\nLine 36\nLine 37\nLine 38\nLine 39\nLine 40\n";
            testString += "Line 41\nLine 42\nLine 43\nLine 44\nLine 45\nLine 46\nLine 47\nLine 48\nLine 49\nLine 50\n";
            testString += "Line 51\nLine 52\nLine 53\nLine 54\nLine 55\nLine 56\nLine 57\nLine 58\nLine 59\nLine 60";

            InformationBox.Show(testString,
                 title: "My Program's Help Window Command Line Help",
                 icon: InformationBoxIcon.Information,
                 titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                 autoSizeMode: InformationBoxAutoSizeMode.FitToText,
                 fontParameters: new FontParameters(new System.Drawing.Font("Courier New", 12.25F))
             );
        }
    }
}
