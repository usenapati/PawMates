using Microsoft.Data.SqlClient;
using PawMates.CORE;
using PawMates.CORE.DTOs;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.ADO
{
    public class ADOReportsRepository : IReportsRepository
    {
        private readonly string _connectionString;
        
        public ADOReportsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Response<List<PlayDatesByLocationListItem>> GetPlayDatesByLocation(string postalCode)
        {
            List<PlayDatesByLocationListItem> playDatesByLocationListItems = new List<PlayDatesByLocationListItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetPlayDatesByLocation", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PostalCode", postalCode);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var item = new PlayDatesByLocationListItem();
                            item.LocationName = reader.GetString(0);
                            item.PetType = reader.GetString(1);
                            item.EventName = reader.GetString(2);
                            item.StartTime = reader.GetDateTime(3);
                            item.EndTime = reader.GetDateTime(4);
                            item.City = reader.GetString(5);
                            item.State = reader.GetString(6);
                            item.PostalCode = reader.GetString(7);
                            playDatesByLocationListItems.Add(item);
                        }
                    } catch (SqlException ex)
                    {
                        return new Response<List<PlayDatesByLocationListItem>>() { Message = "Unable to add PlayDates By Location List Item.\n" + ex.Message, Success = false };
                    }
                }
            }
            return new Response<List<PlayDatesByLocationListItem>>() { Data = playDatesByLocationListItems, Success = true };
        }

        public Response<List<PlayDatesBySpeciesListItem>> GetPlayDatesBySpecies(string species)
        {
            List<PlayDatesBySpeciesListItem> playDatesBySpeciesListItems = new List<PlayDatesBySpeciesListItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetPlayDatesBySpecies", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Species", species);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var item = new PlayDatesBySpeciesListItem();
                            item.Species = reader.GetString(0);
                            item.EventName = reader.GetString(1);
                            item.StartTime = reader.GetDateTime(2);
                            item.EndTime = reader.GetDateTime(3);
                            item.City = reader.GetString(4);
                            item.State = reader.GetString(5);
                            item.PostalCode = reader.GetString(6);
                            playDatesBySpeciesListItems.Add(item);
                        }
                    }
                    catch (SqlException ex)
                    {
                        return new Response<List<PlayDatesBySpeciesListItem>>() { Message = "Unable to add PlayDates By Species List Item.\n" + ex.Message, Success = false };
                    }
                }
            }
            return new Response<List<PlayDatesBySpeciesListItem>>() { Data = playDatesBySpeciesListItems, Success = true };
        }

        public Response<List<TopPetParentsListItem>> GetTopPetParents()
        {
            List<TopPetParentsListItem> topPetParentsListItems = new List<TopPetParentsListItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetTopPetParents", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var item = new TopPetParentsListItem();
                            item.PlayDateCount = reader.GetInt32(0);
                            item.FirstName = reader.GetString(1);
                            item.LastName = reader.GetString(2);
                            topPetParentsListItems.Add(item);
                        }
                    }
                    catch (SqlException ex)
                    {
                        return new Response<List<TopPetParentsListItem>>() { Message = "Unable to add Top Pet Parent List Item.\n" + ex.Message, Success = false };
                    }
                }
            }
            return new Response<List<TopPetParentsListItem>>() { Data = topPetParentsListItems, Success = true };
        }
    }
}
