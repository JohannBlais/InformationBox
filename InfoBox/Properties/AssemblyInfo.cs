// <copyright file="AssemblyInfo.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains assembly informations</summary>

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NET6_0_OR_GREATER
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("InfoBox")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Johann Blais")]
[assembly: AssemblyProduct("InformationBox")]
[assembly: AssemblyCopyright("Copyright © Johann Blais 2007-2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Neutral language
[assembly: NeutralResourcesLanguage("en")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Make internal types visible to test assemblies
[assembly: InternalsVisibleTo("InfoBoxCore.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001000d083a79d3b652c8e75ecc5b0eaf93f8160a1de04d6f967a87d4a7f6e2b5916017afef3cb81a9a6789079138170385c6e30dfdbb8b9999e08e29436e87bb10044b637e6c9cf0f6e52b64ba19001b5181839a5471dff368d415d29cbaae2189f89d7b5f736ef3e7692e257a35c0836ec97e5a2a950864617b8642590517bf8c9a")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e14a6ac1-494d-481d-8510-d652082e43ba")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.4.0.0")]
[assembly: AssemblyFileVersion("1.4.0.0")]
