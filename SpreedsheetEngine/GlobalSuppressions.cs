// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "I did not write this file.")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Specifically told it should be protected.", Scope = "member", Target = "~F:CptS321.Cell.text")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Specifically told it should be protected.", Scope = "member", Target = "~F:CptS321.Cell.value")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "Used delegate syntax in lecture so using it here.", Scope = "member", Target = "~E:CptS321.Cell.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "Used delegate syntax in lecture so using it here.", Scope = "member", Target = "~E:SpreadsheetEngine.Spreadsheet.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:Field names should begin with lower-case letter", Justification = "The name BGColor is specified in instructions.", Scope = "member", Target = "~F:CptS321.Cell.BGColor")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Protected to match other cell attributes.", Scope = "member", Target = "~F:CptS321.Cell.BGColor")]
