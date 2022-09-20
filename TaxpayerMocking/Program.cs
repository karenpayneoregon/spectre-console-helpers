using Newtonsoft.Json;
using TaxpayerLibraryEntityVersion.Models;
using TaxpayerMocking.Classes;
using TaxpayerMocking.LanguageExtensions;

namespace TaxpayerMocking
{
    internal partial class Program
    {
        /// <summary>
        /// This code is responsible for creating a new database and populating it with data
        /// As coded if the database does not exists it's created, otherwise display data.
        ///
        /// Alternate use is to run SetupDatabase.Initialize(25); even if the database exist
        /// to refresh after during the data.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var databaseExists = await LocalDbOperations.CanConnect();

            if (!databaseExists)
            {
                SetupDatabase.Initialize(25);
            }

            List<Taxpayer> taxpayerList = SetupDatabase.GetTaxpayers();

            JsonExample(taxpayerList);

            var table = CreateTable();

            AnsiConsole.Clear();

            foreach (var taxpayer in taxpayerList)
            {
                if (taxpayer.StartDate.HasValue)
                {
                    table.AddRow(taxpayer.Id.ToString(),
                        taxpayer.FullName,
                        taxpayer.SocialSecurityNumber,
                        taxpayer.Pin,
                        taxpayer.StartDate.Value.ToString("MM/dd/yyyy"),
                        taxpayer.Category.Description);
                }
            }

            AnsiConsole.Write(table);
            Console.ReadLine();
        }

        /// <summary>
        /// How to serialize a self-referencing model
        /// </summary>
        private static void JsonExample(List<Taxpayer> taxpayerList)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects, 
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(taxpayerList, jsonSerializerSettings);
            File.WriteAllText("Taxpayers.json", json);
            List<Taxpayer> taxpayers = JsonConvert.DeserializeObject<List<Taxpayer>>(json);

            // put breakpoint here to examine the list
        }
    }
}