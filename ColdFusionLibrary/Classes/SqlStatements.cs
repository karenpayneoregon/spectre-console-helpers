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
        /// Get MPC records
        /// </summary>
        /// <param name="maxRow">max rows to return</param>
        /// <param name="idNull">null condition for where clause</param>
        /// <returns></returns>
        public static string GetMpcRecordsSelectStatement(int maxRow = 500, bool idNull = true)
        {
            string selectStatement = @$"
			SELECT
				 BASE_PER_BEGIN_DATE
				,BASE_PER_END_DATE
				,BASE_PER_WAGES_THIS_EMPL_AMT
				,BENEFIT_YEAR_BEGIN_DATE
				,CLAIM_NUMBER_BYE
				,CHECK_DIGIT_NUM
				,CLAIMANT_FIRST_NAME
				,CLAIMANT_LAST_NAME
				,CLAIMANT_MIDDLE_INITIAL
				,CLAIMANT_OTHER_LAST_NAME
				,CLAIMANT_SEPREASNCD_CODE
				,COMMENT_TXT
				,EMPL_SEP_REASON_NEEDED_FLAG
				,EMPLOYER_ACCT_NUM
				,EMPLOYER_ACCOUNT_TYPE_CODE		SIDECLMTYPCD_CODE
				,EMPLOYER_NAME
				,FEDERAL_EMPLOYER_ID_NUM
				,FORM_NUM
				,INITIAL_CLAIM_DATE
				,PIN
				,POST_DATE
				,PROTEST_COMMENT_FLAG
				,REQUEST_DATE
				,REQUESTING_STATECD_CODE
				,RESPONSE_DUE_DATE
				,SIDES_BROKER_ID
				,SSN
				,STATE_BIN_PREDECESSOR
				,STATE_REQUEST_FILE_GUID
				,STATE_REQUEST_RECORD_GUID
				,TOTAL_BENEFIT_WEEKS
				,TOTAL_EMPL_CHARGE_AMT
				,TOTAL_EMPL_PCT_CHGS_NBR
				,UI_OFFICE_EMAIL_ADDRESS
				,UI_OFFICE_FAX_NUM
				,UI_OFFICE_NAME
				,UI_OFFICE_PHONE_NUM
			FROM SIDESTAB.MAINFRAME_MPC_FT
			WHERE STATE_REQUEST_FILE_GUID = @fileGuid AND ROWNUM <= {maxRow}";

            if (idNull)
            {
                selectStatement += " AND SIDES_BROKER_ID IS NULL";
            }
            else
            {
                selectStatement += " AND SIDES_BROKER_ID IS NOT NULL";
            }


            return selectStatement;
        }
    }
}
