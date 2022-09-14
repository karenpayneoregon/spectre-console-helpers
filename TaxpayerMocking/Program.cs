using TaxpayerMocking.Classes;

namespace TaxpayerMocking
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            SetupDatabase.Initialize(20);
            var taxpayers = SetupDatabase.GetTaxpayers();

            var table = CreateTable();

            AnsiConsole.Clear();

            foreach (var taxpayer in taxpayers)
            {
                table.AddRow(taxpayer.Id.ToString(), taxpayer.FullName, taxpayer.SocialSecurityNumber, taxpayer.Pin,
                    taxpayer.StartDate.Value.ToString("MM/dd/yyyy"));
            }

            AnsiConsole.Write(table);
            Console.ReadLine();
        }
    }
}