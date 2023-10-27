using Microsoft.EntityFrameworkCore;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;
using System.Net;

namespace PawMates.DAL.Tests
{
    public class EFParentRepositoryTest
    {
        private EFParentRepository _petParentRepository;
        private IRepository<PetType> _petTypeRepository;
        private IRepository<Pet> _petRepository;
        private PawMatesContext _context;

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

            // Instantiate repository with InMemory Database
            _petParentRepository = new EFParentRepository(_context);
            _petTypeRepository = new EFRepository<PetType>(_context);
            _petRepository = new EFRepository<Pet>(_context);

            // Add Seed Data
            _petParentRepository.Add(new PetParent
            {
                 FirstName = "Udayan", LastName = "Senapati", PhoneNumber = "(111) 111-1111", Email = "test@example.com"
            });
            _petParentRepository.Add(new PetParent
            {
                Id = 55,
                FirstName = "Chester",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "cmac@tester.com"
            });
            _petTypeRepository.Add(new PetType
            {
                Species = "Domestic Cat"
            });
            _petRepository.Add(new Pet
            {
                PetParentId = 1, PetTypeId = 1, Age = 3, Name = "Maruki", Breed = "Tabby", PostalCode = "11111",
            });
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        [Test]
        public void GetPets_Success()
        {
            var petParent = _petParentRepository.GetById(1);
            var pets = _petParentRepository.GetPets(petParent.Data);

            Assert.IsTrue(pets.Success);
            Assert.AreEqual(1, pets.Data.Count());
            var pet = _petRepository.GetOne(p => p.Id == 1);
            Assert.IsTrue(pet.Success);
            Assert.AreEqual(pet.Data, pets.Data.First());
        }

        [Test]
        public void GetPets_NotSuccess()
        {
            var petParent = _petParentRepository.GetById(1);
            petParent.Data.Id = 999;
            var pets = _petParentRepository.GetPets(petParent.Data);

            Assert.IsFalse(pets.Success);
            Assert.AreEqual("Pet parent could not found.\n", pets.Message);
        }

        [Test]
        public void ShouldAddPet()
        {
            //Arrange
            int parentId = 1;
            var expected = new Pet
            {
                Id = 2,
                PetParentId = 1,
                PetTypeId = 1,
                Age = 3,
                Name = "Wren",
                Breed = "Tabby",
                PostalCode = "90210",
            };
            //Act
            var actual = _petParentRepository.AddPetToParent(parentId, expected.Id);
            //Assert
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void ShouldRemovePet()
        {
            //Arrange
            int parentId = 1;
            var expected = new Pet
            {
                Id = 1,
                PetParentId = 1,
                PetTypeId = 1,
                Age = 3,
                Name = "Wren",
                Breed = null,
                PostalCode = "90210",
            };
            //Act
            var actual = _petParentRepository.DeletePetFromParent(parentId, expected.Id);
            //Assert
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void ShouldGetAll()
        {
            //Arrange
            var expected = 2;
            //Act
            var actual = _petParentRepository.GetAll().Data.Count();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldGetById()
        {
            //Arrange
            int id = 55;
            var expected = new PetParent
            {
                Id = 55,
                FirstName = "Chester",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "cmac@tester.com"
            };
            //Act
            var actual = _petParentRepository.GetById(id).Data;
            //Assert
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Email, actual.Email);
        }

        [Test]
        public void ShouldNotGetByIdThatDoesNotExist()
        {
            //Arrange
            int id = 999;
            //Act
            var test = _petParentRepository.GetById(id);
            //Assert
            Assert.IsFalse(test.Success);
        }

        [Test]
        public void ShouldAdd()
        {
            //Arrange
            var expected = new PetParent
            {
                FirstName = "Test",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "tmac@tester.com"
            };
            //Act
            var actual = _petParentRepository.Add(expected);
            //Assert
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.FirstName, actual.Data.FirstName);
            Assert.AreEqual(expected.LastName, actual.Data.LastName);
            Assert.AreEqual(expected.PhoneNumber, actual.Data.PhoneNumber);
            Assert.AreEqual(expected.Email, actual.Data.Email);
        }

        [Test]
        public void ShouldUpdate()
        {
            //Arrange
            var expected = _petParentRepository.GetById(55).Data;
            expected.FirstName = "String";
            //Act
            var actual = _petParentRepository.Update(expected);
            //Assert
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void ShouldNotUpdateParentThatDoesNotExist()
        {
            //Arrange
            var expected = new PetParent
            {
                Id = 999,
                FirstName = "Test",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "tmac@tester.com"
            };
            //Act
            var actual = _petParentRepository.Update(expected);
            //Assert
            Assert.IsFalse(actual.Success);
        }

        [Test]
        public void ShouldDelete()
        {
            //Arrange
            var expected = new PetParent
            {
                Id = 55,
                FirstName = "Chester",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "cmac@tester.com"
            };
            //Act
            var actual = _petParentRepository.Delete(expected);
            //Assert
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void ShouldNotDeleteParentThatDoesNotExist()
        {
            //Arrange
            var expected = new PetParent
            {
                Id = 999,
                FirstName = "Test",
                LastName = "McTester",
                PhoneNumber = "123-456-7890",
                Email = "tmac@tester.com"
            };
            //Act
            var actual = _petParentRepository.Delete(expected);
            //Assert
            Assert.IsFalse(actual.Success);
        }
    }
}
