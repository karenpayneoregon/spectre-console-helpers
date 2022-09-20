SELECT TOP 1
    Id,
    FirstName,
    LastName,
    SSN,
    Pin,
    StartDate
FROM
    [OED.Lessons].dbo.Taxpayer
ORDER BY NEWID()