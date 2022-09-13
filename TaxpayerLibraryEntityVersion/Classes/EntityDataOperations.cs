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
    }
}
