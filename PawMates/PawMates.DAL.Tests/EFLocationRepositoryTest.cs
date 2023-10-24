using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

namespace PawMates.DAL.Tests
{
    public class EFLocationRepositoryTest
    {
        private EFLocationRepository _locationRepository;
        private IRepository<PetType> _petTypeRepository;
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

            _locationRepository = new EFLocationRepository(_context);
            _petTypeRepository = new EFRepository<PetType>(_context);
            //Seed Data
            _petTypeRepository.Add(new PetType
            {
                Species = "Domestic Cat"
            });
            _locationRepository.Add(new Location
            {
                PetTypeId = 1, Name = "Cat Cafe", Street1 = "123 Main Street", City = "Apex", State = "NC", PostalCode = "27502", PetAge = 2
            });
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        // GetByPetType
        [Test]
        public void GetByPetType_Success()
        {
            var petType = _petTypeRepository.GetById(1);
            Assert.IsTrue(petType.Success);

            var expected = _locationRepository.GetById(1);
            var locations = _locationRepository.GetByPetType(petType.Data);
            Assert.IsTrue(locations.Success);
            Assert.AreEqual(1, locations.Data.Count());
            Assert.AreEqual(expected.Data, locations.Data.First());
        }

        [Test]
        public void GetByPetType_NoLocations()
        {
            var petType = _petTypeRepository.GetById(1);

            Assert.IsTrue(petType.Success);
            petType.Data.Id = 2;
            var locations = _locationRepository.GetByPetType(petType.Data);
            Assert.IsTrue(locations.Success);
            Assert.AreEqual(0, locations.Data.Count());
        }

        // GetByPetAge
         [Test]
        public void GetByPetAge_Success()
        {
            var expected = _locationRepository.GetById(1);
            var locations = _locationRepository.GetByPetAge(2);
            Assert.IsTrue(locations.Success);
            Assert.AreEqual(1, locations.Data.Count());
            Assert.AreEqual(expected.Data, locations.Data.First());
        }

        [Test]
        public void GetByPetAge_NoLocations()
        {
            var locations = _locationRepository.GetByPetAge(3);
            Assert.IsTrue(locations.Success);
            Assert.AreEqual(0, locations.Data.Count());
        }
    }
}
