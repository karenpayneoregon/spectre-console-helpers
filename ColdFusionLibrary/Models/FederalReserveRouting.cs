namespace ColdFusionLibrary.Models;

public class FederalReserveRouting
{
    public string ROUTING_NUM { get; set; }
    public string BANK_NAME { get; set; }
    public string BANK_ADDRESS_CITY { get; set; }
    public string BANK_ADDRESS_STATE { get; set; }
    public DateTime LAST_REVISION_DATE { get; set; }
    public DateTime POST_DATE { get; set; }
    public string VALID_FLAG { get; set; }
    public string Row => $"{ROUTING_NUM}, {BANK_NAME}, {POST_DATE:MM/dd/yyyy}";
    public override string ToString() => ROUTING_NUM;
}