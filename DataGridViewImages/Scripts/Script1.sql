/*
    Scripts to add/update an image
*/

SELECT
    [CategoryID],
    [CategoryName],
    [Description],
    [Picture]
FROM
    [NorthWind2022].[dbo].[Categories];

------------------------------------------------------------------------------
INSERT INTO [NorthWind2022].[dbo].[Categories] (Picture)
SELECT
    BulkColumn
FROM
    OPENROWSET(BULK
                    'C:\OED\DotnetLand\VS2022\Learn2022Solution\DataGridViewImages\bin\Debug\net6.0-windows\wine.png',
                    SINGLE_BLOB
                )  AS img;


------------------------------------------------------------------------------

UPDATE
    [NorthWind2022].[dbo].[Categories]
SET
    Picture =
        (
        SELECT
            BulkColumn
        FROM
            OPENROWSET(BULK
                            N'C:\OED\DotnetLand\VS2022\Learn2022Solution\DataGridViewImages\bin\Debug\net6.0-windows\wine.png',
                            SINGLE_BLOB
                        )
                AS x
        )
WHERE
    CategoryID = 9;