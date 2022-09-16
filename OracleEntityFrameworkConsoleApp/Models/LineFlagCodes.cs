using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OracleEntityFrameworkConsoleApp.Models;

[Keyless]
[Table("LINE_FLAG_CODES")]
internal class LineFlagCodes
{
    [Column("CODE")]
    public string Code { get; set; }
    [Column("DESCR")]
    public string Description { get; set; }
    [Column("VALID_FLAG")]
    public string ValidFlag { get; set; }
}