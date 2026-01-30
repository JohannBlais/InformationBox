using System.Drawing;
using System.Media;
using System.Windows.Forms;
using InfoBox;
using InfoBox.Internals;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    [TestFixture]
    public class InformationBoxViewModelTests
    {
        #region Default Values

        [Test]
        public void DefaultValues_AreCorrect()
        {
            var vm = new InformationBoxViewModel();

            Assert.That(vm.Text, Is.EqualTo(string.Empty));
            Assert.That(vm.Title, Is.EqualTo(string.Empty));
            Assert.That(vm.HelpFile, Is.EqualTo(string.Empty));
            Assert.That(vm.HelpTopic, Is.EqualTo(string.Empty));
            Assert.That(vm.ButtonUser1Text, Is.EqualTo("User1"));
            Assert.That(vm.ButtonUser2Text, Is.EqualTo("User2"));
            Assert.That(vm.ButtonUser3Text, Is.EqualTo("User3"));
            Assert.That(vm.DoNotShowAgainText, Is.Null);
            Assert.That(vm.Buttons, Is.EqualTo(InformationBoxButtons.OK));
            Assert.That(vm.Icon, Is.EqualTo(InformationBoxIcon.None));
            Assert.That(vm.CustomIcon, Is.Null);
            Assert.That(vm.IconType, Is.EqualTo(IconType.Internal));
            Assert.That(vm.DefaultButton, Is.EqualTo(InformationBoxDefaultButton.Button1));
            Assert.That(vm.ButtonsLayout, Is.EqualTo(InformationBoxButtonsLayout.GroupMiddle));
            Assert.That(vm.AutoSizeMode, Is.EqualTo(InformationBoxAutoSizeMode.None));
            Assert.That(vm.Position, Is.EqualTo(InformationBoxPosition.CenterOnParent));
            Assert.That(vm.Style, Is.EqualTo(InformationBoxStyle.Standard));
            Assert.That(vm.Behavior, Is.EqualTo(InformationBoxBehavior.Modal));
            Assert.That(vm.Opacity, Is.EqualTo(InformationBoxOpacity.NoFade));
            Assert.That(vm.Order, Is.EqualTo(InformationBoxOrder.Default));
            Assert.That(vm.Sound, Is.EqualTo(InformationBoxSound.Default));
            Assert.That(vm.ShowHelpButton, Is.False);
            Assert.That(vm.AutoClose, Is.Null);
            Assert.That(vm.Design, Is.Null);
            Assert.That(vm.FontParameters, Is.Null);
            Assert.That(vm.TitleStyle, Is.EqualTo(InformationBoxTitleIconStyle.None));
            Assert.That(vm.TitleIcon, Is.Null);
            Assert.That(vm.Callback, Is.Null);
            Assert.That(vm.Parent, Is.Null);
        }

        #endregion

        #region GetButtonDefinitions

        [Test]
        public void GetButtonDefinitions_OK_ReturnsOneOKButton()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.OK };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(1));
            Assert.That(buttons[0].Name, Is.EqualTo("OK"));
        }

        [Test]
        public void GetButtonDefinitions_OKCancel_ReturnsTwoButtons()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.OKCancel };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(2));
            Assert.That(buttons[0].Name, Is.EqualTo("OK"));
            Assert.That(buttons[1].Name, Is.EqualTo("Cancel"));
        }

        [Test]
        public void GetButtonDefinitions_YesNoCancel_ReturnsThreeButtons()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.YesNoCancel };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(3));
            Assert.That(buttons[0].Name, Is.EqualTo("Yes"));
            Assert.That(buttons[1].Name, Is.EqualTo("No"));
            Assert.That(buttons[2].Name, Is.EqualTo("Cancel"));
        }

        [Test]
        public void GetButtonDefinitions_AbortRetryIgnore_ReturnsThreeButtons()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.AbortRetryIgnore };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(3));
            Assert.That(buttons[0].Name, Is.EqualTo("Abort"));
            Assert.That(buttons[1].Name, Is.EqualTo("Retry"));
            Assert.That(buttons[2].Name, Is.EqualTo("Ignore"));
        }

        [Test]
        public void GetButtonDefinitions_RetryCancel_ReturnsTwoButtons()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.RetryCancel };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(2));
            Assert.That(buttons[0].Name, Is.EqualTo("Retry"));
            Assert.That(buttons[1].Name, Is.EqualTo("Cancel"));
        }

        [Test]
        public void GetButtonDefinitions_YesNo_ReturnsTwoButtons()
        {
            var vm = new InformationBoxViewModel { Buttons = InformationBoxButtons.YesNo };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(2));
            Assert.That(buttons[0].Name, Is.EqualTo("Yes"));
            Assert.That(buttons[1].Name, Is.EqualTo("No"));
        }

        [Test]
        public void GetButtonDefinitions_User1User2User3_ReturnsThreeCustomButtons()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.User1User2User3,
                ButtonUser1Text = "Save",
                ButtonUser2Text = "Discard",
                ButtonUser3Text = "Cancel",
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(3));
            Assert.That(buttons[0].Name, Is.EqualTo("User1"));
            Assert.That(buttons[0].Text, Is.EqualTo("Save"));
            Assert.That(buttons[1].Name, Is.EqualTo("User2"));
            Assert.That(buttons[1].Text, Is.EqualTo("Discard"));
            Assert.That(buttons[2].Name, Is.EqualTo("User3"));
            Assert.That(buttons[2].Text, Is.EqualTo("Cancel"));
        }

        [Test]
        public void GetButtonDefinitions_User1_ReturnsSingleCustomButton()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.User1,
                ButtonUser1Text = "Custom",
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(1));
            Assert.That(buttons[0].Name, Is.EqualTo("User1"));
            Assert.That(buttons[0].Text, Is.EqualTo("Custom"));
        }

        [Test]
        public void GetButtonDefinitions_WithHelpButton_IncludesHelp()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.OK,
                ShowHelpButton = true,
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(2));
            Assert.That(buttons[1].Name, Is.EqualTo("Help"));
        }

        [Test]
        public void GetButtonDefinitions_WithHelpFile_IncludesHelp()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.OK,
                HelpFile = "help.chm",
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(2));
            Assert.That(buttons[1].Name, Is.EqualTo("Help"));
        }

        [Test]
        public void GetButtonDefinitions_OKCancelUser1_ReturnsThreeButtons()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.OKCancelUser1,
                ButtonUser1Text = "Custom",
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(3));
            Assert.That(buttons[0].Name, Is.EqualTo("OK"));
            Assert.That(buttons[1].Name, Is.EqualTo("Cancel"));
            Assert.That(buttons[2].Name, Is.EqualTo("User1"));
            Assert.That(buttons[2].Text, Is.EqualTo("Custom"));
        }

        [Test]
        public void GetButtonDefinitions_YesNoUser1_ReturnsThreeButtons()
        {
            var vm = new InformationBoxViewModel
            {
                Buttons = InformationBoxButtons.YesNoUser1,
                ButtonUser1Text = "Maybe",
            };
            var buttons = vm.GetButtonDefinitions();

            Assert.That(buttons.Length, Is.EqualTo(3));
            Assert.That(buttons[0].Name, Is.EqualTo("Yes"));
            Assert.That(buttons[1].Name, Is.EqualTo("No"));
            Assert.That(buttons[2].Name, Is.EqualTo("User1"));
            Assert.That(buttons[2].Text, Is.EqualTo("Maybe"));
        }

        #endregion

        #region MapButtonNameToResult

        [Test]
        public void MapButtonNameToResult_OK_ReturnsOK()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("OK"), Is.EqualTo(InformationBoxResult.OK));
        }

        [Test]
        public void MapButtonNameToResult_Yes_ReturnsYes()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Yes"), Is.EqualTo(InformationBoxResult.Yes));
        }

        [Test]
        public void MapButtonNameToResult_No_ReturnsNo()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("No"), Is.EqualTo(InformationBoxResult.No));
        }

        [Test]
        public void MapButtonNameToResult_Cancel_ReturnsCancel()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Cancel"), Is.EqualTo(InformationBoxResult.Cancel));
        }

        [Test]
        public void MapButtonNameToResult_Abort_ReturnsAbort()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Abort"), Is.EqualTo(InformationBoxResult.Abort));
        }

        [Test]
        public void MapButtonNameToResult_Retry_ReturnsRetry()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Retry"), Is.EqualTo(InformationBoxResult.Retry));
        }

        [Test]
        public void MapButtonNameToResult_Ignore_ReturnsIgnore()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Ignore"), Is.EqualTo(InformationBoxResult.Ignore));
        }

        [Test]
        public void MapButtonNameToResult_User1_ReturnsUser1()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("User1"), Is.EqualTo(InformationBoxResult.User1));
        }

        [Test]
        public void MapButtonNameToResult_User2_ReturnsUser2()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("User2"), Is.EqualTo(InformationBoxResult.User2));
        }

        [Test]
        public void MapButtonNameToResult_User3_ReturnsUser3()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("User3"), Is.EqualTo(InformationBoxResult.User3));
        }

        [Test]
        public void MapButtonNameToResult_Unknown_ReturnsNone()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.MapButtonNameToResult("Unknown"), Is.EqualTo(InformationBoxResult.None));
        }

        #endregion

        #region IsHelpButton

        [Test]
        public void IsHelpButton_Help_ReturnsTrue()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.IsHelpButton("Help"), Is.True);
        }

        [Test]
        public void IsHelpButton_OK_ReturnsFalse()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.IsHelpButton("OK"), Is.False);
        }

        #endregion

        #region GetOpacityValue

        [Test]
        public void GetOpacityValue_NoFade_Returns1()
        {
            var vm = new InformationBoxViewModel { Opacity = InformationBoxOpacity.NoFade };
            Assert.That(vm.GetOpacityValue(), Is.EqualTo(1.0));
        }

        [Test]
        public void GetOpacityValue_Faded50_Returns05()
        {
            var vm = new InformationBoxViewModel { Opacity = InformationBoxOpacity.Faded50 };
            Assert.That(vm.GetOpacityValue(), Is.EqualTo(0.5));
        }

        [Test]
        public void GetOpacityValue_Faded10_Returns01()
        {
            var vm = new InformationBoxViewModel { Opacity = InformationBoxOpacity.Faded10 };
            Assert.That(vm.GetOpacityValue(), Is.EqualTo(0.1));
        }

        [Test]
        public void GetOpacityValue_Faded90_Returns09()
        {
            var vm = new InformationBoxViewModel { Opacity = InformationBoxOpacity.Faded90 };
            Assert.That(vm.GetOpacityValue(), Is.EqualTo(0.9));
        }

        #endregion

        #region GetSystemSound

        [Test]
        public void GetSystemSound_SoundNone_ReturnsNull()
        {
            var vm = new InformationBoxViewModel { Sound = InformationBoxSound.None };
            Assert.That(vm.GetSystemSound(), Is.Null);
        }

        [Test]
        public void GetSystemSound_UserDefinedIcon_ReturnsBeep()
        {
            var vm = new InformationBoxViewModel
            {
                Sound = InformationBoxSound.Default,
                IconType = IconType.UserDefined,
            };
            Assert.That(vm.GetSystemSound(), Is.EqualTo(SystemSounds.Beep));
        }

        [Test]
        public void GetSystemSound_WarningIcon_ReturnsExclamation()
        {
            var vm = new InformationBoxViewModel
            {
                Sound = InformationBoxSound.Default,
                Icon = InformationBoxIcon.Warning,
            };
            Assert.That(vm.GetSystemSound(), Is.EqualTo(SystemSounds.Exclamation));
        }

        [Test]
        public void GetSystemSound_ErrorIcon_ReturnsHand()
        {
            var vm = new InformationBoxViewModel
            {
                Sound = InformationBoxSound.Default,
                Icon = InformationBoxIcon.Error,
            };
            Assert.That(vm.GetSystemSound(), Is.EqualTo(SystemSounds.Hand));
        }

        [Test]
        public void GetSystemSound_QuestionIcon_ReturnsQuestion()
        {
            var vm = new InformationBoxViewModel
            {
                Sound = InformationBoxSound.Default,
                Icon = InformationBoxIcon.Question,
            };
            Assert.That(vm.GetSystemSound(), Is.EqualTo(SystemSounds.Question));
        }

        [Test]
        public void GetSystemSound_InformationIcon_ReturnsAsterisk()
        {
            var vm = new InformationBoxViewModel
            {
                Sound = InformationBoxSound.Default,
                Icon = InformationBoxIcon.Information,
            };
            Assert.That(vm.GetSystemSound(), Is.EqualTo(SystemSounds.Asterisk));
        }

        #endregion

        #region GetCheckBoxConfiguration

        [Test]
        public void GetCheckBoxConfiguration_NotShown_VisibleFalse()
        {
            var vm = new InformationBoxViewModel { CheckBox = 0 };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.Visible, Is.False);
        }

        [Test]
        public void GetCheckBoxConfiguration_Shown_VisibleTrue()
        {
            var vm = new InformationBoxViewModel { CheckBox = InformationBoxCheckBox.Show };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.Visible, Is.True);
            Assert.That(config.Checked, Is.False);
        }

        [Test]
        public void GetCheckBoxConfiguration_ShowAndChecked_CheckedTrue()
        {
            var vm = new InformationBoxViewModel { CheckBox = InformationBoxCheckBox.Show | InformationBoxCheckBox.Checked };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.Visible, Is.True);
            Assert.That(config.Checked, Is.True);
        }

        [Test]
        public void GetCheckBoxConfiguration_RightAligned_SetsAlignment()
        {
            var vm = new InformationBoxViewModel { CheckBox = InformationBoxCheckBox.Show | InformationBoxCheckBox.RightAligned };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.TextAlign, Is.EqualTo(ContentAlignment.BottomRight));
            Assert.That(config.CheckAlign, Is.EqualTo(ContentAlignment.MiddleRight));
        }

        [Test]
        public void GetCheckBoxConfiguration_LeftAligned_SetsAlignment()
        {
            var vm = new InformationBoxViewModel { CheckBox = InformationBoxCheckBox.Show };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.TextAlign, Is.EqualTo(ContentAlignment.BottomLeft));
            Assert.That(config.CheckAlign, Is.EqualTo(ContentAlignment.MiddleLeft));
        }

        [Test]
        public void GetCheckBoxConfiguration_CustomText_UsesCustomText()
        {
            var vm = new InformationBoxViewModel
            {
                CheckBox = InformationBoxCheckBox.Show,
                DoNotShowAgainText = "Custom text",
            };
            var config = vm.GetCheckBoxConfiguration();

            Assert.That(config.Text, Is.EqualTo("Custom text"));
        }

        #endregion

        #region GetWindowStyleConfiguration

        [Test]
        public void GetWindowStyleConfiguration_Standard_ReturnsFixedDialog()
        {
            var vm = new InformationBoxViewModel { Style = InformationBoxStyle.Standard };
            var config = vm.GetWindowStyleConfiguration();

            Assert.That(config.BorderStyle, Is.EqualTo(FormBorderStyle.FixedDialog));
            Assert.That(config.TitleLabelVisible, Is.False);
            Assert.That(config.AdjustPanelTop, Is.True);
            Assert.That(config.RemoveSideBorder, Is.True);
        }

        [Test]
        public void GetWindowStyleConfiguration_Modern_ReturnsNone()
        {
            var vm = new InformationBoxViewModel { Style = InformationBoxStyle.Modern };
            var config = vm.GetWindowStyleConfiguration();

            Assert.That(config.BorderStyle, Is.EqualTo(FormBorderStyle.None));
            Assert.That(config.TitleLabelVisible, Is.True);
            Assert.That(config.AdjustPanelTop, Is.False);
            Assert.That(config.RemoveSideBorder, Is.False);
        }

        [Test]
        public void GetWindowStyleConfiguration_ModernWithDesign_UsesDesignColors()
        {
            var vm = new InformationBoxViewModel
            {
                Style = InformationBoxStyle.Modern,
                Design = new DesignParameters(Color.DarkBlue, Color.LightGray),
            };
            var config = vm.GetWindowStyleConfiguration();

            Assert.That(config.BarsBackColor, Is.EqualTo(Color.DarkBlue));
            Assert.That(config.FormBackColor, Is.EqualTo(Color.LightGray));
        }

        [Test]
        public void GetWindowStyleConfiguration_ModernWithoutDesign_UsesDefaults()
        {
            var vm = new InformationBoxViewModel { Style = InformationBoxStyle.Modern };
            var config = vm.GetWindowStyleConfiguration();

            Assert.That(config.BarsBackColor, Is.EqualTo(Color.Black));
            Assert.That(config.FormBackColor, Is.EqualTo(Color.Silver));
        }

        [Test]
        public void GetWindowStyleConfiguration_StandardWithDesign_UsesDesignColors()
        {
            var vm = new InformationBoxViewModel
            {
                Style = InformationBoxStyle.Standard,
                Design = new DesignParameters(Color.Navy, Color.Ivory),
            };
            var config = vm.GetWindowStyleConfiguration();

            Assert.That(config.BarsBackColor, Is.EqualTo(Color.Navy));
            Assert.That(config.FormBackColor, Is.EqualTo(Color.Ivory));
        }

        #endregion

        #region HasIcon

        [Test]
        public void HasIcon_NoIcon_ReturnsFalse()
        {
            var vm = new InformationBoxViewModel { Icon = InformationBoxIcon.None, IconType = IconType.Internal };
            Assert.That(vm.HasIcon(), Is.False);
        }

        [Test]
        public void HasIcon_InternalIcon_ReturnsTrue()
        {
            var vm = new InformationBoxViewModel { Icon = InformationBoxIcon.Warning };
            Assert.That(vm.HasIcon(), Is.True);
        }

        [Test]
        public void HasIcon_UserDefinedIcon_ReturnsTrue()
        {
            var vm = new InformationBoxViewModel { IconType = IconType.UserDefined };
            Assert.That(vm.HasIcon(), Is.True);
        }

        #endregion

        #region HasCheckBox

        [Test]
        public void HasCheckBox_NotShown_ReturnsFalse()
        {
            var vm = new InformationBoxViewModel { CheckBox = 0 };
            Assert.That(vm.HasCheckBox(), Is.False);
        }

        [Test]
        public void HasCheckBox_Shown_ReturnsTrue()
        {
            var vm = new InformationBoxViewModel { CheckBox = InformationBoxCheckBox.Show };
            Assert.That(vm.HasCheckBox(), Is.True);
        }

        #endregion

        #region AutoClose

        [Test]
        public void GetAutoCloseButtonIndex_NoAutoClose_ReturnsNegative()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.GetAutoCloseButtonIndex(3), Is.EqualTo(-1));
        }

        [Test]
        public void GetAutoCloseButtonIndex_ButtonMode_ReturnsCorrectIndex()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(10, InformationBoxDefaultButton.Button2),
            };
            Assert.That(vm.GetAutoCloseButtonIndex(3), Is.EqualTo(1));
        }

        [Test]
        public void GetAutoCloseButtonIndex_TimeOnlyMode_UsesDefaultButton()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(10),
                DefaultButton = InformationBoxDefaultButton.Button1,
            };
            Assert.That(vm.GetAutoCloseButtonIndex(3), Is.EqualTo(0));
        }

        [Test]
        public void GetAutoCloseButtonIndex_ButtonExceedsCount_ReturnsNegative()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(10, InformationBoxDefaultButton.Button3),
            };
            Assert.That(vm.GetAutoCloseButtonIndex(2), Is.EqualTo(-1));
        }

        [Test]
        public void ProcessAutoCloseTick_TimeReached_ShouldClose()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(5),
                DefaultButton = InformationBoxDefaultButton.Button1,
            };

            var result = vm.ProcessAutoCloseTick(5, 2);

            Assert.That(result.ShouldClose, Is.True);
            Assert.That(result.ShouldStopTimer, Is.True);
        }

        [Test]
        public void ProcessAutoCloseTick_TimeNotReached_ShouldNotClose()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(10),
                DefaultButton = InformationBoxDefaultButton.Button1,
            };

            var result = vm.ProcessAutoCloseTick(3, 2);

            Assert.That(result.ShouldClose, Is.False);
            Assert.That(result.ShouldStopTimer, Is.False);
        }

        [Test]
        public void ProcessAutoCloseTick_ResultMode_SetsDirectResult()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(5, InformationBoxResult.Yes),
            };

            var result = vm.ProcessAutoCloseTick(5, 2);

            Assert.That(result.ShouldClose, Is.True);
            Assert.That(result.UseDirectResult, Is.True);
            Assert.That(result.DirectResult, Is.EqualTo(InformationBoxResult.Yes));
        }

        [Test]
        public void ProcessAutoCloseTick_NoAutoClose_ReturnsDefault()
        {
            var vm = new InformationBoxViewModel();
            var result = vm.ProcessAutoCloseTick(0, 2);

            Assert.That(result.ShouldClose, Is.False);
            Assert.That(result.ButtonIndex, Is.EqualTo(-1));
        }

        #endregion

        #region FormatAutoCloseButtonText

        [Test]
        public void FormatAutoCloseButtonText_FirstCall_AppendsCountdown()
        {
            var vm = new InformationBoxViewModel();
            var text = vm.FormatAutoCloseButtonText("OK", 10);

            Assert.That(text, Is.EqualTo("OK (10)"));
        }

        [Test]
        public void FormatAutoCloseButtonText_Subsequent_UpdatesCountdown()
        {
            var vm = new InformationBoxViewModel();
            var text = vm.FormatAutoCloseButtonText("OK (10)", 9);

            Assert.That(text, Is.EqualTo("OK (9)"));
        }

        [Test]
        public void FormatAutoCloseButtonText_MultiDigit_UpdatesCorrectly()
        {
            var vm = new InformationBoxViewModel();
            var text = vm.FormatAutoCloseButtonText("OK (100)", 5);

            Assert.That(text, Is.EqualTo("OK (5)"));
        }

        #endregion

        #region GetAutoCloseSecondsRemaining

        [Test]
        public void GetAutoCloseSecondsRemaining_ReturnsCorrectValue()
        {
            var vm = new InformationBoxViewModel
            {
                AutoClose = new AutoCloseParameters(30),
            };

            Assert.That(vm.GetAutoCloseSecondsRemaining(10), Is.EqualTo(20));
        }

        [Test]
        public void GetAutoCloseSecondsRemaining_NoAutoClose_ReturnsZero()
        {
            var vm = new InformationBoxViewModel();
            Assert.That(vm.GetAutoCloseSecondsRemaining(5), Is.EqualTo(0));
        }

        #endregion
    }
}
