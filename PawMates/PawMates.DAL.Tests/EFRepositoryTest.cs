using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;
using PawMates.DAL;

namespace PawMates.DAL.Tests
{
    public class EFRepositoryTest
    {
        private IRepository<PetParent> _petParentRepository;
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
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        // Add
        [Test]
        public void AddUser_Success()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };

            // Act
            _petParentRepository.Add(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualUser = _petParentRepository.GetById(petParent.Id);
            Assert.AreEqual(petParent, actualUser.Data);
        }

        // Delete
        [Test]
        public void DeleteUser_Success()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };

            // Act
            _petParentRepository.Add(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Act
            _petParentRepository.Delete(petParent);
            count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);

            // Get By ID
            var actualResponse = _petParentRepository.GetById(petParent.Id);
            Assert.IsFalse(actualResponse.Success);
            Assert.AreEqual("PetParent not found.", actualResponse.Message);
        }

        [Test]
        public void DeleteUser_NotSuccess()
        {
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };
            var response = _petParentRepository.Delete(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("PetParent not found.", response.Message);
        }

        // Update
        [Test]
        public void UpdateUser_Success()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };

            // Act
            _petParentRepository.Add(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualUser = _petParentRepository.GetById(petParent.Id);
            Assert.AreEqual(petParent, actualUser.Data);

            // Arrange
            petParent.FirstName = "Jane";

            _petParentRepository.Update(petParent);


            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            actualUser = _petParentRepository.GetById(petParent.Id);
            Assert.AreEqual(petParent, actualUser.Data);


        }

        [Test]
        public void UpdateUser_NotSuccess()
        {
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };
            var response = _petParentRepository.Update(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("PetParent not found.", response.Message);
        }

        // GetAll Predicate
        [Test]
        public void GetPred_Success()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com" };

            // Act
            _petParentRepository.Add(petParent);
            var count = _petParentRepository.GetAll(p => p.FirstName == "John").Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualUser = _petParentRepository.GetOne(p => p.Id == petParent.Id);
            Assert.AreEqual(petParent, actualUser.Data);
        }
    }
}