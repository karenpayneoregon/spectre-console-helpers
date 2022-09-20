# Code helpers in Visual Studio

Here you will find information to assist with code helpers within Visual Studio

- [Snippets](snippets.md) Code snippets are small blocks of reusable code that can be inserted in a code file using a right-click menu (context menu) command or a combination of hotkeys. Knowing code snippets if nothing else saves time not having to type out all code as explained with examples.
- [Refactoring code](refactoringCode.md) Visual Studio has built-in refactoring for common issues, suggestions and more.
- [Working with data basics](InteractingWithData.md) using both manage [data provider](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/data-providers) and [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Working with SQL](WritingSql.md) 

# Projects

| Project        |   Description    |   Notes |
|:------------- |:-------------|:-------------|
| TaxpayerLibrary :small_orange_diamond: | Base data operations usig a Microsoft data provider | Microsoft supports all major databases, here we are using SQL-Server provider |
| TaxpayerLibraryEntityVersion :small_orange_diamond: | Base data operations usig a Microsoft [EF Core 6](https://learn.microsoft.com/en-us/ef/core/) with SQL-Server | See also [tips for working with EF Core](https://github.com/karenpayneoregon/ef-core-6-tips) |
| SpectreLibraryConsoleApp :small_blue_diamond: | code samples for enriching console apps see [readme.md](SpectreConsole.md) | Dependent on custom [NuGet package](https://www.nuget.org/packages/SpectreConsoleLibrary/) |
| TaxpayerConsoleApp :small_blue_diamond: | Read, add, edit data using EF Core 6 using SQL-Server | Uses TaxpayerLibraryEntityVersion project, Ask Karen for connections |
| OracleEntityFrameworkConsoleApp :small_blue_diamond: | Simple EF Core using Oracle |  |
| OracleEntityFrameworkLibrary :small_orange_diamond: | Data operations using EF Core 6 and Oracle for OracleEntityFrameworkConsoleApp project | Ask Karen for connections |
| ColdFusionApp :small_blue_diamond: | uses Oracle data provider, not EF Core | Ask Karen for connections |
| ColdFusionLibrary :small_orange_diamond: | Data operations for project ColdFusionApp |  |
| OracleUtils1 :small_blue_diamond: | Basic code to get table schema for an Oracle table. | All code works but is  WIP |
|  |  |  |

**Legend**

:small_orange_diamond: Class project

:small_blue_diamond:  Console project
