using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

namespace PawMates.DAL.Tests
{
    public class EFPlayDateRepositoryTest
    {
        EFPlayDatesRepository _playDateRepository;
        EFLocationRepository _locationRepository;
        EFParentRepository _petParentRepository;
        EFPetRepository _petRepository;
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
            _locationRepository.Add(new Location
            {
                PetTypeId = 1,
                Name = "Cat Cafe",
                Street1 = "123 Main Street",
                City = "Apex",
                State = "NC",
                PostalCode = "27502",
                PetAge = 2
            });
            _restrictionTypeRepository.Add(new RestrictionType
            {
                Name = "Pet Type Restriction: Cats Only"
            });
            _eventTypeRepository.Add(new EventType
            {
                Name = "Purrfect Picnic",
                Description = "An indoor play date for cats and their owners, featuring cozy blankets and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.",
                RestrictionTypeId = 1,
                PetTypeId = 1

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

        // Add and GetAll
        [Test]
        public void AddPlayDate_Success()
        {
            // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            _playDateRepository.Add(playDate);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(2, count);

            // Get By ID
            var actualPlayDate = _playDateRepository.GetById(playDate.Id);
            Assert.AreEqual(playDate, actualPlayDate.Data);

        }
        
        
        // Delete
        [Test]
        public void DeletePlayDate_Success()
        {
             // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            _playDateRepository.Add(playDate);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(2, count);

            // Get By ID
            var actualPlayDate = _playDateRepository.GetById(playDate.Id);
            Assert.AreEqual(playDate, actualPlayDate.Data);

            // Act
            var deleteResponse = _playDateRepository.Delete(playDate);
            count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
            Assert.IsTrue(deleteResponse.Success);

            var actualResponse = _playDateRepository.GetById(playDate.Id);
            Assert.IsFalse(actualResponse.Success);
            Assert.AreEqual("Play Date not found.", actualResponse.Message);
        }

        [Test]
        public void DeletePlayDate_Failure()
        {
             // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            var response = _playDateRepository.Delete(playDate);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Play Date not found.", response.Message);
        }

        [Test]
        public void DeletePlayDateWithPets_Success()
        {
             // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            _playDateRepository.Add(playDate);
            _playDateRepository.AddPetToPlayDate(playDate.Id,1);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(2, count);

            // Get By ID
            var actualPlayDate = _playDateRepository.GetById(playDate.Id);
            Assert.AreEqual(playDate, actualPlayDate.Data);

            // Act
            var deleteResponse = _playDateRepository.Delete(playDate);
            count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
            Assert.IsTrue(deleteResponse.Success);
        }

        // Update
        [Test]
        public void UpdatePlayDate_Success()
        {
            // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            _playDateRepository.Add(playDate);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(2, count);

            // Get By ID
            var actualPlayDate = _playDateRepository.GetById(playDate.Id);
            Assert.AreEqual(playDate, actualPlayDate.Data);

            // Arrange
            playDate.StartTime = new DateTime(2023, 11, 8, 15, 0, 0);
            playDate.EndTime = new DateTime(2023, 11, 8, 17, 0, 0);

            var updateResponse = _playDateRepository.Update(playDate);
            count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(2, count);
            Assert.IsTrue(updateResponse.Success);

            // Get By ID
            actualPlayDate = _playDateRepository.GetById(playDate.Id);
            Assert.AreEqual(playDate, actualPlayDate.Data);
        }
        
        [Test]
        public void UpdatePlayDate_Failure()
        {
            // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };
            var updateResponse = _playDateRepository.Update(playDate);
            var count = _playDateRepository.GetAll().Data.ToList().Count;

            // Assert
            Assert.AreEqual(1, count);
            Assert.IsFalse(updateResponse.Success);
            Assert.AreEqual("Play Date not found.", updateResponse.Message);

        }

        // GetPred[Test]
        public void GetPredicatePlayDate_Success()
        {
            // Arrange
            var playDate = new PlayDate
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };

            // Act
            _playDateRepository.Add(playDate);
            var count = _playDateRepository.GetAll(p => p.LocationId == 1).Data.ToList().Count; 
            
            // Assert
            Assert.AreEqual(1, count);

            // Get One
            var getResponse = _playDateRepository.GetOne(p => p.PetParentId == 1);
            Assert.AreEqual(playDate, getResponse.Data);
        }

        // Validate
        public void ValidatePlayDate_Failure()
        {

        }
        

        // AddPet
        [Test]
        public void AddPet_Success()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPetToPlayDate(1, pet.Data.Id);
            Assert.IsTrue(addResponse.Success);

            var getResponse = _playDateRepository.GetById(1);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(pet.Data, getResponse.Data.Pets.First());

            addResponse = _playDateRepository.AddPetToPlayDate(1, pet.Data.Id);
            Assert.IsTrue(addResponse.Success);

            getResponse = _playDateRepository.GetById(1);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(1, getResponse.Data.Pets.Count);
        }

        [Test]
        public void AddPet_NotSuccess()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPetToPlayDate(2, pet.Data.Id);
            Assert.IsFalse(addResponse.Success);
            Assert.AreEqual("Play Date not found.", addResponse.Message);


            pet.Data.Id = 2;
            addResponse = _playDateRepository.AddPetToPlayDate(1, pet.Data.Id);
            Assert.IsFalse(addResponse.Success);
            Assert.AreEqual("Pet 2 does not exist.", addResponse.Message);

        }

        // RemovePet
        [Test]
        public void RemovePet_Success()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var addResponse = _playDateRepository.AddPetToPlayDate(1, pet.Data.Id);
            Assert.IsTrue(addResponse.Success);

            var getResponse = _playDateRepository.GetById(1);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(pet.Data, getResponse.Data.Pets.First());

            var removeResponse = _playDateRepository.DeletePetFromPlayDate(1, pet.Data.Id);
            Assert.IsTrue(removeResponse.Success);
            Assert.AreEqual(0, getResponse.Data.Pets.Count);

            removeResponse = _playDateRepository.DeletePetFromPlayDate(1, pet.Data.Id);
            Assert.IsTrue(removeResponse.Success);
            Assert.AreEqual(0, getResponse.Data.Pets.Count);
        }

        [Test]
        public void RemovePet_NotSuccess()
        {
            var pet = _petRepository.GetById(1);
            Assert.IsTrue(pet.Success);


            var removeResponse = _playDateRepository.DeletePetFromPlayDate(2, pet.Data.Id);
            Assert.IsFalse(removeResponse.Success);
            Assert.AreEqual("Play Date not found.", removeResponse.Message);

            pet.Data.Id = 2;
            removeResponse = _playDateRepository.DeletePetFromPlayDate(1, pet.Data.Id);
            Assert.IsFalse(removeResponse.Success);
            Assert.AreEqual("Pet 2 does not exist.", removeResponse.Message);
        }
    }
}
