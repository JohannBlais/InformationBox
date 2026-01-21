// <copyright file="InformationBoxPresenterTests.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Unit tests for InformationBoxPresenter</summary>

namespace InfoBoxCore.Tests.Presentation
{
    using FluentAssertions;
    using InfoBox;
    using InfoBox.Presentation;
    using InfoBoxCore.Tests.Mocks;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Unit tests for the InformationBoxPresenter class.
    /// </summary>
    [TestFixture]
    public class InformationBoxPresenterTests
    {
        private InformationBoxModel model;
        private MockTextMeasurement textMeasurement;
        private InformationBoxPresenter presenter;

        [SetUp]
        public void SetUp()
        {
            this.model = new InformationBoxModel
            {
                Text = "Test message",
                Title = "Test Title",
                Font = new Font("Arial", 10),
                Buttons = InformationBoxButtons.OK,
                WorkingArea = new Rectangle(0, 0, 1920, 1080)
            };

            this.textMeasurement = new MockTextMeasurement();
            this.textMeasurement.DefaultSize = new SizeF(100, 20);

            this.presenter = new InformationBoxPresenter(this.model, this.textMeasurement);
        }

        [TearDown]
        public void TearDown()
        {
            if (this.model.Font != null)
            {
                this.model.Font.Dispose();
            }
        }

        #region GetButtons Tests

        [Test]
        public void GetButtons_OKButton_ReturnsOneButton()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.OK;

            // Act
            var buttons = this.presenter.GetButtons();

            // Assert
            buttons.Should().HaveCount(1);
            buttons[0].Name.Should().Be("OK");
            buttons[0].Result.Should().Be(InformationBoxResult.OK);
            buttons[0].IsDefault.Should().BeTrue();
        }

        [Test]
        public void GetButtons_OKCancel_ReturnsTwoButtons()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.OKCancel;

            // Act
            var buttons = this.presenter.GetButtons();

            // Assert
            buttons.Should().HaveCount(2);
            buttons[0].Name.Should().Be("OK");
            buttons[1].Name.Should().Be("Cancel");
        }

        [Test]
        public void GetButtons_YesNoCancel_ReturnsThreeButtonsInCorrectOrder()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.YesNoCancel;

            // Act
            var buttons = this.presenter.GetButtons();

            // Assert
            buttons.Should().HaveCount(3);
            buttons[0].Name.Should().Be("Yes");
            buttons[1].Name.Should().Be("No");
            buttons[2].Name.Should().Be("Cancel");
        }

        [Test]
        public void GetButtons_AbortRetryIgnore_ReturnsThreeButtonsInCorrectOrder()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.AbortRetryIgnore;

            // Act
            var buttons = this.presenter.GetButtons();

            // Assert
            buttons.Should().HaveCount(3);
            buttons[0].Name.Should().Be("Abort");
            buttons[1].Name.Should().Be("Retry");
            buttons[2].Name.Should().Be("Ignore");
        }

        [Test]
        public void GetButtons_User1User2User3_ReturnsThreeCustomButtons()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.User1User2User3;
            var customTexts = new string[] { "Custom1", "Custom2", "Custom3" };

            // Act
            var buttons = this.presenter.GetButtons(customTexts);

            // Assert
            buttons.Should().HaveCount(3);
            buttons[0].Name.Should().Be("User1");
            buttons[0].Text.Should().Be("Custom1");
            buttons[1].Name.Should().Be("User2");
            buttons[1].Text.Should().Be("Custom2");
            buttons[2].Name.Should().Be("User3");
            buttons[2].Text.Should().Be("Custom3");
        }

        [Test]
        public void GetButtons_WithHelpFile_AddsHelpButton()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.OK;

            // Act
            var buttons = this.presenter.GetButtons(hasHelpFile: true);

            // Assert
            buttons.Should().HaveCount(2);
            buttons[1].Name.Should().Be("Help");
        }

        [Test]
        public void GetButtons_DefaultButtonButton2_MarksSecondButtonAsDefault()
        {
            // Arrange
            this.model.Buttons = InformationBoxButtons.YesNo;
            this.model.DefaultButton = InformationBoxDefaultButton.Button2;

            // Act
            var buttons = this.presenter.GetButtons();

            // Assert
            buttons[0].IsDefault.Should().BeFalse();
            buttons[1].IsDefault.Should().BeTrue();
        }

        #endregion

        #region CalculateLayout Tests

        [Test]
        public void CalculateLayout_SimpleText_CalculatesCorrectDimensions()
        {
            // Arrange
            this.model.Text = "Test message";
            this.model.Title = "Title";
            this.textMeasurement.SetMeasuredSize("Test message", 200, 40);
            this.textMeasurement.SetMeasuredSize("Title", 80, 20);

            // Act
            var layout = this.presenter.CalculateLayout(
                buttonCount: 1,
                buttonWidthPerButton: 75,
                bottomPanelHeight: 50,
                hasCheckBox: false,
                checkBoxText: null);

            // Assert
            layout.TextWidth.Should().Be(220); // 200 + BorderPadding (20)
            layout.TextHeight.Should().Be(40);
            layout.CaptionWidth.Should().Be(110); // 80 + 30
            layout.ButtonsMinWidth.Should().BeGreaterThan(0);
            layout.RequiredWidth.Should().BeGreaterThan(0);
            layout.RequiredHeight.Should().BeGreaterThan(0);
        }

        [Test]
        public void CalculateLayout_WithIcon_IncludesIconWidth()
        {
            // Arrange
            this.model.Icon = InformationBoxIcon.Information;
            this.textMeasurement.SetMeasuredSize(this.model.Text, 200, 40);

            // Act
            var layout = this.presenter.CalculateLayout(
                buttonCount: 1,
                buttonWidthPerButton: 75,
                bottomPanelHeight: 50,
                hasCheckBox: false,
                checkBoxText: null);

            // Assert
            layout.IconHeight.Should().Be(32);
        }

        [Test]
        public void CalculateLayout_WithCheckBox_IncludesCheckBoxWidth()
        {
            // Arrange
            this.textMeasurement.SetMeasuredSize("Do not show again", 150, 20);

            // Act
            var layout = this.presenter.CalculateLayout(
                buttonCount: 1,
                buttonWidthPerButton: 75,
                bottomPanelHeight: 50,
                hasCheckBox: true,
                checkBoxText: "Do not show again");

            // Assert
            layout.CheckBoxWidth.Should().Be(230); // 150 + (BorderPadding * 4)
        }

        [Test]
        public void CalculateLayout_VeryTallContent_EnablesVerticalScroll()
        {
            // Arrange
            this.model.WorkingArea = new Rectangle(0, 0, 1920, 500); // Small height
            this.textMeasurement.SetMeasuredSize(this.model.Text, 200, 600); // Text taller than screen

            // Act
            var layout = this.presenter.CalculateLayout(
                buttonCount: 2,
                buttonWidthPerButton: 75,
                bottomPanelHeight: 50,
                hasCheckBox: false,
                checkBoxText: null);

            // Assert
            layout.RequiresVerticalScroll.Should().BeTrue();
            layout.RequiredHeight.Should().Be(450); // WorkingArea.Height - 50
        }

        #endregion

        #region UpdateAutoClose Tests

        [Test]
        public void UpdateAutoClose_BeforeTimeout_ShouldNotClose()
        {
            // Arrange
            this.model.AutoClose = new AutoCloseParameters(5000); // 5 seconds
            var buttonNames = new List<string> { "OK", "Cancel" };

            // Act
            var state = this.presenter.UpdateAutoClose(
                elapsedSeconds: 3,
                totalSeconds: 5,
                buttonNames: buttonNames);

            // Assert
            state.ShouldClose.Should().BeFalse();
            state.RemainingSeconds.Should().Be(2);
        }

        [Test]
        public void UpdateAutoClose_AfterTimeout_ShouldClose()
        {
            // Arrange
            this.model.AutoClose = new AutoCloseParameters(5000); // 5 seconds
            var buttonNames = new List<string> { "OK", "Cancel" };

            // Act
            var state = this.presenter.UpdateAutoClose(
                elapsedSeconds: 5,
                totalSeconds: 5,
                buttonNames: buttonNames);

            // Assert
            state.ShouldClose.Should().BeTrue();
            state.RemainingSeconds.Should().Be(0);
        }

        [Test]
        public void UpdateAutoClose_ModeButton_SetsButtonToUpdate()
        {
            // Arrange
            // Use constructor with time and button (Mode is automatically set to Button)
            this.model.AutoClose = new AutoCloseParameters(
                5,
                InformationBoxDefaultButton.Button2);
            var buttonNames = new List<string> { "Yes", "No", "Cancel" };

            // Act
            var state = this.presenter.UpdateAutoClose(
                elapsedSeconds: 5,
                totalSeconds: 5,
                buttonNames: buttonNames);

            // Assert
            state.ShouldClose.Should().BeTrue();
            state.ButtonToUpdate.Should().Be("No"); // Button2 = index 1
        }

        [Test]
        public void UpdateAutoClose_ModeResult_SetsResultOnClose()
        {
            // Arrange
            // Use constructor with time and result (Mode is automatically set to Result)
            this.model.AutoClose = new AutoCloseParameters(
                5,
                InformationBoxResult.Cancel);
            var buttonNames = new List<string> { "OK" };

            // Act
            var state = this.presenter.UpdateAutoClose(
                elapsedSeconds: 5,
                totalSeconds: 5,
                buttonNames: buttonNames);

            // Assert
            state.ShouldClose.Should().BeTrue();
            state.ResultOnClose.Should().Be(InformationBoxResult.Cancel);
        }

        #endregion
    }
}
