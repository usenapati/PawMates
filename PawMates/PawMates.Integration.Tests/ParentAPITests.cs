using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework.Internal.Execution;
using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using PawMates.DAL;
using PawMates.ParentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Integration.Tests
{
    public class ParentAPITests
    {
        private CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;
        [SetUp]
        public void Setup()
        {
            _factory?.Dispose();
            _factory = new CustomWebApplicationFactory<Program>();
            // Initialize a client to ensure the TestServer is built.
            _client = _factory.CreateClient();
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
            }
        }

        [TearDown]

        public void Teardown()
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

        [Test]
        public async Task ShouldGetParents()
        {
            //Arrange
            int expectedCount = 1;
            //Act
            var response = await _client.GetAsync("/api/parents");
            var responseContent = await response.Content.ReadAsStringAsync();
            var parents = JsonConvert.DeserializeObject<IEnumerable<PetParentDTO>>(responseContent);
            //Assert
            Assert.AreEqual(expectedCount, parents.Count());
        }

        [Test]
        public async Task ShouldGetParentsById()
        {
            //Arrange
            int id = 1;
            var expected = new PetParentDTO
            {
                Id = 1,
                FirstName = "Chester",
                LastName = "McTester",
                Email = "cmac@chester.com",
                PhoneNumber = "123-456-7890"
            };
            //Act
            var response = await _client.GetAsync($"/api/parents/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<PetParentDTO>(responseContent);
            //Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
        }

        [Test]
        public async Task ShouldNotGetParentsByIdThatDoesNotExist()
        {
            //Arrange
            int id = 999;
            var expected = HttpStatusCode.BadRequest;
            //Act
            var response = await _client.GetAsync($"/api/parents/{id}");
            var actual = response.StatusCode;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ShouldPutPetParent()
        {
            //Arrange
            int id = 1;
            var expected = new PetParentDTO
            {
                Id = 1,
                FirstName = "Sarah",
                LastName = "McTester",
                Email = "smac@chester.com",
                PhoneNumber = "123-456-7890"
            };
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(expected), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/parents/{id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<PetParentDTO>(responseContent);
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ShouldNotPutPetParentThatDoesNotExist()
        {
            //Arrange
            int id = 999;
            var test = new PetParentDTO
            {
                Id = 999,
                FirstName = "Sarah",
                LastName = "McTester",
                Email = "smac@chester.com",
                PhoneNumber = "123-456-7890"
            };
            var expected = HttpStatusCode.NotFound;
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/parents/{id}", content);
            var actual = response.StatusCode;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ShouldPostPetParent()
        {
            var expected = new PetParentDTO
            {
                Id = 2,
                FirstName = "New",
                LastName = "Tester",
                Email = "newt@tester.com",
                PhoneNumber = "098-765-4321"
            };
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(expected), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/parents", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<PetParentDTO>(responseContent);
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
        }

        [Test]
        public async Task ShouldNotPostPetParentWithInvalidEmail()
        {
            var test = new PetParentDTO
            {
                Id = 2,
                FirstName = "New",
                LastName = "Tester",
                Email = "string",
                PhoneNumber = "string"
            };
            var expected = HttpStatusCode.BadRequest;
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/parents", content);
            var actual = response.StatusCode;
            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [Test]
        public async Task ShouldNotPostPetParentWithInvalidPhoneNumber()
        {
            var test = new PetParentDTO
            {
                Id = 2,
                FirstName = "New",
                LastName = "Tester",
                Email = "newt@tester.com",
                PhoneNumber = "string"
            };
            var expected = HttpStatusCode.BadRequest;
            //Act
            var content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/parents", content);
            var actual = response.StatusCode;
            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public async Task ShouldDeletePetParent()
        {
            int id = 1;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancelToken = source.Token;
            //Act
            var response = await _client.DeleteAsync($"/api/parents/{id}", cancelToken);
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ShouldNotDeletePetParentThatDoesNotExist()
        {
            int id = 999;
            var expected = HttpStatusCode.NotFound;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancelToken = source.Token;
            //Act
            var response = await _client.DeleteAsync($"/api/parents/{id}", cancelToken);
            var actual = response.StatusCode;
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
