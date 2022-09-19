using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OracleEntityFrameworkLibrary.Models;

[Keyless]
[Table("LINE_FLAG_CODES")]
public class LineFlagCodes
{
    
    [Column("CODE")]
    public string Code { get; set; }
    [Column("DESCR")]
    public string Description { get; set; }
    [Column("VALID_FLAG")]
    public string ValidFlag { get; set; }

    public override string ToString() => Code;

    
}