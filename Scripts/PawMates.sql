USE master;
GO
ALTER DATABASE PawMates SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE IF EXISTS PawMates;
GO
CREATE DATABASE PawMates;
GO
USE PawMates;
GO

CREATE TABLE PetParents(
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15) NULL,
    Email NVARCHAR(50) NOT NULL
);

CREATE TABLE Users(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetParentId INT NULL,
    Username NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Users_PetParents
		  FOREIGN KEY (PetParentId)
		  REFERENCES PetParents(Id)
);
GO

CREATE TABLE RestrictionTypes(
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL
);
CREATE TABLE EventTypes(
    Id INT PRIMARY KEY IDENTITY(1,1),
    RestrictionTypeId INT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_EventTypes_RestrictionTypes
		  FOREIGN KEY (RestrictionTypeId)
		  REFERENCES RestrictionTypes(Id)
);


CREATE TABLE PetTypes(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Species NVARCHAR(50) NOT NULL
);

CREATE TABLE Locations(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetTypeId INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    Street1 NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    [State] NVARCHAR(50) NOT NULL,
    PostalCode NVARCHAR(15) NOT NULL,
    PetAge INT NOT NULL,
    CONSTRAINT FK_Locations_PetTypes
		  FOREIGN KEY (PetTypeId)
		  REFERENCES PetTypes(Id)
);
GO
CREATE TABLE Pets(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetParentId INT NOT NULL,
    PetTypeId INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    Breed NVARCHAR(50) NULL,
    Age INT NOT NULL,
    PostalCode NVARCHAR(10),
    CONSTRAINT FK_Pets_PetParents
		  FOREIGN KEY (PetParentId)
		  REFERENCES PetParents(Id),
    CONSTRAINT FK_Pets_PetTypes
		  FOREIGN KEY (PetTypeId)
		  REFERENCES PetTypes(Id)
);
CREATE TABLE PlayDates(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetParentId INT NOT NULL,
    LocationId INT NOT NULL,
    EventTypeId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    CONSTRAINT FK_PlayDates_PetParents
		  FOREIGN KEY (PetParentId)
		  REFERENCES PetParents(Id),
    CONSTRAINT FK_PlayDates_Locations
		  FOREIGN KEY (LocationId)
		  REFERENCES Locations(Id),
    CONSTRAINT FK_PlayDates_EventTypes
		  FOREIGN KEY (EventTypeId)
		  REFERENCES EventTypes(Id)

);

CREATE TABLE PlayDatesPets(
    PlayDateId INT NOT NULL,
    PetId INT NOT NULL,
    CONSTRAINT PK_PlayDates_Pets
      PRIMARY KEY (PlayDateId, PetId),
    CONSTRAINT FK_PlayDates_Pets_PetId
      FOREIGN KEY (PetId)
      REFERENCES Pets(Id),
    CONSTRAINT FK_PlayDates_Pets_PlayDateId
      FOREIGN KEY (PlayDateId)
      REFERENCES PlayDates(Id)
);
