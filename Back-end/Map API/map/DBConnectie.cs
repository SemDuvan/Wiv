using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace map
{
    public class ObservationRepository
    {
        private string connectionString;

        public ObservationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Observation> GetObservations()
        {
            var observations = new List<Observation>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Name, Category, Description, DateTime FROM Observations";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var observation = new Observation
                            {
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Description = reader["Description"].ToString(),
                                DateTime = reader["DateTime"].ToString()
                            };
                            observations.Add(observation);
                        }
                    }
                }
            }

            return observations;
        }
    }
}

