using Microsoft.EntityFrameworkCore;
using OracleEntityFrameworkLibrary.Data;
using OracleEntityFrameworkLibrary.Models;

namespace OracleEntityFrameworkConsoleApp.Classes
{
    /// <summary>
    /// Pure data retrieval separated by caller which displays information to the console colorized.
    ///
    /// See <see cref="OedContext"/> in regards to connection setup
    /// </summary>
    public class DataOperations
    {
        /// <summary>
        /// Example to get a <see cref="FederalReserveRouting"/> record by two WHERE conditions from table FEDERAL_RESERVE_ROUTINGS
        /// </summary>
        /// <param name="bankName">name of bank</param>
        /// <param name="revised">revision date</param>
        /// <remarks>
        /// AsNoTracking is used as we are not making changes. There is also an option to use
        /// UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) when configuring our DbContext which affects all queries then there is a
        /// counterpart to AsNoTracking, AsTracking
        /// </remarks>
        public static async Task<FederalReserveRouting> FederalReserveRouting(string bankName, DateTime revised)
        {
            await using var context = new OedContext();

            return await context.FederalReserveRouting.AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BankName == bankName && x.LastRevisionDate == revised);
        }

        /// <summary>
        /// Example to return top <see cref="count"/> from table LINE_FLAG_CODES
        /// </summary>
        /// <param name="count">optional, if not passed the default is top 15 records</param>
        /// <remarks>
        /// AsNoTracking is used as we are not making changes. There is also an option to use
        /// UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) when configuring our DbContext which affects all queries then there is a
        /// counterpart to AsNoTracking, AsTracking
        /// </remarks>
        public static async Task<List<LineFlagCodes>> GetLineFlagCodesList(int count = 15)
        {
            await using var context = new OedContext();
            return await context.LineFlagCodes.AsNoTracking().Take(count).ToListAsync();
        }
    }
}
