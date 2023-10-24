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
FROM Locations l
LEFT JOIN PlayDates pd ON pd.LocationId = l.Id
LEFT JOIN PetTypes p ON p.Id = l.PetTypeId
LEFT JOIN EventTypes e ON e.Id = pd.EventTypeId
WHERE p.Species = @Species
ORDER BY pd.StartTime ASC;
GO

CREATE PROCEDURE GetPlayDatesByLocation
@PostalCode NVARCHAR
AS
SELECT l.[Name], p.Species, e.[Name], pd.StartTime, pd.EndTime, l.City, l.[State], l.PostalCode
FROM PlayDates pd
LEFT JOIN Locations l ON l.Id = pd.LocationId
LEFT JOIN PetTypes p ON p.Id = l.PetTypeId
LEFT JOIN EventTypes e ON e.Id = pd.EventTypeId
WHERE l.PostalCode = @PostalCode
ORDER BY pd.StartTime ASC;
GO