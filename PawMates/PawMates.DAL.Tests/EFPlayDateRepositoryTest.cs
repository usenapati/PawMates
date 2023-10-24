using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

namespace PawMates.DAL.Tests
{
    public class EFPlayDateRepositoryTest
    {
        EFPlayDateRepository _playDateRepository;
        EFLocationRepository _locationRepository;
        EFParentRepository _petParentRepository;
        EFRepository<Pet> _petRepository;
        EFRepository<EventType> _eventTypeRepository;
        EFRepository<RestrictionType> _restrictionTypeRepository;
        EFRepository<PetType> _petTypeRepository;




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

            _playDateRepository = new EFPlayDateRepository(_context);
            _locationRepository = new EFLocationRepository(_context);
            _petParentRepository = new EFParentRepository(_context);
            _petRepository = new EFRepository<Pet>(_context);
            _eventTypeRepository = new EFRepository<EventType>(_context);
            _restrictionTypeRepository = new EFRepository<RestrictionType>(_context);
            _petTypeRepository = new EFRepository<PetType>(_context);

            // Seed Data
            _petParentRepository.Add(new PetParent
            {
                FirstName = "Udayan", LastName = "Senapati", PhoneNumber = "(111) 111-1111", Email = "test@example.com"
            });
            _petTypeRepository.Add(new PetType
            {
                Species = "Domestic Cat"
            });
            _petRepository.Add(new Pet
            {
                PetParentId = 1, PetTypeId = 1, Age = 3, Name = "Maruki", Breed = "Tabby", PostalCode = "11111",
            });
            _locationRepository.Add(new Location
            {
                PetTypeId = 1, Name = "Cat Cafe", Street1 = "123 Main Street", City = "Apex", State = "NC", PostalCode = "27502", PetAge = 2
            });
            _restrictionTypeRepository.Add(new RestrictionType
            {
                Name = "Pet Type Restriction: Cats Only"
            });
            _eventTypeRepository.Add(new EventType
            {
                Name = "Purrfect Picnic", 
                Description = "An indoor play date for cats and their owners, featuring cozy blankets and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.",
                RestrictionTypeId = 1

            });
            _playDateRepository.Add(new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 10, 31, 15, 0, 0),
                EndTime = new DateTime(2023, 10, 31, 17, 0, 0),
            });
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.CloseConnection();
        }

        // GetByLocation
        [Test]
        public void GetByLocation_Success()
        {
            var location = _locationRepository.GetById(1);
            Assert.IsTrue(location.Success);

            var expected = _playDateRepository.GetById(1);
            var playDates = _playDateRepository.GetByLocation(location.Data);
            Assert.IsTrue(playDates.Success);
            Assert.AreEqual(1, playDates.Data.Count());
            Assert.AreEqual(expected.Data, playDates.Data.First());
        }

        [Test]
        public void GetByLocation_NotSuccess()
        {
            var location = _locationRepository.GetById(1);
            Assert.IsTrue(location.Success);
            location.Data.Id = 2;

            var playDates = _playDateRepository.GetByLocation(location.Data);
            Assert.IsFalse(playDates.Success);
            Assert.AreEqual("Location not found.", playDates.Message);
        }

        // GetByDate
        [Test]
        public void GetByDate_Success()
        {
            var expected = _playDateRepository.GetById(1);
            var playDates = _playDateRepository.GetByDate(new DateTime(2023, 10, 31));
            Assert.IsTrue(playDates.Success);
            Assert.AreEqual(1, playDates.Data.Count());
            Assert.AreEqual(expected.Data, playDates.Data.First());
        }

        // AddPet
        [Test]
        public void AddPet_Success()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPet(1, pet.Data);
            Assert.IsTrue(addResponse.Success);

            var getResponse = _playDateRepository.GetById(1);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(pet.Data, getResponse.Data.Pets.First());
        }

        [Test]
        public void AddPet_NotSuccess()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPet(2, pet.Data);
            Assert.IsFalse(addResponse.Success);
            Assert.AreEqual("PlayDate not found.", addResponse.Message);


            pet.Data.Id = 2;
            addResponse = _playDateRepository.AddPet(1, pet.Data);
            Assert.IsFalse(addResponse.Success);
            Assert.AreEqual("Could not add Pet to Play Date.", addResponse.Message);

        }

        // RemovePet
        [Test]
        public void RemovePet_Success()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPet(1, pet.Data);
            Assert.IsTrue(addResponse.Success);

            var getResponse = _playDateRepository.GetById(1);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(pet.Data, getResponse.Data.Pets.First());

            var removeResponse = _playDateRepository.RemovePet(1, pet.Data);
            Assert.IsTrue(removeResponse.Success);
            Assert.AreEqual(0, getResponse.Data.Pets.Count);
        }

        [Test]
        public void RemovePet_NotSuccess()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var removeResponse = _playDateRepository.RemovePet(2, pet.Data);
            Assert.IsFalse(removeResponse.Success);
            Assert.AreEqual("PlayDate not found.", removeResponse.Message);

            pet.Data.Id = 2;
            removeResponse = _playDateRepository.RemovePet(1, pet.Data);
            Assert.IsFalse(removeResponse.Success);
            Assert.AreEqual("Could not remove Pet from Play Date.", removeResponse.Message);
        }
    }
}
