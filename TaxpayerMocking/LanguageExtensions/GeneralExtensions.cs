using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxpayerMocking.LanguageExtensions
{
    internal static class GeneralExtensions
    {
        public static string ToYesNo(this bool value) => value ? "Yes" : "No";
    }
}
