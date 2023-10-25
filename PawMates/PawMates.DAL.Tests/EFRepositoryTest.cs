using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

namespace PawMates.DAL.Tests
{
    public class EFRepositoryTest
    {
        private IRepository<RestrictionType> _restrictionTypeRepository;
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
            _restrictionTypeRepository = new EFRepository<RestrictionType>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        // Add
        [Test]
        public void AddRestrictionType_Success()
        {
            // Arrange
            var restrictionType = new RestrictionType { Name = "Cats Only" };

            // Act
            _restrictionTypeRepository.Add(restrictionType);
            var count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualRestrictionType = _restrictionTypeRepository.GetById(restrictionType.Id);
            Assert.AreEqual(restrictionType, actualRestrictionType.Data);
        }

        // Delete
        [Test]
        public void DeleteRestrictionType_Success()
        {
            // Arrange
            var restrictionType = new RestrictionType { Name = "Cats Only" };

            // Act
            _restrictionTypeRepository.Add(restrictionType);
            var count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Act
            var deleteResponse = _restrictionTypeRepository.Delete(restrictionType);
            count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);
            Assert.IsTrue(deleteResponse.Success);

            // Get By ID
            var actualResponse = _restrictionTypeRepository.GetById(restrictionType.Id);
            Assert.IsFalse(actualResponse.Success);
            Assert.AreEqual("RestrictionType not found.", actualResponse.Message);
        }

        [Test]
        public void DeleteRestrictionType_NotSuccess()
        {
            // Arrange
            var restrictionType = new RestrictionType { Name = "Cats Only" };

            // Act
            var response = _restrictionTypeRepository.Delete(restrictionType);
            var count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("RestrictionType not found.", response.Message);
        }

        // Update
        [Test]
        public void UpdateRestrictionType_Success()
        {
            // Arrange
            var restrictionType = new RestrictionType { Name = "Cats Only" };

            // Act
            _restrictionTypeRepository.Add(restrictionType);
            var count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualRestrictionType = _restrictionTypeRepository.GetById(restrictionType.Id);
            Assert.AreEqual(restrictionType, actualRestrictionType.Data);

            // Arrange
            restrictionType.Name = "Dogs Only";

            var updateResponse = _restrictionTypeRepository.Update(restrictionType);
            count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
            Assert.IsTrue(updateResponse.Success);

            // Get By ID
            actualRestrictionType = _restrictionTypeRepository.GetById(restrictionType.Id);
            Assert.AreEqual(restrictionType, actualRestrictionType.Data);


        }

        [Test]
        public void UpdateRestrictionType_NotSuccess()
        {
            var restrictionType = new RestrictionType { Name = "Cats Only" };
            var response = _restrictionTypeRepository.Update(restrictionType);
            var count = _restrictionTypeRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(0, count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("RestrictionType not found.", response.Message);
        }

        // GetAll Predicate
        [Test]
        public void GetPredicateRestrictionType_Success()
        {
            // Arrange
            var restrictionType = new RestrictionType { Name = "Cats Only" };

            // Act
            _restrictionTypeRepository.Add(restrictionType);
            var count = _restrictionTypeRepository.GetAll(p => p.Name == "Cats Only").Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);

            // Get By ID
            var actualRestrictionType = _restrictionTypeRepository.GetOne(p => p.Id == restrictionType.Id);
            Assert.AreEqual(restrictionType, actualRestrictionType.Data);
        }
    }
}