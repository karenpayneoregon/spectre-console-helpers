using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace ComputerDetails
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            AnsiConsole.MarkupLine("[cyan1]Computer details[/]");
            Console.WriteLine();
        }

        public static bool Question(string questionText)
        {
            ConfirmationPrompt prompt = new($"[{Color.Yellow}]{questionText}[/]")
            {
                DefaultValueStyle = new(Color.Cyan1, Color.Black, Decoration.None),
                ChoicesStyle = new(Color.Yellow, Color.Black, Decoration.None),
                InvalidChoiceMessage = $"[{Color.Red}]Invalid choice[/]. Please select a Y or N.",
                DefaultValue = false
            };

            return prompt.Show(AnsiConsole.Console);
        }
    }
}
