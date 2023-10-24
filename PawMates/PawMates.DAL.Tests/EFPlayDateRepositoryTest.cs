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

        }

        [Test]
        public void GetByLocation_NotSuccess()
        {

        }

        // GetByDate
        [Test]
        public void GetByDate_Success()
        {

        }

        // AddPet
        [Test]
        public void AddPet_Success()
        {

        }

        [Test]
        public void AddPet_NotSuccess()
        {

        }

        // RemovePet
        [Test]
        public void RemovePet_Success()
        {

        }

        [Test]
        public void RemovePet_NotSuccess()
        {

        }
    }
}
