using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OracleEntityFrameworkConsoleApp.Models;

[Keyless]
[Table("FEDERAL_RESERVE_ROUTINGS")]
internal class FederalReserveRouting
{
    [Column("ROUTING_NUM")]
    public string RoutingNumber { get; set; }

    [Column("BANK_NAME")]
    public string BankName { get; set; }

    [Column("BANK_ADDRESS_CITY")]
    public string BankAddressCity { get; set; }

    [Column("BANK_ADDRESS_STATE")]
    public string BankAddressState { get; set; }

    [Column("LAST_REVISION_DATE")]
    public DateTime LastRevisionDate { get; set; }

    [Column("POST_DATE")]
    public DateTime PostDate { get; set; }

    [Column("VALID_FLAG")]
    public string Flag { get; set; }

    public override string ToString() => RoutingNumber;


}