using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using PawMates.DAL;
using System.Net;
using System.Text;

namespace PawMates.Integration.Tests
{
    public class EventTypeAPITests
    {
        private CustomWebApplicationFactory<EventTypeAPI.Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {

            _factory?.Dispose(); // Dispose the old factory if it exists
            _factory = new CustomWebApplicationFactory<EventTypeAPI.Program>();
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
                context.Add(new PetType
                {
                    Species = "Dog"
                });
                context.Add(new EventType
                {
                    PetTypeId = 1,
                    Name = "Purrfect Picnic in the Park",
                    Description = "An outdoor play date for cats and their owners, featuring cozy blankets, shade, and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.",
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
        public async Task GetEventTypes_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/EventType");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var eventTypes = JsonConvert.DeserializeObject<IEnumerable<EventTypeDTO>>(responseContent);
            // Perform more assertions with NUnit and FluentValidation here

            Assert.AreEqual(1, eventTypes.Count());
        }

        // Get By ID
        [Test]
        public async Task GetEventType_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/EventType/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var eventType = JsonConvert.DeserializeObject<EventTypeDTO>(responseContent);

            var expectedEventType = new EventTypeDTO
            {
                PetTypeId = 1,
                Name = "Purrfect Picnic in the Park",
                Description = "An outdoor play date for cats and their owners, featuring cozy blankets, shade, and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.",

            };

            Assert.AreEqual(expectedEventType.PetTypeId, eventType.PetTypeId);
            Assert.AreEqual(expectedEventType.Name, eventType.Name);
            Assert.AreEqual(expectedEventType.Description, eventType.Description);
        }

        [Test]
        public async Task GetEventType_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/EventType/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Add Event Type
        [Test]
        public async Task AddEventType_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newEventType = new EventTypeDTO
            {
                PetTypeId = 2,
                Name = "Bark 'n' Splash Day",
                Description = "A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.",
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newEventType), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PostAsync("/api/EventType", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedEventType = JsonConvert.DeserializeObject<EventTypeDTO>(responseContent);
            Assert.IsNotNull(returnedEventType);
            Assert.AreEqual(newEventType.Name, returnedEventType.Name);
            Assert.AreEqual(newEventType.Description, returnedEventType.Description);
            Assert.AreEqual(newEventType.PetTypeId, returnedEventType.PetTypeId);
        }

        [Test]
        public async Task AddEventType_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newEventType = new EventTypeDTO
            {
                PetTypeId = 2,
                Name = new string('x', 51),
                Description = "A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.",
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newEventType), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/EventType", jsonContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // Update Event Type
        [Test]
        public async Task UpdateEventType_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateEventType = new EventTypeDTO
            {
                PetTypeId = 2,
                Name = "Bark 'n' Splash Day",
                Description = "A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.",
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateEventType), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/EventType/1", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            response = await client.GetAsync("/api/EventType/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedEventType = JsonConvert.DeserializeObject<EventTypeDTO>(responseContent);
            Assert.IsNotNull(returnedEventType);
            Assert.AreEqual(updateEventType.Name, returnedEventType.Name);
            Assert.AreEqual(updateEventType.Description, returnedEventType.Description);
            Assert.AreEqual(updateEventType.PetTypeId, returnedEventType.PetTypeId);

        }

        [Test]
        public async Task UpdateEventType_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateEventType = new EventTypeDTO
            {
                PetTypeId = 2,
                Name = new string('x', 51),
                Description = "A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.",
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateEventType), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/EventType/1", jsonContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Test]
        public async Task UpdateEventType_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateEventType = new EventTypeDTO
            {
                PetTypeId = 2,
                Name = "Bark 'n' Splash Day",
                Description = "A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.",
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateEventType), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/EventType/0", jsonContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Delete Event Type
        [Test]
        public async Task DeleteEventType_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
           
            // Act
            var response = await client.DeleteAsync("/api/EventType/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task DeleteEventType_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.DeleteAsync("/api/EventType/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
