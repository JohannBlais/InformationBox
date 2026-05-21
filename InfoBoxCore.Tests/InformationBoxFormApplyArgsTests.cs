using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using InfoBox;
using InfoBox.Internals;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    /// <summary>
    /// Tests covering <see cref="InformationBoxForm.ApplyArgs"/> - the projection of an
    /// <see cref="InformationBoxArgs"/> onto a form's private fields.
    /// </summary>
    /// <remarks>
    /// Two pivot tests cover both branches of every <c>if</c> in <c>ApplyArgs</c>:
    /// one with a fully populated <see cref="InformationBoxArgs"/> (exercises the
    /// "value supplied" path) and one with an empty <see cref="InformationBoxArgs"/>
    /// (exercises the "value absent" path, asserting the form's defaults are preserved).
    /// Two extra tests target the non-trivial double-assignment cases (<c>Title</c>
    /// sets both <c>Form.Text</c> and the title label; <c>CustomIcon</c> flips
    /// <c>iconType</c> to <see cref="IconType.UserDefined"/>).
    /// Runs on an STA thread because the form's <see cref="Form.InitializeComponent"/>
    /// instantiates real WinForms controls.
    /// </remarks>
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class InformationBoxFormApplyArgsTests
    {
        #region Reflection helper

        private static T GetField<T>(InformationBoxForm form, string fieldName)
        {
            FieldInfo field = typeof(InformationBoxForm).GetField(
                fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field is null)
            {
                throw new InvalidOperationException(
                    $"Field '{fieldName}' not found on InformationBoxForm");
            }

            return (T)field.GetValue(form);
        }

        #endregion

        [Test]
        public void ApplyArgs_WithFullyPopulatedArgs_AppliesEveryField()
        {
            // Arrange
            using InformationBoxForm form = new InformationBoxForm("base text");
            using Form parentForm = new Form();

            AsyncResultCallback callback = _ => { };
            Icon customIcon = SystemIcons.Information;
            Icon titleIconValue = SystemIcons.Warning;
            FontParameters fontParams = new FontParameters(SystemFonts.MessageBoxFont);
            AutoCloseParameters autoClose = AutoCloseParameters.Default;
            DesignParameters design = new DesignParameters(Color.White, Color.Black);

            InformationBoxArgs args = new InformationBoxArgs
            {
                Title = "applied title",
                HelpFile = "myhelp.chm",
                HelpTopic = "topic42",
                DoNotShowAgainText = "Hide me",
                Buttons = InformationBoxButtons.AbortRetryIgnore,
                Icon = InformationBoxIcon.Error,
                CustomIcon = customIcon,
                DefaultButton = InformationBoxDefaultButton.Button3,
                ButtonUser1Text = "A",
                ButtonUser2Text = "B",
                ButtonUser3Text = "C",
                ButtonsLayout = InformationBoxButtonsLayout.GroupRight,
                AutoSizeMode = InformationBoxAutoSizeMode.MinimumHeight,
                Position = InformationBoxPosition.CenterOnScreen,
                ShowHelpButton = true,
                HelpNavigator = HelpNavigator.Topic,
                CheckBox = InformationBoxCheckBox.Show,
                Style = InformationBoxStyle.Modern,
                AutoClose = autoClose,
                Design = design,
                FontParameters = fontParams,
                TitleStyle = InformationBoxTitleIconStyle.Custom,
                TitleIcon = titleIconValue,
                Behavior = InformationBoxBehavior.Modeless,
                Callback = callback,
                Opacity = InformationBoxOpacity.Faded30,
                Parent = parentForm,
                Order = InformationBoxOrder.TopMost,
                Sound = InformationBoxSound.None,
            };

            // Act - exercises the "value supplied" branch of every conditional in ApplyArgs
            form.ApplyArgs(args);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(form.Text, Is.EqualTo("applied title"));
                Assert.That(GetField<Control>(form, "lblTitle").Text, Is.EqualTo("applied title"));
                Assert.That(GetField<string>(form, "helpFile"), Is.EqualTo("myhelp.chm"));
                Assert.That(GetField<string>(form, "helpTopic"), Is.EqualTo("topic42"));
                Assert.That(GetField<string>(form, "doNotShowAgainText"), Is.EqualTo("Hide me"));
                Assert.That(GetField<InformationBoxButtons>(form, "buttons"), Is.EqualTo(InformationBoxButtons.AbortRetryIgnore));
                Assert.That(GetField<InformationBoxIcon>(form, "icon"), Is.EqualTo(InformationBoxIcon.Error));
                Assert.That(GetField<IconType>(form, "iconType"), Is.EqualTo(IconType.UserDefined));
                Assert.That(GetField<Icon>(form, "customIcon"), Is.SameAs(customIcon));
                Assert.That(GetField<InformationBoxDefaultButton>(form, "defaultButton"), Is.EqualTo(InformationBoxDefaultButton.Button3));
                Assert.That(GetField<string>(form, "buttonUser1Text"), Is.EqualTo("A"));
                Assert.That(GetField<string>(form, "buttonUser2Text"), Is.EqualTo("B"));
                Assert.That(GetField<string>(form, "buttonUser3Text"), Is.EqualTo("C"));
                Assert.That(GetField<InformationBoxButtonsLayout>(form, "buttonsLayout"), Is.EqualTo(InformationBoxButtonsLayout.GroupRight));
                Assert.That(GetField<InformationBoxAutoSizeMode>(form, "autoSizeMode"), Is.EqualTo(InformationBoxAutoSizeMode.MinimumHeight));
                Assert.That(GetField<InformationBoxPosition>(form, "position"), Is.EqualTo(InformationBoxPosition.CenterOnScreen));
                Assert.That(GetField<bool>(form, "showHelpButton"), Is.True);
                Assert.That(GetField<HelpNavigator>(form, "helpNavigator"), Is.EqualTo(HelpNavigator.Topic));
                Assert.That(GetField<InformationBoxCheckBox>(form, "checkBox"), Is.EqualTo(InformationBoxCheckBox.Show));
                Assert.That(GetField<InformationBoxStyle>(form, "style"), Is.EqualTo(InformationBoxStyle.Modern));
                Assert.That(GetField<AutoCloseParameters>(form, "autoClose"), Is.SameAs(autoClose));
                Assert.That(GetField<DesignParameters>(form, "design"), Is.SameAs(design));
                Assert.That(GetField<FontParameters>(form, "fontParameters"), Is.SameAs(fontParams));
                Assert.That(GetField<InformationBoxTitleIconStyle>(form, "titleStyle"), Is.EqualTo(InformationBoxTitleIconStyle.Custom));
                Assert.That(GetField<Icon>(form, "titleIcon"), Is.SameAs(titleIconValue));
                Assert.That(GetField<InformationBoxBehavior>(form, "behavior"), Is.EqualTo(InformationBoxBehavior.Modeless));
                Assert.That(GetField<AsyncResultCallback>(form, "callback"), Is.SameAs(callback));
                Assert.That(GetField<InformationBoxOpacity>(form, "opacity"), Is.EqualTo(InformationBoxOpacity.Faded30));
                Assert.That(form.Owner, Is.SameAs(parentForm), "Parent argument is wired to Form.Owner (not Form.Parent, which rejects top-level Forms)");
                Assert.That(form.Parent, Is.Null, "Form.Parent must remain null - it would throw if assigned");
                Assert.That(GetField<InformationBoxOrder>(form, "order"), Is.EqualTo(InformationBoxOrder.TopMost));
                Assert.That(GetField<InformationBoxSound>(form, "sound"), Is.EqualTo(InformationBoxSound.None));
            });
        }

        [Test]
        public void ApplyArgs_WithEmptyArgs_LeavesFormStateUnchanged()
        {
            // Arrange - snapshot a representative subset of fields before the call
            using InformationBoxForm form = new InformationBoxForm("base text");

            string initialText = form.Text;
            string initialLblTitleText = GetField<Control>(form, "lblTitle").Text;
            string initialHelpFile = GetField<string>(form, "helpFile");
            string initialHelpTopic = GetField<string>(form, "helpTopic");
            InformationBoxButtons initialButtons = GetField<InformationBoxButtons>(form, "buttons");
            InformationBoxIcon initialIcon = GetField<InformationBoxIcon>(form, "icon");
            IconType initialIconType = GetField<IconType>(form, "iconType");
            Icon initialCustomIcon = GetField<Icon>(form, "customIcon");
            InformationBoxStyle initialStyle = GetField<InformationBoxStyle>(form, "style");
            InformationBoxBehavior initialBehavior = GetField<InformationBoxBehavior>(form, "behavior");
            AsyncResultCallback initialCallback = GetField<AsyncResultCallback>(form, "callback");
            Control initialParent = form.Parent;

            // Act - exercises the "value absent" branch of every conditional in ApplyArgs
            form.ApplyArgs(new InformationBoxArgs());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(form.Text, Is.EqualTo(initialText));
                Assert.That(GetField<Control>(form, "lblTitle").Text, Is.EqualTo(initialLblTitleText));
                Assert.That(GetField<string>(form, "helpFile"), Is.EqualTo(initialHelpFile));
                Assert.That(GetField<string>(form, "helpTopic"), Is.EqualTo(initialHelpTopic));
                Assert.That(GetField<InformationBoxButtons>(form, "buttons"), Is.EqualTo(initialButtons));
                Assert.That(GetField<InformationBoxIcon>(form, "icon"), Is.EqualTo(initialIcon));
                Assert.That(GetField<IconType>(form, "iconType"), Is.EqualTo(initialIconType));
                Assert.That(GetField<Icon>(form, "customIcon"), Is.SameAs(initialCustomIcon));
                Assert.That(GetField<InformationBoxStyle>(form, "style"), Is.EqualTo(initialStyle));
                Assert.That(GetField<InformationBoxBehavior>(form, "behavior"), Is.EqualTo(initialBehavior));
                Assert.That(GetField<AsyncResultCallback>(form, "callback"), Is.SameAs(initialCallback));
                Assert.That(form.Parent, Is.SameAs(initialParent));
            });
        }

        [Test]
        public void ApplyArgs_WithTitleOnly_SetsBothFormTextAndTitleLabel()
        {
            // Arrange - the Title branch is the only one that touches two distinct UI fields
            using InformationBoxForm form = new InformationBoxForm("base");
            InformationBoxArgs args = new InformationBoxArgs { Title = "new title" };

            // Act
            form.ApplyArgs(args);

            // Assert
            Assert.That(form.Text, Is.EqualTo("new title"));
            Assert.That(GetField<Control>(form, "lblTitle").Text, Is.EqualTo("new title"));
        }

        [Test]
        public void ApplyArgs_WithParent_SetsOwnerNotParent()
        {
            // Regression guard: an earlier shape of this code set this.Parent = args.Parent,
            // which throws ArgumentException at runtime because a top-level Form cannot be
            // a child control of another control. Form.Owner is the correct WinForms API
            // for "this dialog is owned by that form".
            using InformationBoxForm form = new InformationBoxForm("base");
            using Form parentForm = new Form();
            InformationBoxArgs args = new InformationBoxArgs { Parent = parentForm };

            form.ApplyArgs(args);

            Assert.That(form.Owner, Is.SameAs(parentForm));
            Assert.That(form.Parent, Is.Null);
        }

        [Test]
        public void ApplyArgs_WithCustomIcon_AlsoFlipsIconTypeToUserDefined()
        {
            // Arrange - the CustomIcon branch is the only one that sets two coupled fields
            using InformationBoxForm form = new InformationBoxForm("base");
            Icon icon = SystemIcons.Information;
            InformationBoxArgs args = new InformationBoxArgs { CustomIcon = icon };

            // Pre-assert: iconType starts as Internal (form default)
            Assume.That(GetField<IconType>(form, "iconType"), Is.EqualTo(IconType.Internal));

            // Act
            form.ApplyArgs(args);

            // Assert
            Assert.That(GetField<Icon>(form, "customIcon"), Is.SameAs(icon));
            Assert.That(GetField<IconType>(form, "iconType"), Is.EqualTo(IconType.UserDefined));
        }
    }
}
