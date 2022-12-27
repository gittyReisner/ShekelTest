using ShekelTest.Models;
using System.Configuration;

namespace ShekelTest.DataAccess
{
    public sealed class DBRepository
    {
        public static IConfigurationRoot Configuration;
        private static volatile DBRepository instance;
        private static object syncRoot = new Object();
        private DBRepository() { }

        public static DBRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DBRepository();
                    }
                }

                return instance;
            }
        }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var connectionString = Configuration["ConnectionStrings:dbo"];
            return connectionString;
        }


        public static Customer AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"INSERT into Customer VALUES ({customer.CustomerId}, {customer.Name}, {customer.Address}, {customer.Phone})";
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();


                cmd.CommandType
          
                }
            }
        }   
    }
}
