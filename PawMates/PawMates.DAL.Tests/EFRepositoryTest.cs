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
            _petParentRepository = new EFRepository<PetParent>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        [Test]
        public void AddUser_ShouldIncreaseCount()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com"};

            // Act
            _petParentRepository.Add(petParent);
            var count = _petParentRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public void DeleteUser_ShouldDecreaseCount()
        {
            // Arrange
            var petParent = new PetParent { FirstName = "John", LastName = "Doe", PhoneNumber = "(111) 111-1111", Email = "johndoe@example.com"};

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
        }
    }
}