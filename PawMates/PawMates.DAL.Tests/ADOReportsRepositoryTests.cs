using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawMates.CORE.DTOs;
using PawMates.CORE.Models;
using PawMates.DAL.ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.Tests
{
    public class ADOReportsRepositoryTests
    {
        private string _connectionString;
        private IDbConnection _connection;
        private ADOReportsRepository _repository;
        [SetUp]
        public void Setup()
        {
            _connectionString = "Server=localhost;Database=PawMates;User Id=sa;Password=HTD-C#-2023!;TrustServerCertificate=True";
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _repository = new ADOReportsRepository(_connectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }

        [Test]
        public void ShouldGetTopPetParents()
        {
            //Arrange
            int expected = 3;
            //Act
            List<TopPetParentsListItem> list = _repository.GetTopPetParents().Data;
            int actual = list.Count;
            //Assert
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void ShouldGetPlayDatesBySpecies()
        {
            //Arrange
            string expected = "Dog";
            int expectedCount = 3;
            //Act
            List<PlayDatesBySpeciesListItem> actual = _repository.GetPlayDatesBySpecies(expected).Data;
            int actualCount = actual.Count;
            //Assert
            //Assert.AreEqual(expected, actual[0].Species);
            Assert.AreEqual(expectedCount, actualCount);

        }
        [Test]
        public void ShouldGetPlayDatesByLocation()
        {
            //Arrange
            string expected = "28203";
            int expectedCount = 2;
            //Act
            List<PlayDatesByLocationListItem> actual = _repository.GetPlayDatesByLocation(expected).Data;
            int actualCount = actual.Count;
            //Assert
            //Assert.AreEqual(expected, actual[0].PostalCode);
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
