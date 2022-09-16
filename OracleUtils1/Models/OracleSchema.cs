namespace OracleUtils1.Models;

public class OracleSchema
{
    public string ColumnName { get; set; }
    public int ColumnOrdinal { get; set; }
    public int ColumnSize { get; set; }
    public object NumericPrecision { get; set; }
    public object NumericScale { get; set; }
    public object IsUnique { get; set; }
    public object IsKey { get; set; }
    public bool IsRowID { get; set; }
    public string BaseColumnName { get; set; }
    public object BaseSchemaName { get; set; }
    public string BaseTableName { get; set; }
    public string DataType { get; set; }
    public int ProviderType { get; set; }
    public bool AllowDBNull { get; set; }
    public bool IsAliased { get; set; }
    public bool? IsByteSemantic { get; set; }
    public bool IsExpression { get; set; }
    public bool IsHidden { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsLong { get; set; }
    public object UdtTypeName { get; set; }
    public bool IsAutoIncrement { get; set; }
    public bool IsIdentity { get; set; }
    public object IdentityType { get; set; }
    public bool IsValueLob { get; set; }
}