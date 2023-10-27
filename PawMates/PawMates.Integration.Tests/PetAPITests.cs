using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using PawMates.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Integration.Tests
{
    public class PetAPITests
    {
        private CustomWebApplicationFactory<PetAPI.Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {

            _factory?.Dispose(); // Dispose the old factory if it exists
            _factory = new CustomWebApplicationFactory<PetAPI.Program>();
            // Initialize a client to ensure the TestServer is built.
            _client = _factory.CreateClient();
            // Create database
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PawMatesContext>();
                context.Database.EnsureCreated();

                context.Add(new PetType
                {
                    Species = "Cat"
                });
                context.Add(new PetParent
                {
                    FirstName = "Udayan",
                    LastName = "Senapati",
                    PhoneNumber = "(111) 111-1111",
                    Email = "test@example.com"
                });
                context.SaveChanges();

                context.Add(new Pet
                {
                    PetParentId = 1,
                    PetTypeId = 1,
                    Age = 3,
                    Name = "Maruki",
                    Breed = "Tabby",
                    PostalCode = "11111",
                    ImageUrl= "https://en.wikipedia.org/wiki/Tabby_cat#/media/File:Cat_November_2010-1a.jpg"
                });
                context.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Delete database
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PawMatesContext>();
                context.Database.EnsureDeleted();
                context.Dispose();
            }
            _factory.Dispose();
        }

        // Get All
        [Test]
        public async Task GetPets_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/pets");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<IEnumerable<PetDTO>>(responseContent);
            // Perform more assertions with NUnit and FluentValidation here

            Assert.AreEqual(1, pets.Count());
        }

        // Get By ID
        [Test]
        public async Task GetPet_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/pets/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<PetDTO>(responseContent);

            var expectedPet = new PetDTO
            {
              ParentId = 1,
              PetTypeId = 1,
              Age = 3,
              Name = "Maruki",
              Breed = "Tabby",
              PostalCode = "11111",
              ImageUrl = "https://en.wikipedia.org/wiki/Tabby_cat#/media/File:Cat_November_2010-1a.jpg"
            };

            Assert.AreEqual(expectedPet.ParentId, pet.ParentId);
            Assert.AreEqual(expectedPet.Name, pet.Name);
            Assert.AreEqual(expectedPet.PetTypeId, pet.PetTypeId);
            Assert.AreEqual(expectedPet.Age, pet.Age);
            Assert.AreEqual(expectedPet.Breed, pet.Breed);
            Assert.AreEqual(expectedPet.PostalCode, pet.PostalCode);            
        }

        [Test]
        public async Task GetPet_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Pets/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Add Event Type
        [Test]
        public async Task AddPet_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newPet = new PetDTO
            {
                ParentId = 1,
                PetTypeId = 1,
                Age = 5,
                Name = "Whiskers",
                Breed = "Ragdoll",
                PostalCode = "22222",
                ImageUrl = "https://en.wikipedia.org/wiki/Ragdoll#/media/File:Flame_point_Ragdoll.jpg"
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newPet), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PostAsync("/api/pets", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedPet = JsonConvert.DeserializeObject<PetDTO>(responseContent);
            Assert.IsNotNull(returnedPet);
            Assert.AreEqual(newPet.ParentId, returnedPet.ParentId);
            Assert.AreEqual(newPet.PetTypeId, returnedPet.PetTypeId);
            Assert.AreEqual(newPet.Age, returnedPet.Age);
            Assert.AreEqual(newPet.Name, returnedPet.Name);           
            Assert.AreEqual(newPet.Breed, returnedPet.Breed);
            Assert.AreEqual(newPet.PostalCode, returnedPet.PostalCode);            
        }

        [Test]
        public async Task AddPet_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newPet = new PetDTO
            {
                ParentId = 1,
                PetTypeId = 2,
                Age = 2,
                Name = new string('a', 51),
                Breed = "Ragdoll",
                PostalCode = "27519",
                ImageUrl = "https://en.wikipedia.org/wiki/Ragdoll#/media/File:Flame_point_Ragdoll.jpg"
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newPet), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/pets", jsonContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // Update Event Type
        [Test]
        public async Task UpdatePet_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatePet = new PetDTO
            {
                ParentId = 1,
                PetTypeId = 1,
                Age = 5,
                Name = "Whiskers",
                Breed = "Ragdoll",
                PostalCode = "22222",
                ImageUrl = "https://en.wikipedia.org/wiki/Ragdoll#/media/File:Flame_point_Ragdoll.jpg"
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updatePet), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/pets/1", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            response = await client.GetAsync("/api/Pets/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedPet = JsonConvert.DeserializeObject<PetDTO>(responseContent);
            Assert.IsNotNull(returnedPet);
            Assert.AreEqual(updatePet.PetTypeId, returnedPet.PetTypeId);
            Assert.AreEqual(updatePet.Age, returnedPet.Age);
            Assert.AreEqual(updatePet.Name, returnedPet.Name);            
            Assert.AreEqual(updatePet.Breed, returnedPet.Breed);            
            Assert.AreEqual(updatePet.PostalCode, returnedPet.PostalCode);  

        }

        [Test]
        public async Task UpdatePet_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatePet = new PetDTO
            {
                PetTypeId = 2,
                Age = 5,
                Name = new string('a', 51),
                Breed = "Ragdoll",
                PostalCode = "22222",
                ImageUrl = "https://en.wikipedia.org/wiki/Ragdoll#/media/File:Flame_point_Ragdoll.jpg"
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updatePet), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/pets/1", jsonContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ShouldGetPetsParent()
        {
            var client = _factory.CreateClient();
            var expectedParent = new PetParentDTO
            {
                FirstName = "Udayan",
                LastName = "Senapati",
                PhoneNumber = "(111) 111-1111",
                Email = "test@example.com"
            };
            var response = await client.GetAsync("/api/pets/1/petparent");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedPetParent = JsonConvert.DeserializeObject<PetParentDTO>(responseContent);

            Assert.AreEqual(expectedParent.FirstName, returnedPetParent.FirstName);
            Assert.AreEqual(expectedParent.LastName, returnedPetParent.LastName);
            Assert.AreEqual(expectedParent.PhoneNumber, returnedPetParent.PhoneNumber);
            Assert.AreEqual(expectedParent.Email, returnedPetParent.Email);            
        }

        // Delete Pet
        [Test]
        public async Task DeletePet_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/pets/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task DeletePet_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/pets/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
