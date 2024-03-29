﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
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

        /// <summary>
        /// A demonstration which shows how to look at, in this case a Taxpayer property values
        /// after one or more changes have been made and get the changes using a generic method.
        ///
        /// Why do this? To show we can examine original and current values
        /// </summary>
        public static async Task GetOriginalValuesAfterEditingVersion1()
        {
            await using var context = new OedContext();

            Taxpayer taxpayer = (await context.Taxpayer.FirstOrDefaultAsync(payer => payer.Id == 1))!;

            taxpayer.FirstName = "Mads";
            taxpayer.Pin = "9999";

            // ReSharper disable once CollectionNeverQueried.Local
            List<EntityChangeItem> changes = new();

            var entry = context.Entry(taxpayer);
            foreach (IProperty item in entry.CurrentValues.Properties)
            {
                PropertyEntry propEntry = entry.Property(item.Name);
                if (propEntry.IsModified)
                {
                    changes.Add(new EntityChangeItem()
                    {
                        PropertyName = item.Name, 
                        OriginalValue = propEntry.OriginalValue, 
                        CurrentValue = propEntry.CurrentValue
                    });
                }
            }

        }
        /// <summary>
        /// A demonstration which shows how to look at, in this case a Taxpayer property values
        /// after one or more changes have been made and get the changes which targets <see cref="Taxpayer"/>
        ///
        /// Why do this? To show we can examine original and current values
        /// </summary>
        public static async Task GetOriginalValuesAfterEditingVersion2()
        {
            await using var context = new OedContext();

            Taxpayer taxpayer = (await context.Taxpayer.FirstOrDefaultAsync(payer => payer.Id == 1))!;

            taxpayer.FirstName = "Mads";


            var originalFirstName = context
                .Entry(taxpayer)
                .Property(x => x.FirstName)
                .OriginalValue;

            var currentFirstName = context
                .Entry(taxpayer)
                .Property(nameof(taxpayer.FirstName))
                .CurrentValue;

        }

        /// <summary>
        /// Add a new <see cref="Taxpayer"/>
        /// </summary>
        /// <param name="taxpayer"></param>
        /// <returns></returns>
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
