using PawMates.CORE.Interfaces;
using PawMates.DAL.EF;
using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.Tests
{
    public class EFPetRepositoryTests
    {
        private PawMatesContext _context;
        EFPlayDatesRepository _playDateRepository;
        EFLocationRepository _locationRepository;
        EFParentRepository _petParentRepository;
        EFPetRepository _petRepository;
        EFRepository<EventType> _eventTypeRepository;
        EFRepository<RestrictionType> _restrictionTypeRepository;
        EFRepository<PetType> _petTypeRepository;

        [SetUp]
        public void Setup()
        {
            // Setup SQLite InMemory Database options
            var options = new DbContextOptionsBuilder<PawMatesContext>()
                .UseSqlite("Filename=:memory:")
                .Options;

            // Instantiate context with InMemory Database
            _context = new PawMatesContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _playDateRepository = new EFPlayDatesRepository(_context);
            _locationRepository = new EFLocationRepository(_context);
            _petParentRepository = new EFParentRepository(_context);
            _petRepository = new EFPetRepository(_context);
            _eventTypeRepository = new EFRepository<EventType>(_context);
            _restrictionTypeRepository = new EFRepository<RestrictionType>(_context);
            _petTypeRepository = new EFRepository<PetType>(_context);


            // Seed Data
            _petParentRepository.Add(new PetParent
            {
                FirstName = "Udayan",
                LastName = "Senapati",
                PhoneNumber = "(111) 111-1111",
                Email = "test@example.com"
            });
            _petTypeRepository.Add(new PetType
            {
                Species = "Domestic Cat"
            });
            _petRepository.Add(new Pet
            {
                PetParentId = 1,
                PetTypeId = 1,
                Age = 3,
                Name = "Maruki",
                Breed = "Tabby",
                PostalCode = "11111",
            });
        }

        [TearDown] // Tear down the database, everytime, blank the database
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        [Test]
        public void ShouldGet()
        {
            string expectedName = "Maruki";
            string expectedBreed = "Tabby";

            var actual = _petRepository.GetById(1).Data;

            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedBreed, actual.Breed);
        }

        [Test]
        public void ShouldGetAll()
        {
            string expectedName = "Maruki";
            string expectedBreed = "Tabby";

            var actual = _petRepository.GetAll().Data;

            Assert.AreEqual(expectedName, actual.ToList()[0].Name);
            Assert.AreEqual(expectedBreed, actual.ToList()[0].Breed);
        }

        [Test]
        public void ShouldBeNullGetWithInvalidId()
        {

            var actual = _petRepository.GetById(10);

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldAdd()
        {
            //Arrange
            var pet= new Pet()
            {
                PetParentId = 1,
                PetTypeId = 1,
                Age = 5,
                Name = "Demo",
                Breed = "Tabby",
                PostalCode = "22222",
            };
            int id = 2;

            //Act
            var actual = _petRepository.Add(pet);
            var actualRecord = _petRepository.GetById(id);

            //Assert
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(pet.Name, actualRecord.Data.Name);
            Assert.AreEqual(pet.Breed, actualRecord.Data.Breed);
        }

        [Test]
        public void ShouldUpdate()
        {
            //Arrange        
            var updatepet = _petRepository.GetById(1).Data;
            string expectedName = "Updated";
            updatepet.Name = expectedName;
            string expectedBreed = "A";
            updatepet.Breed = expectedBreed;


            //Act

            var actual = _petRepository.Update(updatepet);
            var actualRecord = _petRepository.GetById(1).Data;

            //Assert
            Assert.AreEqual(expectedName, actualRecord.Name);
            Assert.AreEqual(expectedBreed, actualRecord.Breed);
        }

        [Test]
        public void ShouldDelete()
        {
            //Arrange
            var pet = _petRepository.GetById(1).Data;

            //Act
            var actual = _petRepository.Delete(pet);
            var actualResult = _petRepository.GetById(1);
            //
            Assert.IsTrue(actual.Success);
            Assert.IsFalse(actualResult.Success);
        }

        [Test]
        public void ShouldNotDeleteWithInvalidId()
        {
            //Arrange
            string expectedMessage = "Pet not found.";
            var pet = new Pet()
            {
                PetParentId = 1,
                PetTypeId = 1,
                Age = 5,
                Name = "Demo",
                Breed = "Tabby",
                PostalCode = "22222",
            };

            //Act
            var actual = _petRepository.Delete(pet);

            //
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(expectedMessage, actual.Message);
        }
    }
}
