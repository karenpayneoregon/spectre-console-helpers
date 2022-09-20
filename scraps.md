
[![](assets/home-ec.png)](index.md)

```sql
SELECT TOP 1
    Id,
    FirstName,
    LastName,
    SSN,
    Pin,
    StartDate
FROM
    [OED.Lessons].dbo.Taxpayer
```

<br>

```sql
SELECT name FROM sysdatabases WHERE dbid > 6  ORDER BY name
```

```sql

--- EF.BookCatalog1

DECLARE @TableName AS NVARCHAR(50) = N'Books';


SELECT COLUMN_NAME AS ColumnName,
       ORDINAL_POSITION AS Postion,
       prop.value AS [Description]
FROM INFORMATION_SCHEMA.TABLES AS tbl
    INNER JOIN INFORMATION_SCHEMA.COLUMNS AS col
        ON col.TABLE_NAME = tbl.TABLE_NAME
    INNER JOIN sys.columns AS sc
        ON sc.object_id = OBJECT_ID(tbl.TABLE_SCHEMA + '.' + tbl.TABLE_NAME)
           AND sc.name = col.COLUMN_NAME
    LEFT JOIN sys.extended_properties prop
        ON prop.major_id = sc.object_id
           AND prop.minor_id = sc.column_id
           AND prop.name = 'MS_Description'
WHERE tbl.TABLE_NAME = @TableName
ORDER BY col.ORDINAL_POSITION;

```

</br>

**gaps**

```sql
DECLARE @Count AS INT 

SET @Count = (SELECT COUNT(*) FROM dbo.Customers);

WITH CTE
AS (SELECT 1 AS Identifier
    UNION ALL
    SELECT Identifier + 1
    FROM CTE
    WHERE Identifier <= @Count)
SELECT  TOP (@Count) *
FROM CTE
WHERE Identifier NOT IN (SELECT CustomerIdentifier FROM dbo.Customers)
ORDER BY Identifier
OPTION (MAXRECURSION 0);
```


For generating json [FOR JSON AUTO](https://docs.microsoft.com/en-us/sql/relational-databases/json/format-json-output-automatically-with-auto-mode-sql-server?view=sql-server-ver16)

```sql
SELECT CategoryId
      ,Description
  FROM [OED.Lessons].dbo.Categories
  FOR JSON AUTO
```