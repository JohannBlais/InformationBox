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
using System.Runtime;
using System.Windows.Forms;

namespace InfoBoxCore.Designer.Tests
{
    public class CSharpGeneratorTests
    {
        private CSharpGenerator generator = new CSharpGenerator();
        private IEnumerable<MetadataReference> references;

        [SetUp]
        public void Setup()
        {
            references = new MetadataReference[]
               {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(AssemblyTargetedPatchBandAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(CSharpArgumentInfo).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(InformationBox).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(AutoScaleMode).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Icon).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Color).Assembly.Location),
               };
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
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.IsTrue(CompileCode(code).Success);
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
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.IsTrue(CompileCode(code).Success);
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
                                                    style: InformationBoxStyle.Standard,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: null,
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.IsTrue(CompileCode(code).Success);
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
                                                    style: InformationBoxStyle.Modern,
                                                    useAutoClose: false,
                                                    autoClose: null,
                                                    design: new DesignParameters(Color.Red, Color.Green),
                                                    titleStyle: InformationBoxTitleIconStyle.SameAsBox,
                                                    titleIconFileName: null,
                                                    opacity: InformationBoxOpacity.NoFade,
                                                    order: InformationBoxOrder.Default,
                                                    sound: InformationBoxSound.Default);

            Assert.IsTrue(CompileCode(code).Success);
        }

        private EmitResult CompileCode(string sourceCode)
        {
            var result = null as EmitResult;
            var sampleApp = Resources.SampleConsoleApp.Replace("{{CODE}}", sourceCode);
            var codeString = SourceText.From(sampleApp);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp8);

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
