using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public class ParkDAO : IParkDAO
    {
        private readonly string connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        public ParkDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Gets all parks from database
        /// </summary>
        /// <returns>List of all Parks</returns>
        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park JOIN parkVotes on park.parkCode = parkVotes.parkCode", connection);


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        parks.Add(MapRowToPark(reader));
                    }

                    return parks;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred reading park");
                throw;
            }
        }

        /// <summary>
        /// Gets all weather from database for a given park
        /// </summary>
        /// <param name="park">Selected park</param>
        /// <returns>Five day forecast</returns>
        public List<Weather> GetWeather(Park park)
        {
            List<Weather> weatherForecast = new List<Weather>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", connection);
                    command.Parameters.AddWithValue("@parkCode", park.ParkCode);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        weatherForecast.Add(MapRowToWeather(reader));
                    }

                    return weatherForecast;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred reading weather");
                throw;
            }
        }

        /// <summary>
        /// Gets a list of all the parks with more than 0 votes from database
        /// </summary>
        /// <returns>List of all the parks with more than 0 votes from database</returns>
        public List<Park> ParksWithVotes()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park JOIN parkVotes on park.parkCode = parkVotes.parkCode WHERE votes > 0 ORDER BY votes desc, parkName asc", connection);


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        parks.Add(MapRowToPark(reader));
                    }

                    return parks;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred reading park");
                throw;
            }
        }

        /// <summary>
        /// Gets a single park from the database based on the code passed in
        /// </summary>
        /// <param name="code">The park's code</param>
        /// <returns>The selected Park</returns>
        public Park GetPark(string code)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park JOIN parkVotes on park.parkCode = parkVotes.parkCode WHERE park.parkCode = @parkCode", connection);
                    command.Parameters.AddWithValue("@parkCode", code);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        park = MapRowToPark(reader);
                    }

                    return park;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred reading park");
                throw;
            }
        }

        /// <summary>
        /// Saves vote information to database
        /// </summary>
        /// <param name="vm">The VoteViewModel containing all the info</param>
        /// <returns>True if the SQL command ran successfully, False otherwise</returns>
        public bool Vote(VoteViewModel vm)
        {
            bool inserted = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Command To Vote For A specified Park
                    SqlCommand command = new SqlCommand("UPDATE parkVotes SET votes = votes + 1 WHERE parkCode = @parkCode", connection);
                    command.Parameters.AddWithValue("@parkCode", vm.Code);

                    command.ExecuteNonQuery();

                    //Second Command to save survey information
                    SqlCommand command2 = new SqlCommand("INSERT INTO survey (parkCode, email, stateAbbreviation, activity) VALUES (@parkCode, @email, @stateAbbreviation, @activity)" + _getLastIdSQL, connection);
                    command2.Parameters.AddWithValue("@parkCode", vm.Code);
                    command2.Parameters.AddWithValue("@email", vm.Email);
                    command2.Parameters.AddWithValue("@stateAbbreviation", vm.State);
                    command2.Parameters.AddWithValue("@activity", vm.Activity);

                    command2.ExecuteScalar();
                    inserted = true;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred voting");
                throw;
            }
            return inserted;
        }

        /// <summary>
        /// Park Object from row in Database
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Park</returns>
        private Park MapRowToPark(SqlDataReader reader)
        {
            return new Park()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                Acreage = Convert.ToInt32(reader["acreage"]),
                Elevation = Convert.ToInt32(reader["elevationInFeet"]),
                MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = Convert.ToInt32(reader["yearFounded"]),
                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                Description = Convert.ToString(reader["parkDescription"]),
                EntryFee = Convert.ToInt32(reader["entryFee"]),
                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                Votes = Convert.ToInt32(reader["votes"])
            };
        }

        /// <summary>
        /// Weather Object from row in Database
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Weather</returns>
        private Weather MapRowToWeather(SqlDataReader reader)
        {
            return new Weather()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                Day = Convert.ToInt32(reader["fiveDayForecastValue"]),
                Low = Convert.ToInt32(reader["low"]),
                High = Convert.ToInt32(reader["high"]),
                Forecast = Convert.ToString(reader["forecast"])

            };
        }

        public int AddPark()
        {
            int update = 0; 
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO park (parkCode, parkName, state, acreage, elevationInFeet," +
                                                        "milesOfTrail, numberOfCampsites, climate, yearFounded, annualVisitorCount," +
                                                        "inspirationalQuote, inspirationalQuoteSource, parkDescription, entryFee, numberOfAnimalSpecies)" +
                                                        "VALUES('TST', 'TestPark', 'OH', 999, 999, 999, 999, 'Woodland', 999, 999, " +
                                                        "'test quote', 'test name', 'test description', 999, 999)"+ _getLastIdSQL, connection);
                    update = command.ExecuteNonQuery();
                    return update;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred writing park");
                throw;
            }
            
        }
    }
}
