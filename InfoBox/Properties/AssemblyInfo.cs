using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("InfoBox")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("SaumonAgile")]
[assembly: AssemblyProduct("InfoBox")]
[assembly: AssemblyCopyright("Copyright © SaumonAgile 2007")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Assembly is CLS compliant
[assembly: CLSCompliant(true)]

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
[assembly: AssemblyVersion("0.5.1.*")]
[assembly: AssemblyFileVersion("0.5.1.0")]

//
// Pour signer votre assembly, vous devez spécifier la clé à utiliser. Consultez 
// la documentation Microsoft .NET Framework pour plus d'informations sur la signature d'un assembly.
//
// Utilisez les attributs ci-dessous pour contrôler la clé utilisée lors de la signature. 
//
// Remarques : 
//   (*) Si aucune clé n'est spécifiée, l'assembly n'est pas signé.
//   (*) KeyName fait référence à une clé installée dans le fournisseur de
//       services cryptographiques (CSP) de votre ordinateur. KeyFile fait référence à un fichier qui contient
//       une clé.
//   (*) Si les valeurs de KeyFile et de KeyName sont spécifiées, le 
//       traitement suivant se produit :
//       (1) Si KeyName se trouve dans le CSP, la clé est utilisée.
//       (2) Si KeyName n'existe pas mais que KeyFile existe, la clé 
//           de KeyFile est installée dans le CSP et utilisée.
//   (*) Pour créer KeyFile, vous pouvez utiliser l'utilitaire sn.exe (Strong Name, Nom fort).
//        Lors de la spécification de KeyFile, son emplacement doit être
//        relatif au répertoire de sortie du projet qui est
//       %Project Directory%\obj\<configuration>. Par exemple, si votre KeyFile se trouve
//       dans le répertoire du projet, vous devez spécifier l'attribut 
//       AssemblyKeyFile sous la forme [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) DelaySign (signature différée) est une option avancée. Pour plus d'informations, consultez la
//       documentation Microsoft .NET Framework.
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("..\\..\\Properties\\key.snk")]
[assembly: AssemblyKeyName("")]