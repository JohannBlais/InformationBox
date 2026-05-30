using System.Drawing;
using System.Windows.Forms;
using InfoBox;
using InfoBox.Internals;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    /// <summary>
    /// Unit tests for <see cref="InformationBoxArgsParser"/>. Verifies the loosely-typed
    /// <c>params object[]</c> dispatch behavior without needing to instantiate the form.
    /// </summary>
    [TestFixture]
    public class InformationBoxArgsParserTests
    {
        #region Null / Empty Input

        [Test]
        public void Parse_NullParameters_ReturnsDefaultArgs()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(null);

            // Assert
            Assert.That(args, Is.Not.Null);
            Assert.That(args.LoadScope, Is.True);
            Assert.That(args.Title, Is.Null);
            Assert.That(args.Buttons, Is.Null);
        }

        [Test]
        public void Parse_EmptyArray_ReturnsDefaultArgs()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(System.Array.Empty<object>());

            // Assert
            Assert.That(args.LoadScope, Is.True);
            Assert.That(args.Title, Is.Null);
            Assert.That(args.Icon, Is.Null);
        }

        [Test]
        public void Parse_NullEntryInArray_IsSkipped()
        {
            // Act - null entries are skipped and don't advance the string counter
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { null, "title", null });

            // Assert
            Assert.That(args.Title, Is.EqualTo("title"));
            Assert.That(args.HelpFile, Is.Null);
        }

        [Test]
        public void Parse_UnknownParameter_IsSilentlyIgnored()
        {
            // Act - boxed int and a plain object are unrecognized kinds
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { 42, "title", new object() });

            // Assert - the string still made it through positionally
            Assert.That(args.Title, Is.EqualTo("title"));
        }

        #endregion

        #region String Positional Assignment

        [Test]
        public void Parse_SingleString_SetsTitle()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "My Title" });

            // Assert
            Assert.That(args.Title, Is.EqualTo("My Title"));
            Assert.That(args.HelpFile, Is.Null);
            Assert.That(args.HelpTopic, Is.Null);
            Assert.That(args.DoNotShowAgainText, Is.Null);
        }

        [Test]
        public void Parse_TwoStrings_SetsTitleAndHelpFile()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "title", "help.chm" });

            // Assert
            Assert.That(args.Title, Is.EqualTo("title"));
            Assert.That(args.HelpFile, Is.EqualTo("help.chm"));
            Assert.That(args.HelpTopic, Is.Null);
        }

        [Test]
        public void Parse_ThreeStrings_SetsTitleHelpFileAndHelpTopic()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "title", "help.chm", "topic1" });

            // Assert
            Assert.That(args.Title, Is.EqualTo("title"));
            Assert.That(args.HelpFile, Is.EqualTo("help.chm"));
            Assert.That(args.HelpTopic, Is.EqualTo("topic1"));
            Assert.That(args.DoNotShowAgainText, Is.Null);
        }

        [Test]
        public void Parse_FourStrings_SetsAllPositionalStrings()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "title", "help.chm", "topic1", "Don't ask again" });

            // Assert
            Assert.That(args.Title, Is.EqualTo("title"));
            Assert.That(args.HelpFile, Is.EqualTo("help.chm"));
            Assert.That(args.HelpTopic, Is.EqualTo("topic1"));
            Assert.That(args.DoNotShowAgainText, Is.EqualTo("Don't ask again"));
        }

        [Test]
        public void Parse_FifthAndLaterStrings_AreIgnored()
        {
            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "1", "2", "3", "4", "5", "6" });

            // Assert - only the first four positions are kept
            Assert.That(args.Title, Is.EqualTo("1"));
            Assert.That(args.HelpFile, Is.EqualTo("2"));
            Assert.That(args.HelpTopic, Is.EqualTo("3"));
            Assert.That(args.DoNotShowAgainText, Is.EqualTo("4"));
        }

        [Test]
        public void Parse_StringsInterspersedWithEnums_StringPositionsPreserved()
        {
            // Arrange - enums between strings should not affect string positional counting
            object[] parameters = new object[]
            {
                "title",
                InformationBoxButtons.YesNo,
                "help.chm",
                InformationBoxIcon.Error,
                "topic1",
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.That(args.Title, Is.EqualTo("title"));
            Assert.That(args.HelpFile, Is.EqualTo("help.chm"));
            Assert.That(args.HelpTopic, Is.EqualTo("topic1"));
            Assert.That(args.Buttons, Is.EqualTo(InformationBoxButtons.YesNo));
            Assert.That(args.Icon, Is.EqualTo(InformationBoxIcon.Error));
        }

        #endregion

        #region Enum Dispatch

        [TestCase(InformationBoxButtons.OK)]
        [TestCase(InformationBoxButtons.YesNo)]
        [TestCase(InformationBoxButtons.YesNoCancel)]
        public void Parse_InformationBoxButtons_SetsButtons(InformationBoxButtons value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.Buttons, Is.EqualTo(value));
        }

        [TestCase(InformationBoxIcon.Information)]
        [TestCase(InformationBoxIcon.Warning)]
        [TestCase(InformationBoxIcon.Error)]
        [TestCase(InformationBoxIcon.Question)]
        public void Parse_InformationBoxIcon_SetsIcon(InformationBoxIcon value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.Icon, Is.EqualTo(value));
        }

        [TestCase(InformationBoxDefaultButton.Button1)]
        [TestCase(InformationBoxDefaultButton.Button2)]
        [TestCase(InformationBoxDefaultButton.Button3)]
        public void Parse_InformationBoxDefaultButton_SetsDefaultButton(InformationBoxDefaultButton value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.DefaultButton, Is.EqualTo(value));
        }

        [Test]
        public void Parse_VariousEnums_AllRouteCorrectly()
        {
            // Arrange
            object[] parameters = new object[]
            {
                InformationBoxButtonsLayout.GroupRight,
                InformationBoxAutoSizeMode.MinimumWidth,
                InformationBoxPosition.CenterOnScreen,
                InformationBoxCheckBox.Show,
                InformationBoxStyle.Modern,
                InformationBoxBehavior.Modal,
                InformationBoxOpacity.Faded30,
                InformationBoxOrder.TopMost,
                InformationBoxSound.Default,
                InformationBoxTitleIconStyle.Custom,
                HelpNavigator.TableOfContents,
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.That(args.ButtonsLayout, Is.EqualTo(InformationBoxButtonsLayout.GroupRight));
            Assert.That(args.AutoSizeMode, Is.EqualTo(InformationBoxAutoSizeMode.MinimumWidth));
            Assert.That(args.Position, Is.EqualTo(InformationBoxPosition.CenterOnScreen));
            Assert.That(args.CheckBox, Is.EqualTo(InformationBoxCheckBox.Show));
            Assert.That(args.Style, Is.EqualTo(InformationBoxStyle.Modern));
            Assert.That(args.Behavior, Is.EqualTo(InformationBoxBehavior.Modal));
            Assert.That(args.Opacity, Is.EqualTo(InformationBoxOpacity.Faded30));
            Assert.That(args.Order, Is.EqualTo(InformationBoxOrder.TopMost));
            Assert.That(args.Sound, Is.EqualTo(InformationBoxSound.Default));
            Assert.That(args.TitleStyle, Is.EqualTo(InformationBoxTitleIconStyle.Custom));
            Assert.That(args.HelpNavigator, Is.EqualTo(HelpNavigator.TableOfContents));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Parse_BoolParameter_SetsShowHelpButton(bool value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.ShowHelpButton, Is.EqualTo(value));
        }

        #endregion

        #region MessageBox Enum Conversion

        [TestCase(MessageBoxButtons.OKCancel)]
        [TestCase(MessageBoxButtons.YesNo)]
        public void Parse_MessageBoxButtons_ConvertsToInformationBoxButtons(MessageBoxButtons value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.Buttons.HasValue, Is.True, "MessageBoxButtons should be routed through MessageBoxEnumConverter");
        }

        [TestCase(MessageBoxIcon.Error)]
        [TestCase(MessageBoxIcon.Warning)]
        public void Parse_MessageBoxIcon_ConvertsToInformationBoxIcon(MessageBoxIcon value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.Icon.HasValue, Is.True);
        }

        [TestCase(MessageBoxDefaultButton.Button1)]
        [TestCase(MessageBoxDefaultButton.Button2)]
        public void Parse_MessageBoxDefaultButton_ConvertsToInformationBoxDefaultButton(MessageBoxDefaultButton value)
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { value });
            Assert.That(args.DefaultButton.HasValue, Is.True);
        }

        #endregion

        #region Multi-Field Dispatch

        [Test]
        public void Parse_CustomIcon_StoresIconReference()
        {
            // Arrange
            Icon icon = SystemIcons.Information;

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { icon });

            // Assert - parser captures the icon; form applies iconType=UserDefined on it
            Assert.That(args.CustomIcon, Is.SameAs(icon));
        }

        [Test]
        public void Parse_StringArray_PopulatesAllThreeButtonLabels()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[]
            {
                new[] { "Save", "Discard", "Cancel" },
            });

            Assert.That(args.ButtonUser1Text, Is.EqualTo("Save"));
            Assert.That(args.ButtonUser2Text, Is.EqualTo("Discard"));
            Assert.That(args.ButtonUser3Text, Is.EqualTo("Cancel"));
        }

        [Test]
        public void Parse_StringArrayWithTwoElements_OnlyFirstTwoButtonsSet()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[]
            {
                new[] { "Save", "Cancel" },
            });

            Assert.That(args.ButtonUser1Text, Is.EqualTo("Save"));
            Assert.That(args.ButtonUser2Text, Is.EqualTo("Cancel"));
            Assert.That(args.ButtonUser3Text, Is.Null);
        }

        [Test]
        public void Parse_EmptyStringArray_LeavesAllButtonLabelsNull()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { System.Array.Empty<string>() });

            Assert.That(args.ButtonUser1Text, Is.Null);
            Assert.That(args.ButtonUser2Text, Is.Null);
            Assert.That(args.ButtonUser3Text, Is.Null);
        }

        [Test]
        public void Parse_Font_WrapsInFontParameters()
        {
            // Arrange
            Font font = SystemFonts.MessageBoxFont;

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { font });

            // Assert
            Assert.That(args.FontParameters, Is.Not.Null);
        }

        [Test]
        public void Parse_FontParametersDirectly_StoresReference()
        {
            // Arrange
            FontParameters fp = new FontParameters(SystemFonts.MessageBoxFont);

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { fp });

            // Assert
            Assert.That(args.FontParameters, Is.SameAs(fp));
        }

        [Test]
        public void Parse_InformationBoxTitleIcon_ExtractsInnerIcon()
        {
            // Arrange
            Icon innerIcon = SystemIcons.Information;
            using InformationBoxTitleIcon wrapper = new InformationBoxTitleIcon(innerIcon);

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { wrapper });

            // Assert
            Assert.That(args.TitleIcon, Is.SameAs(innerIcon));
        }

        [Test]
        public void Parse_AutoCloseParameters_StoresReference()
        {
            // Arrange
            AutoCloseParameters ac = AutoCloseParameters.Default;

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { ac });

            // Assert
            Assert.That(args.AutoClose, Is.SameAs(ac));
        }

        [Test]
        public void Parse_DesignParameters_StoresReference()
        {
            // Arrange
            DesignParameters dp = new DesignParameters(Color.White, Color.Black);

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { dp });

            // Assert
            Assert.That(args.Design, Is.SameAs(dp));
        }

        #endregion

        #region Scope Handling

        [Test]
        public void Parse_NoInitializationParameter_LoadScopeIsTrue()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { "title" });
            Assert.That(args.LoadScope, Is.True);
        }

        [Test]
        public void Parse_FromParametersOnly_DisablesScopeLoading()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { InformationBoxInitialization.FromParametersOnly });
            Assert.That(args.LoadScope, Is.False);
        }

        [Test]
        public void Parse_FromScopeAndParameters_KeepsScopeLoading()
        {
            InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { InformationBoxInitialization.FromScopeAndParameters });
            Assert.That(args.LoadScope, Is.True);
        }

        [Test]
        public void Parse_FromParametersOnly_DetectedRegardlessOfPosition()
        {
            // Arrange - FromParametersOnly is detected in a separate pass; position should not matter
            object[] parameters = new object[]
            {
                "title",
                InformationBoxButtons.OK,
                InformationBoxInitialization.FromParametersOnly,
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.That(args.LoadScope, Is.False);
        }

        #endregion

        #region Last-Wins Ordering

        [Test]
        public void Parse_DuplicateButtons_LastValueWins()
        {
            // Arrange - the historical dispatcher overwrote earlier values; tests preserve this
            object[] parameters = new object[]
            {
                InformationBoxButtons.OK,
                InformationBoxButtons.YesNoCancel,
                InformationBoxButtons.YesNo,
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.That(args.Buttons, Is.EqualTo(InformationBoxButtons.YesNo));
        }

        [Test]
        public void Parse_DuplicateIcon_LastValueWins()
        {
            object[] parameters = new object[]
            {
                InformationBoxIcon.Information,
                InformationBoxIcon.Error,
            };
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);
            Assert.That(args.Icon, Is.EqualTo(InformationBoxIcon.Error));
        }

        #endregion

        #region Form Parent (regression test for the bug fixed in PR #79)

        [Test]
        public void Parse_FormParameter_StoresParentReference()
        {
            // Regression test: before PR #79 the dispatcher's `Form` branch was a silent
            // no-op (it assigned `(Form)Parent` instead of the local). The parser must now
            // actually capture the supplied form.
            Form form = new Form();
            try
            {
                InformationBoxArgs args = InformationBoxArgsParser.Parse(new object[] { form });
                Assert.That(args.Parent, Is.SameAs(form));
            }
            finally
            {
                form.Dispose();
            }
        }

        #endregion

        #region Realistic Multi-Parameter Calls

        [Test]
        public void Parse_TypicalSaveDialog_PopulatesExpectedFields()
        {
            // Arrange - mirrors what InformationBox.Show("Save?", YesNoCancel, Question, Button2) would pass
            object[] parameters = new object[]
            {
                "Save changes?",
                InformationBoxButtons.YesNoCancel,
                InformationBoxIcon.Question,
                InformationBoxDefaultButton.Button2,
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.That(args.Title, Is.EqualTo("Save changes?"));
            Assert.That(args.Buttons, Is.EqualTo(InformationBoxButtons.YesNoCancel));
            Assert.That(args.Icon, Is.EqualTo(InformationBoxIcon.Question));
            Assert.That(args.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button2));
            // Unspecified properties remain null
            Assert.That(args.AutoClose, Is.Null);
            Assert.That(args.Style, Is.Null);
        }

        [Test]
        public void Parse_FullKitchenSink_AllFieldsPopulated()
        {
            // Arrange - a call exercising most of the dispatcher branches at once
            Icon customIcon = SystemIcons.Information;
            object[] parameters = new object[]
            {
                "Title",
                "help.chm",
                "topic42",
                "Hide me",
                InformationBoxButtons.AbortRetryIgnore,
                customIcon,
                InformationBoxDefaultButton.Button3,
                new[] { "A", "B", "C" },
                InformationBoxButtonsLayout.GroupRight,
                InformationBoxAutoSizeMode.MinimumHeight,
                InformationBoxPosition.CenterOnScreen,
                true,
                HelpNavigator.Topic,
                InformationBoxCheckBox.Show,
                InformationBoxStyle.Modern,
                InformationBoxTitleIconStyle.SameAsBox,
                InformationBoxBehavior.Modal,
                InformationBoxOpacity.Faded30,
                InformationBoxOrder.TopMost,
                InformationBoxSound.Default,
            };

            // Act
            InformationBoxArgs args = InformationBoxArgsParser.Parse(parameters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(args.Title, Is.EqualTo("Title"));
                Assert.That(args.HelpFile, Is.EqualTo("help.chm"));
                Assert.That(args.HelpTopic, Is.EqualTo("topic42"));
                Assert.That(args.DoNotShowAgainText, Is.EqualTo("Hide me"));
                Assert.That(args.Buttons, Is.EqualTo(InformationBoxButtons.AbortRetryIgnore));
                Assert.That(args.CustomIcon, Is.SameAs(customIcon));
                Assert.That(args.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button3));
                Assert.That(args.ButtonUser1Text, Is.EqualTo("A"));
                Assert.That(args.ButtonUser2Text, Is.EqualTo("B"));
                Assert.That(args.ButtonUser3Text, Is.EqualTo("C"));
                Assert.That(args.ButtonsLayout, Is.EqualTo(InformationBoxButtonsLayout.GroupRight));
                Assert.That(args.AutoSizeMode, Is.EqualTo(InformationBoxAutoSizeMode.MinimumHeight));
                Assert.That(args.Position, Is.EqualTo(InformationBoxPosition.CenterOnScreen));
                Assert.That(args.ShowHelpButton, Is.True);
                Assert.That(args.HelpNavigator, Is.EqualTo(HelpNavigator.Topic));
                Assert.That(args.CheckBox, Is.EqualTo(InformationBoxCheckBox.Show));
                Assert.That(args.Style, Is.EqualTo(InformationBoxStyle.Modern));
                Assert.That(args.TitleStyle, Is.EqualTo(InformationBoxTitleIconStyle.SameAsBox));
                Assert.That(args.Behavior, Is.EqualTo(InformationBoxBehavior.Modal));
                Assert.That(args.Opacity, Is.EqualTo(InformationBoxOpacity.Faded30));
                Assert.That(args.Order, Is.EqualTo(InformationBoxOrder.TopMost));
                Assert.That(args.Sound, Is.EqualTo(InformationBoxSound.Default));
                Assert.That(args.LoadScope, Is.True);
            });
        }

        #endregion
    }
}
