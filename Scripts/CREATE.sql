USE PawMates;
GO

CREATE PROCEDURE GetTopPetParents
AS
SELECT TOP 3 COUNT(pp.Id) AS PlayDateCount, pp.FirstName AS FirstName, pp.LastName AS LastName
FROM PlayDates pd
LEFT JOIN PetParents pp ON pp.Id = pd.PetParentId
GROUP BY pp.FirstName, pp.LastName
ORDER BY COUNT(pp.Id) DESC;
GO

CREATE PROCEDURE GetPlayDatesBySpecies
@Species NVARCHAR
AS
SELECT p.Species, e.[Name], pd.StartTime, pd.EndTime, l.City, l.[State], l.PostalCode
FROM PlayDates pd
INNER JOIN Locations l ON l.Id = pd.LocationId
INNER JOIN EventTypes e ON e.Id = pd.EventTypeId
INNER JOIN PetTypes AS p ON p.Id = e.PetTypeId
WHERE p.Species = @Species
ORDER BY pd.StartTime ASC;
GO

CREATE PROCEDURE GetPlayDatesByLocation
@PostalCode NVARCHAR
AS
SELECT l.[Name], p.Species, e.[Name], pd.StartTime, pd.EndTime, l.City, l.[State], l.PostalCode
FROM PlayDates pd
INNER JOIN Locations l ON l.Id = pd.LocationId
INNER JOIN PetTypes p ON p.Id = l.PetTypeId
INNER JOIN EventTypes e ON e.Id = pd.EventTypeId
WHERE l.PostalCode = @PostalCode
ORDER BY pd.StartTime ASC;
GO