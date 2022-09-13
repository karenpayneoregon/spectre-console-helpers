# About

Provides easy to use common prompts.

## Included

| Name        |   Description    |   Comments |
|:------------- |:-------------|:-------------|
| GetFirstName | prompt for first name | Allows no input which returns an empty string |
| GetMiddleName | prompt for first name | Allows no input which returns an empty string |
| GetLastName | prompt for last name | Allows no input which returns an empty string |
| GetInt | prompts for an int |  |
| GetPin | prompts for an int with length of four |  |
| GetSSN | prompts for an social security number | hidden with special validation |
| GetDecimal | prompts for a decimal |  |
| GetDouble | prompts for a double |  |
| GetBirthDate | prompts for a birth date  |  |
| GetDateTime | prompts for a regular DateTime |  |
| GetDateOnly | prompts for a nullable DateOnly | accepts a start value or has a default value |
| GetTimeOnly | prompts for a nullable TimeOnly | accepts a start value or has a default value |
| GetBool | prompts for Yes or No | returns a bool |
| GetUserName | prompt for user name  | suitable for a login |
| GetPassword | prompt for a password masked | suitable for a login |
| GetNewPassword | prompts for a new password | has default rules which can be changed see [package repo](https://github.com/havardt/PasswordValidator) |
| AskConfirmation | Ask a question  | returns a bool |
| MonthsSelection | presents a list of months | allows single or mulitple selections |
| GenericSelectionList&lt;T&gt; | Present a list where the user can select one or more items |  |
|  |  |  |


```csharp
using SpectreConsoleLibrary;
using TaxpayerConsoleApp.Models;

namespace TaxpayerConsoleApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        var taxpayer = GetTaxpayer();
        Console.Clear();
            
        AnsiConsole.MarkupLine("[cyan]Taxpayer details[/]");
        AnsiConsole.MarkupLine($"\t[yellow]Name[/] {taxpayer.FirstName} {taxpayer.LastName}");
        AnsiConsole.MarkupLine($"\t[yellow]SSN[/] {taxpayer.SSN}");
        AnsiConsole.MarkupLine($"\t[yellow]Pin[/] {taxpayer.Pin}");
        AnsiConsole.MarkupLine($"\t[yellow]Start date[/] {taxpayer.StartDate}");

        Console.ReadLine();
    }

    internal static Taxpayer GetTaxpayer()
    {
            
        Taxpayer taxpayer = new()
        {
            FirstName = Prompts.GetFirstName(false),
            LastName = Prompts.GetLastName(false),
            StartDate = Prompts.GetDateOnly(new DateOnly(2022, 2, 2)),
            SSN = Prompts.GetSSN(),
            Pin = Prompts.GetPin().ToString()
        };


        return taxpayer;
    }
}
```

