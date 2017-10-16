**NuGet packages**

.NET 2.0/3.5: [https://nuget.org/packages/InformationBoxLegacy/](https://nuget.org/packages/InformationBoxLegacy/)

.NET 4.0/4.5: [https://nuget.org/packages/InformationBox/](https://nuget.org/packages/InformationBox/)

**Build**

Status for .NET 4.0+ branch: 
![master](https://johannblais.visualstudio.com/_apis/public/build/definitions/ce081581-71ef-4bf2-bda5-0288ef071e07/1/badge)

**Project Description**

InformationBox is a flexible alternative to the default MessageBox included in the System.Windows.Forms namespace.

It provides the same base functionality and is extended with, for example, custom buttons, or personnalized icons. You can customize your InformationBoxes with custom screen position, or custom button placement. And there are many more features !

InformationBox is the simplest and easiest way to create personalized MessageBox.

Stop wasting time developing your own custom MessageBox, everything you need is already available. Just customize your MessageBox using the visual designer and the code is automatically generated !

**Find out more about the inner working of InformationBox [on my blog](http://johannblais.blogspot.com/)**.

**Highlights**

The InformationBox initializes itself according the provided parameters. The constructor accepts an undefined number of parameters, in an undefined order. Unlike the default MessageBox, if you only need to specify the default button, you do not have to provide any other parameter. Provide what you need, and let the default values do their jobs.
A version exists for both the framework 1.1 and 2.0+ and 4.0+.
The .NET 4.0 version supports optional parameters allowing clearer use of the different parameters.

Starting with release 0.6.0.0, a new visual style is available. It is only for the .NET 2.0+ version. As a result, the latest .NET 1.1 version is 0.5.1.0.
It is not planned to implement the new visual style in the .NET 1.1 branch, and as soon as this branch is stable, it will be discontinued, allowing faster development of the .NET 2.0+ branch.

Starting with release 0.6.5.2, the source, binary and help files are packaged using NSIS.

**Availability**

The project is available on NuGet, deployable using the installers, or compilable from source.

**Features**
* Multilanguage support (contributors needed). [Here](Supported%20Languages.md) is a list of supported languages.
* New visual style using glass components (.NET 2.0+ only).
* Scrollable when the text is too large.
* Message is selectable for easy copy (Ctrl-C).
* Possibility to modify the colors (back colors).
* Auto closing after _n_ seconds.
* Pretty icons included as replacements for the old MessageBox icons.
* Many new icons added to the default ones.
* External icons support, and automatic selection of the corresponding icon size.
* Possibility to add an icon in the title bar (it may be different from the main one in the box).
* Built-in support for "do not show again" checkbox.
* Custom buttons text.
* Custom buttons placement (right/left aligned, centered, etc).
* Custom placement (center on parent/screen).
* Possibility to reformat text to minimize width or height of the InformationBox.
* Support for MessageBox enums.
* .NET 1.1/2.0+ versions.
* Help file support.
* Possibility to show modeless boxes, details [here](Modeless%20boxes.md).
* Possibility to define [scopes](InformationBox%20scopes.md).
* Opacity (10 to 100%)
* Developer friendly (provide-only-what-you-want-to-customize constructor).
* Visual Designer. Customize your InformationBox and the designer generates the code!
* Available for FREE even for commercial use. 

[Screenshots](Screenshots.md) | [Samples](Samples.md) | [Contributors](Contributors.md) | [Contribute](Contribute.md) | [InformationBox in action](InformationBox%20in%20action.md)
