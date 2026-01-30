using System.Drawing;
using System.Windows.Forms;
using InfoBox;
using InfoBox.Internals;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    [TestFixture]
    public class ParameterParserTests
    {
        #region Basic Parsing

        [Test]
        public void Parse_WithTextOnly_SetsTextAndDefaults()
        {
            var vm = ParameterParser.Parse("Hello");

            Assert.That(vm.Text, Is.EqualTo("Hello"));
            Assert.That(vm.Title, Is.EqualTo(string.Empty));
            Assert.That(vm.Buttons, Is.EqualTo(InformationBoxButtons.OK));
            Assert.That(vm.Icon, Is.EqualTo(InformationBoxIcon.None));
            Assert.That(vm.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button1));
            Assert.That(vm.ButtonsLayout, Is.EqualTo(InformationBoxButtonsLayout.GroupMiddle));
            Assert.That(vm.AutoSizeMode, Is.EqualTo(InformationBoxAutoSizeMode.None));
            Assert.That(vm.Position, Is.EqualTo(InformationBoxPosition.CenterOnParent));
            Assert.That(vm.Style, Is.EqualTo(InformationBoxStyle.Standard));
            Assert.That(vm.Behavior, Is.EqualTo(InformationBoxBehavior.Modal));
            Assert.That(vm.Opacity, Is.EqualTo(InformationBoxOpacity.NoFade));
            Assert.That(vm.Order, Is.EqualTo(InformationBoxOrder.Default));
            Assert.That(vm.Sound, Is.EqualTo(InformationBoxSound.Default));
        }

        [Test]
        public void Parse_NullParameters_AreSkipped()
        {
            var vm = ParameterParser.Parse("Hello", null, "Title");

            Assert.That(vm.Text, Is.EqualTo("Hello"));
            Assert.That(vm.Title, Is.EqualTo("Title"));
        }

        #endregion

        #region String Parameters

        [Test]
        public void Parse_FirstString_SetsTitleAndLblTitle()
        {
            var vm = ParameterParser.Parse("Hello", "My Title");

            Assert.That(vm.Title, Is.EqualTo("My Title"));
        }

        [Test]
        public void Parse_SecondString_SetsHelpFile()
        {
            var vm = ParameterParser.Parse("Hello", "Title", "help.chm");

            Assert.That(vm.HelpFile, Is.EqualTo("help.chm"));
        }

        [Test]
        public void Parse_ThirdString_SetsHelpTopic()
        {
            var vm = ParameterParser.Parse("Hello", "Title", "help.chm", "topic1");

            Assert.That(vm.HelpTopic, Is.EqualTo("topic1"));
        }

        [Test]
        public void Parse_FourthString_SetsDoNotShowAgainText()
        {
            var vm = ParameterParser.Parse("Hello", "Title", "help.chm", "topic1", "Don't show");

            Assert.That(vm.DoNotShowAgainText, Is.EqualTo("Don't show"));
        }

        #endregion

        #region Enum Parameters

        [Test]
        public void Parse_InformationBoxButtons_SetsButtons()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxButtons.YesNoCancel);

            Assert.That(vm.Buttons, Is.EqualTo(InformationBoxButtons.YesNoCancel));
        }

        [Test]
        public void Parse_InformationBoxIcon_SetsIcon()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxIcon.Warning);

            Assert.That(vm.Icon, Is.EqualTo(InformationBoxIcon.Warning));
        }

        [Test]
        public void Parse_InformationBoxDefaultButton_SetsDefaultButton()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxDefaultButton.Button2);

            Assert.That(vm.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button2));
        }

        [Test]
        public void Parse_InformationBoxButtonsLayout_SetsLayout()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxButtonsLayout.GroupRight);

            Assert.That(vm.ButtonsLayout, Is.EqualTo(InformationBoxButtonsLayout.GroupRight));
        }

        [Test]
        public void Parse_InformationBoxAutoSizeMode_SetsAutoSizeMode()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxAutoSizeMode.MinimumWidth);

            Assert.That(vm.AutoSizeMode, Is.EqualTo(InformationBoxAutoSizeMode.MinimumWidth));
        }

        [Test]
        public void Parse_InformationBoxPosition_SetsPosition()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxPosition.CenterOnScreen);

            Assert.That(vm.Position, Is.EqualTo(InformationBoxPosition.CenterOnScreen));
        }

        [Test]
        public void Parse_InformationBoxCheckBox_SetsCheckBox()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxCheckBox.Show);

            Assert.That(vm.CheckBox, Is.EqualTo(InformationBoxCheckBox.Show));
        }

        [Test]
        public void Parse_InformationBoxStyle_SetsStyle()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxStyle.Modern);

            Assert.That(vm.Style, Is.EqualTo(InformationBoxStyle.Modern));
        }

        [Test]
        public void Parse_InformationBoxBehavior_SetsBehavior()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxBehavior.Modeless);

            Assert.That(vm.Behavior, Is.EqualTo(InformationBoxBehavior.Modeless));
        }

        [Test]
        public void Parse_InformationBoxOpacity_SetsOpacity()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxOpacity.Faded50);

            Assert.That(vm.Opacity, Is.EqualTo(InformationBoxOpacity.Faded50));
        }

        [Test]
        public void Parse_InformationBoxOrder_SetsOrder()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxOrder.TopMost);

            Assert.That(vm.Order, Is.EqualTo(InformationBoxOrder.TopMost));
        }

        [Test]
        public void Parse_InformationBoxSound_SetsSound()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxSound.None);

            Assert.That(vm.Sound, Is.EqualTo(InformationBoxSound.None));
        }

        [Test]
        public void Parse_InformationBoxTitleIconStyle_SetsTitleStyle()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxTitleIconStyle.SameAsBox);

            Assert.That(vm.TitleStyle, Is.EqualTo(InformationBoxTitleIconStyle.SameAsBox));
        }

        [Test]
        public void Parse_BoolTrue_SetsShowHelpButton()
        {
            var vm = ParameterParser.Parse("Hello", true);

            Assert.That(vm.ShowHelpButton, Is.True);
        }

        [Test]
        public void Parse_HelpNavigator_SetsHelpNavigator()
        {
            var vm = ParameterParser.Parse("Hello", HelpNavigator.Index);

            Assert.That(vm.HelpNavigator, Is.EqualTo(HelpNavigator.Index));
        }

        #endregion

        #region Complex Parameters

        [Test]
        public void Parse_CustomIcon_SetsIconTypeAndCustomIcon()
        {
            var icon = SystemIcons.Application;
            var vm = ParameterParser.Parse("Hello", icon);

            Assert.That(vm.IconType, Is.EqualTo(IconType.UserDefined));
            Assert.That(vm.CustomIcon, Is.SameAs(icon));
        }

        [Test]
        public void Parse_CustomButtons_SetsButtonTexts()
        {
            // string[] must be boxed as object to avoid params expansion
            var vm = ParameterParser.Parse("Hello", (object)new string[] { "Save", "Discard", "Cancel" });

            Assert.That(vm.ButtonUser1Text, Is.EqualTo("Save"));
            Assert.That(vm.ButtonUser2Text, Is.EqualTo("Discard"));
            Assert.That(vm.ButtonUser3Text, Is.EqualTo("Cancel"));
        }

        [Test]
        public void Parse_CustomButtons_OneButton_SetsOnlyFirst()
        {
            // string[] must be boxed as object to avoid params expansion
            var vm = ParameterParser.Parse("Hello", (object)new string[] { "Only" });

            Assert.That(vm.ButtonUser1Text, Is.EqualTo("Only"));
            Assert.That(vm.ButtonUser2Text, Is.EqualTo("User2"));
            Assert.That(vm.ButtonUser3Text, Is.EqualTo("User3"));
        }

        [Test]
        public void Parse_AutoCloseParameters_SetsAutoClose()
        {
            var autoClose = new AutoCloseParameters(10);
            var vm = ParameterParser.Parse("Hello", autoClose);

            Assert.That(vm.AutoClose, Is.SameAs(autoClose));
        }

        [Test]
        public void Parse_DesignParameters_SetsDesign()
        {
            var design = new DesignParameters(Color.Red, Color.Blue);
            var vm = ParameterParser.Parse("Hello", design);

            Assert.That(vm.Design, Is.SameAs(design));
        }

        [Test]
        public void Parse_FontAsParameter_CreatesFontParameters()
        {
            var font = new Font("Arial", 12);
            var vm = ParameterParser.Parse("Hello", font);

            Assert.That(vm.FontParameters, Is.Not.Null);
            Assert.That(vm.FontParameters.HasFont(), Is.True);
        }

        [Test]
        public void Parse_AsyncResultCallback_SetsCallback()
        {
            AsyncResultCallback callback = (result) => { };
            var vm = ParameterParser.Parse("Hello", callback);

            Assert.That(vm.Callback, Is.SameAs(callback));
        }

        #endregion

        #region Multiple Parameters

        [Test]
        public void Parse_MultipleParameters_SetsAll()
        {
            var vm = ParameterParser.Parse("Hello",
                "Title",
                InformationBoxButtons.YesNo,
                InformationBoxIcon.Question,
                InformationBoxDefaultButton.Button2,
                InformationBoxStyle.Modern);

            Assert.That(vm.Title, Is.EqualTo("Title"));
            Assert.That(vm.Buttons, Is.EqualTo(InformationBoxButtons.YesNo));
            Assert.That(vm.Icon, Is.EqualTo(InformationBoxIcon.Question));
            Assert.That(vm.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button2));
            Assert.That(vm.Style, Is.EqualTo(InformationBoxStyle.Modern));
        }

        #endregion

        #region Initialization Parameter

        [Test]
        public void Parse_FromParametersOnly_SkipsScope()
        {
            var vm = ParameterParser.Parse("Hello", InformationBoxInitialization.FromParametersOnly);

            Assert.That(vm.Text, Is.EqualTo("Hello"));
        }

        #endregion

        #region ParseNamed

        [Test]
        public void ParseNamed_WithAllDefaults_ReturnsDefaultViewModel()
        {
            var vm = ParameterParser.ParseNamed("Hello");

            Assert.That(vm.Text, Is.EqualTo("Hello"));
            Assert.That(vm.Title, Is.EqualTo(string.Empty));
            Assert.That(vm.Buttons, Is.EqualTo(InformationBoxButtons.OK));
            Assert.That(vm.Icon, Is.EqualTo(InformationBoxIcon.None));
        }

        [Test]
        public void ParseNamed_WithTitle_SetsTitle()
        {
            var vm = ParameterParser.ParseNamed("Hello", title: "My Title");

            Assert.That(vm.Title, Is.EqualTo("My Title"));
        }

        [Test]
        public void ParseNamed_WithFont_CreatesFontParameters()
        {
            var font = new Font("Arial", 14);
            var vm = ParameterParser.ParseNamed("Hello", font: font);

            Assert.That(vm.FontParameters, Is.Not.Null);
            Assert.That(vm.FontParameters.HasFont(), Is.True);
        }

        [Test]
        public void ParseNamed_WithCustomIcon_SetsIconType()
        {
            var icon = SystemIcons.Warning;
            var vm = ParameterParser.ParseNamed("Hello", customIcon: icon);

            Assert.That(vm.IconType, Is.EqualTo(IconType.UserDefined));
            Assert.That(vm.CustomIcon, Is.SameAs(icon));
        }

        [Test]
        public void ParseNamed_WithCustomButtons_SetsButtonTexts()
        {
            var vm = ParameterParser.ParseNamed("Hello", customButtons: new[] { "A", "B" });

            Assert.That(vm.ButtonUser1Text, Is.EqualTo("A"));
            Assert.That(vm.ButtonUser2Text, Is.EqualTo("B"));
        }

        #endregion
    }
}
