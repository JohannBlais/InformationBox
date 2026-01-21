using InfoBox;
using InfoBox.Designer.CodeGeneration;
using InfoBoxCore.Designer.Tests.Properties;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Windows.Forms;

namespace InfoBoxCore.Designer.Tests
{
    public class CSharpGeneratorTests
    {
        private readonly CSharpGenerator generator = new();
        private IEnumerable<MetadataReference> references;

        [SetUp]
        public void Setup()
        {
            var localReferences = new List<MetadataReference>
               {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(AssemblyTargetedPatchBandAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(CSharpArgumentInfo).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(InformationBox).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(AutoScaleMode).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Icon).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Color).Assembly.Location)
               };

            Assembly.GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .ToList()
                    .ForEach(a => localReferences.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));

            references = localReferences.ToArray();
        }

        [Test]
        public void Test0001()
        {
            var code = generator.GenerateSingleCall(behavior: InformationBoxBehavior.Modal,
                                                    text: "Text",
                                                    title: "Title",
                                                    buttons: InformationBoxButtons.User1User2,
                                                    button1Text: "Button 1",
                                                    button2Text: "Button 2",
                                                    button3Text: "Button 3",
                                                    icon: InformationBoxIcon.Hand,
                                                    iconFileName: null,
                                                    defaultButton: InformationBoxDefaultButton.Button1,
                                                    buttonsLayout: InformationBoxButtonsLayout.GroupMiddle,
                                                    autoSize: InformationBoxAutoSizeMode.None,
                                                    position: InformationBoxPosition.CenterOnParent,
                                                    showHelp: false,
                                                    helpFile: null,
                                                    helpTopic: null,
                                                    navigator: HelpNavigator.TableOfContents,
                                                    checkState: InformationBoxCheckBox.Show,
                                                    doNotShowAgainText: null,
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    fontParameters: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.That(CompileCode(code).Success);
        }

        [Test]
        public void Test0002()
        {
            var code = generator.GenerateSingleCall(behavior: InformationBoxBehavior.Modal,
                                                    text: "Text",
                                                    title: "Title",
                                                    buttons: InformationBoxButtons.OK,
                                                    button1Text: null,
                                                    button2Text: null,
                                                    button3Text: null,
                                                    icon: InformationBoxIcon.Hand,
                                                    iconFileName: null,
                                                    defaultButton: InformationBoxDefaultButton.Button1,
                                                    buttonsLayout: InformationBoxButtonsLayout.GroupMiddle,
                                                    autoSize: InformationBoxAutoSizeMode.None,
                                                    position: InformationBoxPosition.CenterOnParent,
                                                    showHelp: false,
                                                    helpFile: null,
                                                    helpTopic: null,
                                                    navigator: HelpNavigator.TableOfContents,
                                                    checkState: InformationBoxCheckBox.Show,
                                                    doNotShowAgainText: "Checkbox",
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    fontParameters: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.That(CompileCode(code).Success);
        }

        [Test]
        public void Test0003()
        {
            var code = generator.GenerateSingleCall(behavior: InformationBoxBehavior.Modal,
                                                    text: "Text",
                                                    title: "Title",
                                                    buttons: InformationBoxButtons.OK,
                                                    button1Text: null,
                                                    button2Text: null,
                                                    button3Text: null,
                                                    icon: InformationBoxIcon.Hand,
                                                    iconFileName: @"C:\fake\filename.ico",
                                                    defaultButton: InformationBoxDefaultButton.Button1,
                                                    buttonsLayout: InformationBoxButtonsLayout.GroupMiddle,
                                                    autoSize: InformationBoxAutoSizeMode.None,
                                                    position: InformationBoxPosition.CenterOnParent,
                                                    showHelp: true,
                                                    helpFile: @"C:\fake\helpfile.chm",
                                                    helpTopic: null,
                                                    navigator: HelpNavigator.TableOfContents,
                                                    checkState: 0,
                                                    doNotShowAgainText: "",
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    fontParameters: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.That(CompileCode(code).Success);
        }

        [Test]
        public void Test0004()
        {
            var code = generator.GenerateSingleCall(behavior: InformationBoxBehavior.Modal,
                                                    text: "Text",
                                                    title: "Title",
                                                    buttons: InformationBoxButtons.OK,
                                                    button1Text: null,
                                                    button2Text: null,
                                                    button3Text: null,
                                                    icon: InformationBoxIcon.Hand,
                                                    iconFileName: @"C:\fake\filename.ico",
                                                    defaultButton: InformationBoxDefaultButton.Button1,
                                                    buttonsLayout: InformationBoxButtonsLayout.GroupMiddle,
                                                    autoSize: InformationBoxAutoSizeMode.None,
                                                    position: InformationBoxPosition.CenterOnParent,
                                                    showHelp: true,
                                                    helpFile: @"C:\fake\helpfile.chm",
                                                    helpTopic: null,
                                                    navigator: HelpNavigator.TableOfContents,
                                                    checkState: 0,
                                                    doNotShowAgainText: "",
                                                    style: InformationBoxStyle.Modern,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: new DesignParameters(Color.Red, Color.Green),
                                                    fontParameters: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.That(CompileCode(code).Success);
        }

        [Test]
        public void Test0005()
        {
            var code = generator.GenerateSingleCall(behavior: InformationBoxBehavior.Modal,
                                                    text: "Text",
                                                    title: "Title",
                                                    buttons: InformationBoxButtons.User1User2User3,
                                                    button1Text: "Button1 Text",
                                                    button2Text: "Button2 Text",
                                                    button3Text: "Button3 Text",
                                                    icon: InformationBoxIcon.Hand,
                                                    iconFileName: @"C:\fake\filename.ico",
                                                    defaultButton: InformationBoxDefaultButton.Button1,
                                                    buttonsLayout: InformationBoxButtonsLayout.GroupMiddle,
                                                    autoSize: InformationBoxAutoSizeMode.None,
                                                    position: InformationBoxPosition.CenterOnParent,
                                                    showHelp: true,
                                                    helpFile: @"C:\fake\helpfile.chm",
                                                    helpTopic: null,
                                                    navigator: HelpNavigator.TableOfContents,
                                                    checkState: 0,
                                                    doNotShowAgainText: "",
                                                    style: InformationBoxStyle.Modern,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: new DesignParameters(Color.Red, Color.Green),
                                                    fontParameters: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.That(CompileCode(code).Success);
        }

        private EmitResult CompileCode(string sourceCode)
        {
            var result = null as EmitResult;
            var sampleApp = Resources.SampleConsoleApp.Replace("{{CODE}}", sourceCode);
            var codeString = SourceText.From(sampleApp);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);


            var compiledCode = CSharpCompilation.Create("Tests.dll",
                new[] { parsedSyntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.WindowsApplication,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));

            using (var peStream = new MemoryStream())
            {
                result = compiledCode.Emit(peStream);
            }

            return result;
        }

    }
}
