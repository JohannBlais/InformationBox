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

        private void btnTestLongLinesFixedFont_Click(object sender, EventArgs e)
        {
            InformationBox.Show("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas porttitor ante et lacinia tristique. Suspendisse massa felis, dapibus nec tristique a, malesuada ut mi. In fermentum augue vel odio rhoncus dignissim. Sed cursus ipsum lacinia efficitur consequat. Integer sit amet dui nunc. Curabitur aliquet, urna non condimentum viverra, nunc leo fermentum orci, quis faucibus sem erat ut nibh. Duis viverra in nisl a interdum. Praesent porta lectus et scelerisque porttitor. Nam a lectus a neque interdum faucibus non sit amet lacus. Aenean vehicula auctor neque. Nam vitae metus sed est laoreet dapibus. Fusce pulvinar tincidunt diam eu iaculis. Nulla blandit egestas arcu vitae malesuada. Aliquam erat volutpat.\n\nAenean iaculis cursus nisl quis sodales. Fusce id nisl id dolor tempor laoreet. Donec ac condimentum enim. Maecenas at pulvinar odio, a vestibulum turpis. Cras ex odio, dictum feugiat sem eget, tristique elementum quam. Vivamus facilisis quam sed hendrerit vestibulum. Sed ac ullamcorper nunc. Praesent maximus ante arcu, id dapibus nulla hendrerit at. Nullam molestie laoreet sem non finibus.\n\nEtiam imperdiet a purus id dignissim. Curabitur vulputate efficitur risus quis dictum. Aliquam iaculis accumsan diam, in sodales lacus bibendum et. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc eget porttitor ex. Quisque vitae leo auctor, auctor augue in, eleifend quam. Nullam a neque nisi. Nunc imperdiet nunc enim, at congue magna sodales ac. Nam risus enim, laoreet eget pulvinar quis, semper ac lacus. Ut elementum nisl metus, sed porttitor urna feugiat malesuada. Etiam euismod, lorem vel dignissim malesuada, nunc arcu interdum diam, vel placerat lorem turpis sed nibh.\n\nDuis aliquam pulvinar libero sit amet volutpat. Curabitur vel orci nec nisl convallis lobortis. Ut eget condimentum dolor. Aenean iaculis nunc non magna euismod imperdiet. Suspendisse tempus lectus nec nulla lobortis blandit. Duis id lorem tempus, aliquam nibh vel, ornare nunc. Sed at ipsum turpis. Duis vel nulla non nibh volutpat dictum. Suspendisse tristique facilisis purus, nec rhoncus turpis tempus at. Donec leo dui, ullamcorper et est in, rhoncus dapibus risus. In finibus sollicitudin felis non facilisis. Nulla cursus mauris quis venenatis elementum. Morbi semper eros nunc, vitae rhoncus lacus vestibulum non. Duis congue maximus ornare.\n\nNam et mauris elementum, aliquam nulla non, venenatis est. Aliquam vitae dui consequat, ornare velit vitae, rutrum dui. Nulla porttitor euismod egestas. Quisque lacinia dignissim sapien nec tempus. Etiam porta malesuada ligula. Mauris non metus lacus. Vivamus sit amet congue risus, at ultricies ante. Sed vulputate auctor suscipit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut nec turpis eu purus efficitur iaculis. Donec a quam sed turpis porta tempus id nec lectus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vivamus venenatis ac nulla ac condimentum. Praesent sapien lorem, dignissim in ultricies sit amet, mollis et nunc. Praesent commodo nibh quis erat bibendum, non porta tellus volutpat. Praesent bibendum elit at mauris aliquam mollis. ",
                title: "Long lines of text",
                icon: InformationBoxIcon.Asterisk,
                fontParameters: new FontParameters(new System.Drawing.Font("Courier New", 12F)),
                titleStyle: InformationBoxTitleIconStyle.SameAsBox);
        }
    }
}
