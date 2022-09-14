using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxpayerLibraryEntityVersion.Data;
using TaxpayerLibraryEntityVersion.Models;

namespace TaxpayerLibraryEntityVersion.Classes
{
    public class EntityDataOperations
    {
        public static async Task<List<Taxpayer>> GetTaxpayers()
        {
            await using var context = new OedContext();
            return await context.Taxpayer.ToListAsync();
        }

        public static async Task<Taxpayer> GetTaxpayerByIdentity(int id)
        {
            await using var context = new OedContext();
            return (await context.Taxpayer.FirstOrDefaultAsync(payer => payer.Id == id))!;
        }

        public static async Task AddNewTaxpayer(Taxpayer taxpayer)
        {
            await using var context = new OedContext();
            context.Add(taxpayer);
            await context.SaveChangesAsync();
        }

        public static async Task<(Taxpayer taxpayer, bool)> EditTaxpayer()
        {
            await using var context = new OedContext();
            int id = 3;
            Taxpayer taxpayer = context.Taxpayer.FirstOrDefault(x => x.Id == id)!;
            if (taxpayer is not null)
            {
                RandomDateTime date = new RandomDateTime();
                taxpayer.StartDate = date.Next();
                await context.SaveChangesAsync();
                return (taxpayer, true);
            }
            else
            {
                return (null, false)!;
            }
        }
    }
}
