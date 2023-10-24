using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

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
            petParent.Data.Id = 2;
            var pets = _petParentRepository.GetPets(petParent.Data);

            Assert.IsFalse(pets.Success);
            Assert.AreEqual("PetParent not found.", pets.Message);
        }
    }
}
