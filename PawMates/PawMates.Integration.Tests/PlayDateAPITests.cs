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
    public class PlayDateAPITests
    {
        private CustomWebApplicationFactory<PlayDateAPI.Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {

            _factory?.Dispose(); // Dispose the old factory if it exists
            _factory = new CustomWebApplicationFactory<PlayDateAPI.Program>();
            // Initialize a client to ensure the TestServer is built.
            _client = _factory.CreateClient();
            // Create database
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PawMatesContext>();
                context.Database.EnsureCreated();

                context.Add(new PetParent
                {
                    Id = 1,
                    FirstName = "Chester",
                    LastName = "McTester",
                    Email = "cmac@chester.com",
                    PhoneNumber = "123-456-7890"
                });
                context.SaveChanges();

                context.Add(new PetType
                {
                    Species = "Cat"
                });
                context.Add(new PetType
                {
                    Species = "Dog"
                });
                context.SaveChanges();

                var pet1 = new Pet
                {
                    PetParentId = 1,
                    PetTypeId = 1,
                    Age = 3,
                    Name = "Maruki",
                    Breed = "Tabby",
                    PostalCode = "11111",
                };

                context.Add(pet1);
                context.Add(new Pet
                {
                    PetParentId = 1,
                    PetTypeId = 1,
                    Age = 5,
                    Name = "Whiskers",
                    Breed = "Ragdoll",
                    PostalCode = "22222",
                });

                context.Add(new Location
                {
                    PetTypeId = 1,
                    Name = "Cat Cafe",
                    Street1 = "123 Main Street",
                    City = "Apex",
                    State = "NC",
                    PostalCode = "27502",
                    PetAge = 2
                });

                context.Add(new RestrictionType
                {
                    Name = "Pet Type Restriction: Cats Only"
                });
                context.SaveChanges();

                context.Add(new EventType
                {
                    Name = "Purrfect Picnic",
                    Description = "An indoor play date for cats and their owners, featuring cozy blankets and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.",
                    RestrictionTypeId = 1,
                    PetTypeId = 1

                });
                var playDate1 = new PlayDate
                {
                    PetParentId = 1,
                    LocationId = 1,
                    EventTypeId = 1,
                    StartTime = new DateTime(2023, 10, 31, 15, 0, 0),
                    EndTime = new DateTime(2023, 10, 31, 17, 0, 0),
                    Pets = new List<Pet>()
                };
                context.Add(playDate1);
                playDate1.Pets.Add(pet1);
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
        public async Task GetPlayDates_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/PlayDates");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var PlayDates = JsonConvert.DeserializeObject<IEnumerable<PlayDateDTO>>(responseContent);
            // Perform more assertions with NUnit and FluentValidation here

            Assert.AreEqual(1, PlayDates.Count());
        }

        // Get By ID
        [Test]
        public async Task GetPlayDate_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/PlayDates/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var PlayDate = JsonConvert.DeserializeObject<PlayDateDTO>(responseContent);

            var expectedPlayDate = new PlayDateDTO
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 10, 31, 15, 0, 0),
                EndTime = new DateTime(2023, 10, 31, 17, 0, 0),
            };

            Assert.AreEqual(expectedPlayDate.PetParentId, PlayDate.PetParentId);
            Assert.AreEqual(expectedPlayDate.LocationId, PlayDate.LocationId);
            Assert.AreEqual(expectedPlayDate.EventTypeId, PlayDate.EventTypeId);
            Assert.AreEqual(expectedPlayDate.StartTime, PlayDate.StartTime);
            Assert.AreEqual(expectedPlayDate.EndTime, PlayDate.EndTime);
            
        }

        [Test]
        public async Task GetPlayDate_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/PlayDates/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task GetPetsOnPlayDate_ReturnsSuccess()
        {
            //Arrange
            var client = _factory.CreateClient();
            var expectedPet = new PetDTO
            {
                ParentId = 1,
                PetTypeId = 1,
                Age = 3,
                Name = "Maruki",
                Breed = "Tabby",
                PostalCode = "11111",
            };

            // Act
            var response = await client.GetAsync("/api/PlayDates/1/pets");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<IEnumerable<PetDTO>>(responseContent).ToList();

            Assert.AreEqual(expectedPet.ParentId, pets[0].ParentId);
            Assert.AreEqual(expectedPet.Name, pets[0].Name);
            Assert.AreEqual(expectedPet.PetTypeId, pets[0].PetTypeId);
            Assert.AreEqual(expectedPet.Age, pets[0].Age);
            Assert.AreEqual(expectedPet.Breed, pets[0].Breed);
            Assert.AreEqual(expectedPet.PostalCode, pets[0].PostalCode);

        }

        // Add PlayDate
        [Test]
        public async Task AddPlayDate_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newPlayDate = new PlayDateDTO
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newPlayDate), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PostAsync("/api/PlayDates", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedPlayDate = JsonConvert.DeserializeObject<PlayDateDTO>(responseContent);
            Assert.IsNotNull(returnedPlayDate);
            Assert.AreEqual(newPlayDate.PetParentId, returnedPlayDate.PetParentId);
            Assert.AreEqual(newPlayDate.LocationId, returnedPlayDate.LocationId);
            Assert.AreEqual(newPlayDate.EventTypeId, returnedPlayDate.EventTypeId);
            Assert.AreEqual(newPlayDate.StartTime, returnedPlayDate.StartTime);
            Assert.AreEqual(newPlayDate.EndTime, returnedPlayDate.EndTime);
            
        }

        [Test]
        public async Task AddPlayDate_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newPlayDate = new PlayDateDTO
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 14, 0, 0),

            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newPlayDate), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/PlayDates", jsonContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // Update Event Type
        [Test]
        public async Task UpdatePlayDate_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatePlayDate = new PlayDateDTO
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 17, 0, 0),
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updatePlayDate), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/PlayDates/1", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            response = await client.GetAsync("/api/PlayDates/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedPlayDate = JsonConvert.DeserializeObject<PlayDateDTO>(responseContent);
            Assert.IsNotNull(returnedPlayDate);

            Assert.AreEqual(updatePlayDate.StartTime, returnedPlayDate.StartTime);
            Assert.AreEqual(updatePlayDate.EndTime, returnedPlayDate.EndTime);

        }

        [Test]
        public async Task UpdatePlayDate_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updatePlayDate = new PlayDateDTO
            {
                PetParentId = 1,
                LocationId = 1,
                EventTypeId = 1,
                StartTime = new DateTime(2023, 11, 1, 15, 0, 0),
                EndTime = new DateTime(2023, 11, 1, 14, 0, 0)
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updatePlayDate), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/PlayDates/1", jsonContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }



        // Delete PlayDate
        [Test]
        public async Task DeletePlayDate_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/PlayDates/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task DeletePlayDate_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/PlayDates/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddPetToPlayDate()
        {
            // Arrange
            var client = _factory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(new object{ }), Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("/api/PlayDates/1/pets/2", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Test]
        public async Task DeletePetFromPlayDate()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/PlayDates/1/pets/1");

            // Assert
            response.EnsureSuccessStatusCode();

        }
    }
}
