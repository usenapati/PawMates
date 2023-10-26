using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using PawMates.DAL;
using System.Net;
using System.Text;

namespace PawMates.Integration.Tests
{
    public class LocationAPITests
    {
        private CustomWebApplicationFactory<LocationAPI.Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {

            _factory?.Dispose(); // Dispose the old factory if it exists
            _factory = new CustomWebApplicationFactory<LocationAPI.Program>();
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
        public async Task GetLocations_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Locations");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var eventTypes = JsonConvert.DeserializeObject<IEnumerable<LocationDTO>>(responseContent);
            // Perform more assertions with NUnit and FluentValidation here

            Assert.AreEqual(1, eventTypes.Count());
        }

        // Get By ID
        [Test]
        public async Task GetLocation_ReturnsExpected()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Locations/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationDTO>(responseContent);

            var expectedLocation = new LocationDTO
            {
                PetTypeId = 1,
                Name = "Cat Cafe",
                Street1 = "123 Main Street",
                City = "Apex",
                State = "NC",
                PostalCode = "27502",
                PetAge = 2
            };

            Assert.AreEqual(expectedLocation.PetTypeId, location.PetTypeId);
            Assert.AreEqual(expectedLocation.Name, location.Name);
            Assert.AreEqual(expectedLocation.Street1, location.Street1);
            Assert.AreEqual(expectedLocation.City, location.City);
            Assert.AreEqual(expectedLocation.State, location.State);
            Assert.AreEqual(expectedLocation.PostalCode, location.PostalCode);
            Assert.AreEqual(expectedLocation.PetAge, location.PetAge);
        }

        [Test]
        public async Task GetLocation_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Locations/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Add Event Type
        [Test]
        public async Task AddLocation_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newLocation = new LocationDTO
            {
                PetTypeId = 2,
                Name = "Dog Park",
                Street1 = "123 Main Street",
                City = "Cary",
                State = "NC",
                PostalCode = "27519",
                PetAge = 2
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newLocation), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PostAsync("/api/Locations", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedLocation = JsonConvert.DeserializeObject<LocationDTO>(responseContent);
            Assert.IsNotNull(returnedLocation);
            Assert.AreEqual(newLocation.PetTypeId, returnedLocation.PetTypeId);
            Assert.AreEqual(newLocation.Name, returnedLocation.Name);
            Assert.AreEqual(newLocation.Street1, returnedLocation.Street1);
            Assert.AreEqual(newLocation.City, returnedLocation.City);
            Assert.AreEqual(newLocation.State, returnedLocation.State);
            Assert.AreEqual(newLocation.PostalCode, returnedLocation.PostalCode);
            Assert.AreEqual(newLocation.PetAge, returnedLocation.PetAge);
        }

        [Test]
        public async Task AddLocation_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newLocation = new LocationDTO
            {
                PetTypeId = 2,
                Name = new string ('a', 51),
                Street1 = "123 Main Street",
                City = "Cary",
                State = "NC",
                PostalCode = "27519",
                PetAge = 2
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newLocation), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Locations", jsonContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // Update Event Type
        [Test]
        public async Task UpdateLocation_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateLocation = new LocationDTO
            {
                PetTypeId = 2,
                Name = "Dog Park",
                Street1 = "123 Main Street",
                City = "Cary",
                State = "NC",
                PostalCode = "27519",
                PetAge = 2
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateLocation), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/Locations/1", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            response = await client.GetAsync("/api/Locations/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedLocation = JsonConvert.DeserializeObject<LocationDTO>(responseContent);
            Assert.IsNotNull(returnedLocation);
            Assert.AreEqual(updateLocation.PetTypeId, returnedLocation.PetTypeId);
            Assert.AreEqual(updateLocation.Name, returnedLocation.Name);
            Assert.AreEqual(updateLocation.Street1, returnedLocation.Street1);
            Assert.AreEqual(updateLocation.City, returnedLocation.City);
            Assert.AreEqual(updateLocation.State, returnedLocation.State);
            Assert.AreEqual(updateLocation.PostalCode, returnedLocation.PostalCode);
            Assert.AreEqual(updateLocation.PetAge, returnedLocation.PetAge);

        }

        [Test]
        public async Task UpdateLocation_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateLocation = new LocationDTO
            {
                PetTypeId = 2,
                Name = new string ('a', 51),
                Street1 = "123 Main Street",
                City = "Cary",
                State = "NC",
                PostalCode = "27519",
                PetAge = 2
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateLocation), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/Locations/1", jsonContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Test]
        public async Task UpdateEventType_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var updateLocation = new LocationDTO
            {
                PetTypeId = 2,
                Name = "Dog Park",
                Street1 = "123 Main Street",
                City = "Cary",
                State = "NC",
                PostalCode = "27519",
                PetAge = 2
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateLocation), Encoding.UTF8, "application/json");

            // Token Auth

            // Act
            var response = await client.PutAsync("/api/Locations/0", jsonContent);

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
            var response = await client.DeleteAsync("/api/Locations/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task DeleteEventType_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Locations/0");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
