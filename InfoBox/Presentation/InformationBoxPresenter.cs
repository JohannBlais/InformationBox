// <copyright file="InformationBoxPresenter.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Presenter containing testable business logic for InformationBox</summary>

namespace InfoBox.Presentation
{
    using InfoBox.Abstractions;
    using InfoBox.Properties;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Presenter containing testable business logic for InformationBox.
    /// </summary>
    /// <remarks>
    /// This presenter separates business logic from UI, making it fully testable
    /// without requiring WinForms dependencies.
    /// See TESTABILITY_ROADMAP.md - P0.1
    /// </remarks>
    public class InformationBoxPresenter
    {
        private readonly InformationBoxModel model;
        private readonly ITextMeasurement textMeasurement;

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxPresenter"/> class.
        /// </summary>
        /// <param name="model">The model containing configuration</param>
        /// <param name="textMeasurement">Text measurement service</param>
        public InformationBoxPresenter(InformationBoxModel model, ITextMeasurement textMeasurement)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (textMeasurement == null)
            {
                throw new ArgumentNullException("textMeasurement");
            }

            this.model = model;
            this.textMeasurement = textMeasurement;
        }

        /// <summary>
        /// Gets the list of buttons to display based on the model configuration.
        /// </summary>
        /// <param name="customButtonTexts">Custom button texts for User1, User2, User3</param>
        /// <param name="showHelpButton">Whether to show help button</param>
        /// <param name="hasHelpFile">Whether a help file is specified</param>
        /// <returns>List of button definitions</returns>
        public List<ButtonDefinition> GetButtons(
            IList<string> customButtonTexts = null,
            bool showHelpButton = false,
            bool hasHelpFile = false)
        {
            var buttons = new List<ButtonDefinition>();

            // Abort button
            if (this.model.Buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                buttons.Add(new ButtonDefinition("Abort", Resources.LabelAbort, InformationBoxResult.Abort));
            }

            // Ok
            if (this.model.Buttons == InformationBoxButtons.OK ||
                this.model.Buttons == InformationBoxButtons.OKCancel ||
                this.model.Buttons == InformationBoxButtons.OKCancelUser1)
            {
                buttons.Add(new ButtonDefinition("OK", Resources.LabelOK, InformationBoxResult.OK));
            }

            // Yes
            if (this.model.Buttons == InformationBoxButtons.YesNo ||
                this.model.Buttons == InformationBoxButtons.YesNoCancel ||
                this.model.Buttons == InformationBoxButtons.YesNoUser1)
            {
                buttons.Add(new ButtonDefinition("Yes", Resources.LabelYes, InformationBoxResult.Yes));
            }

            // Retry
            if (this.model.Buttons == InformationBoxButtons.AbortRetryIgnore ||
                this.model.Buttons == InformationBoxButtons.RetryCancel)
            {
                buttons.Add(new ButtonDefinition("Retry", Resources.LabelRetry, InformationBoxResult.Retry));
            }

            // No
            if (this.model.Buttons == InformationBoxButtons.YesNo ||
                this.model.Buttons == InformationBoxButtons.YesNoCancel ||
                this.model.Buttons == InformationBoxButtons.YesNoUser1)
            {
                buttons.Add(new ButtonDefinition("No", Resources.LabelNo, InformationBoxResult.No));
            }

            // Cancel
            if (this.model.Buttons == InformationBoxButtons.OKCancel ||
                this.model.Buttons == InformationBoxButtons.OKCancelUser1 ||
                this.model.Buttons == InformationBoxButtons.RetryCancel ||
                this.model.Buttons == InformationBoxButtons.YesNoCancel)
            {
                buttons.Add(new ButtonDefinition("Cancel", Resources.LabelCancel, InformationBoxResult.Cancel));
            }

            // Ignore
            if (this.model.Buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                buttons.Add(new ButtonDefinition("Ignore", Resources.LabelIgnore, InformationBoxResult.Ignore));
            }

            // Get custom button texts
            string user1Text = "User1";
            string user2Text = "User2";
            string user3Text = "User3";

            if (customButtonTexts != null)
            {
                if (customButtonTexts.Count > 0)
                {
                    user1Text = customButtonTexts[0];
                }
                if (customButtonTexts.Count > 1)
                {
                    user2Text = customButtonTexts[1];
                }
                if (customButtonTexts.Count > 2)
                {
                    user3Text = customButtonTexts[2];
                }
            }

            // User1
            if (this.model.Buttons == InformationBoxButtons.OKCancelUser1 ||
                this.model.Buttons == InformationBoxButtons.User1User2User3 ||
                this.model.Buttons == InformationBoxButtons.User1User2 ||
                this.model.Buttons == InformationBoxButtons.YesNoUser1 ||
                this.model.Buttons == InformationBoxButtons.User1)
            {
                buttons.Add(new ButtonDefinition("User1", user1Text, InformationBoxResult.User1));
            }

            // User2
            if (this.model.Buttons == InformationBoxButtons.User1User2 ||
                this.model.Buttons == InformationBoxButtons.User1User2User3)
            {
                buttons.Add(new ButtonDefinition("User2", user2Text, InformationBoxResult.User2));
            }

            // User3
            if (this.model.Buttons == InformationBoxButtons.User1User2User3)
            {
                buttons.Add(new ButtonDefinition("User3", user3Text, InformationBoxResult.User3));
            }

            // Help button is displayed when asked or when a help file name exists
            if (showHelpButton || hasHelpFile)
            {
                buttons.Add(new ButtonDefinition("Help", Resources.LabelHelp, InformationBoxResult.None));
            }

            // Mark default button
            if (buttons.Count > 0)
            {
                int defaultIndex = (int)this.model.DefaultButton;
                if (defaultIndex >= 0 && defaultIndex < buttons.Count)
                {
                    buttons[defaultIndex].IsDefault = true;
                }
                else if (buttons.Count > 0)
                {
                    buttons[0].IsDefault = true;
                }
            }

            return buttons;
        }

        /// <summary>
        /// Calculates the layout dimensions for the InformationBox.
        /// </summary>
        /// <param name="buttonCount">Number of buttons</param>
        /// <param name="buttonWidthPerButton">Average width per button</param>
        /// <param name="bottomPanelHeight">Height of bottom panel with buttons</param>
        /// <param name="hasCheckBox">Whether checkbox is displayed</param>
        /// <param name="checkBoxText">Checkbox text</param>
        /// <returns>Layout calculation results</returns>
        public LayoutCalculation CalculateLayout(
            int buttonCount,
            int buttonWidthPerButton,
            int bottomPanelHeight,
            bool hasCheckBox,
            string checkBoxText)
        {
            var result = new LayoutCalculation();

            // Caption width including button
            result.CaptionWidth = (int)this.textMeasurement.MeasureString(this.model.Title, this.model.Font).Width + 30;
            if (this.model.TitleStyle != InformationBoxTitleIconStyle.None)
            {
                result.CaptionWidth += this.model.BorderPadding * 2;
            }

            // "Do not show this dialog again" width
            if (hasCheckBox && !String.IsNullOrEmpty(checkBoxText))
            {
                result.CheckBoxWidth = (int)this.textMeasurement.MeasureString(checkBoxText, this.model.Font).Width + this.model.BorderPadding * 4;
            }
            else
            {
                result.CheckBoxWidth = 0;
            }

            // Minimum width to display all needed buttons
            result.ButtonsMinWidth = (buttonCount + 4) * this.model.BorderPadding + (buttonCount * buttonWidthPerButton);

            // Icon width
            int iconAndTextWidth = 0;
            if (this.model.Icon != InformationBoxIcon.None || this.model.CustomIcon != null)
            {
                iconAndTextWidth += this.model.IconPanelWidth;
            }

            // Text measurements
            int screenWidth = this.model.WorkingArea.Width;
            var textSize = this.textMeasurement.MeasureString(this.model.Text, this.model.Font, screenWidth / 2);
            result.TextWidth = (int)textSize.Width + this.model.BorderPadding;
            result.TextHeight = (int)textSize.Height;

            iconAndTextWidth += result.TextWidth;

            // Calculate total width
            int totalWidth = Math.Max(Math.Max(result.CaptionWidth, result.CheckBoxWidth),
                                     Math.Max(result.ButtonsMinWidth, iconAndTextWidth));

            // Icon height
            result.IconHeight = 0;
            if (this.model.Icon != InformationBoxIcon.None || this.model.CustomIcon != null)
            {
                result.IconHeight = 32; // Standard icon height
            }

            // Calculate total height
            int totalHeight = Math.Max(result.IconHeight, result.TextHeight) + this.model.BorderPadding * 2 + bottomPanelHeight;

            // Add small space to avoid vertical scrollbar if needed
            if (iconAndTextWidth > this.model.WorkingArea.Width - 100)
            {
                totalHeight += 20;
            }

            // Check if vertical scroll is needed
            result.RequiresVerticalScroll = false;
            if (totalHeight > this.model.WorkingArea.Height - 50)
            {
                totalHeight = this.model.WorkingArea.Height - 50;
                totalWidth += 20; // Add space for scrollbar
                result.RequiresVerticalScroll = true;
            }

            // Set final dimensions
            result.RequiredWidth = Math.Min(this.model.WorkingArea.Width - 20, totalWidth);
            result.RequiredHeight = totalHeight;
            result.MainPanelWidth = result.RequiredWidth;
            result.MainPanelHeight = totalHeight - bottomPanelHeight;

            return result;
        }

        /// <summary>
        /// Updates the auto-close state based on elapsed time.
        /// </summary>
        /// <param name="elapsedSeconds">Number of seconds elapsed</param>
        /// <param name="totalSeconds">Total seconds for auto-close</param>
        /// <param name="buttonNames">Names of buttons to potentially update</param>
        /// <returns>Auto-close state</returns>
        public AutoCloseState UpdateAutoClose(
            int elapsedSeconds,
            int totalSeconds,
            IList<string> buttonNames)
        {
            var state = new AutoCloseState();
            state.RemainingSeconds = totalSeconds - elapsedSeconds;
            state.ShouldClose = elapsedSeconds >= totalSeconds;

            if (this.model.AutoClose != null && state.ShouldClose)
            {
                // Determine which button to click or result to return
                if (this.model.AutoClose.Mode == AutoCloseDefinedParameters.Button ||
                    this.model.AutoClose.Mode == AutoCloseDefinedParameters.TimeOnly)
                {
                    // Determine button index based on default button setting
                    int buttonIndex = 0;
                    if (this.model.AutoClose.Mode == AutoCloseDefinedParameters.Button)
                    {
                        buttonIndex = (int)this.model.AutoClose.DefaultButton;
                    }
                    else
                    {
                        buttonIndex = (int)this.model.DefaultButton;
                    }

                    if (buttonIndex >= 0 && buttonIndex < buttonNames.Count)
                    {
                        state.ButtonToUpdate = buttonNames[buttonIndex];
                    }
                }
                else if (this.model.AutoClose.Mode == AutoCloseDefinedParameters.Result)
                {
                    state.ResultOnClose = this.model.AutoClose.Result;
                }
            }

            return state;
        }
    }
}
