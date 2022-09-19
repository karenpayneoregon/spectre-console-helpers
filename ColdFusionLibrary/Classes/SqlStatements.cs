using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColdFusionLibrary.Classes
{
    public class SqlStatements
    {
        /// <summary>
        /// Get FederalReserveRouting records
        /// </summary>
        /// <param name="maxRow">max rows to return</param>
        public static string FederalReserveRouting(int maxRow = 20)
        {
            string selectStatement = @$"
            SELECT 
                ROUTING_NUM,
                BANK_NAME,
                BANK_ADDRESS_CITY,BANK_ADDRESS_STATE,
                LAST_REVISION_DATE,
                POST_DATE,VALID_FLAG 
            FROM FEDERAL_RESERVE_ROUTINGS 
            WHERE ROWNUM <= {maxRow} ORDER BY BANK_NAME";


            return selectStatement;
        }
    }
}
