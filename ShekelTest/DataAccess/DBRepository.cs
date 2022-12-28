using Microsoft.AspNetCore.Mvc.RazorPages;
using ShekelTest.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            var connectionString = Configuration.GetConnectionString("EFCoreTestContext");
            return connectionString;
        }


        public AddCustomer AddCustomer(AddCustomer addCustomer)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
                {
                    /*cmd.CommandText = $"INSERT into Customer VALUES ({addCustomer.customer.CustomerId}, {addCustomer.customer.Name}, {addCustomer.customer.Address}, {addCustomer.customer.Phone})
                     * InsertTo FactoriesToCustomers VALUES ({addCustomer.groupCode}, {addCustomer.factoryCode}, {addCustomer.customerId})";
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    */
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerId", SqlDbType.NVarChar).Value = addCustomer.Customer.CustomerId;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = addCustomer.Customer.Name;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.NVarChar).Value = addCustomer.Customer.Address;
                    cmd.Parameters.AddWithValue("@phone", SqlDbType.NVarChar).Value = addCustomer.Customer.Phone;
                    cmd.Parameters.AddWithValue("@groupCode", SqlDbType.Int).Value = addCustomer.GroupCode;
                    cmd.Parameters.AddWithValue("@factoryCode", SqlDbType.Int).Value = addCustomer.FactoryCode;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
            }
            return addCustomer;
        }

        public List<ListCustomers> GetCustomers()
        {
            var customers = new List<ListCustomers>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                var CommandText = @"select groups.groupCode, groupName, customers.customerId, name from groups
                    join factoriesToCustomer on groups.groupCode = factoriesToCustomer.groupCode
                    join customers on factoriesToCustomer.customerId = customers.customerId";
                using (SqlCommand cmd = new SqlCommand(CommandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new ListCustomers();
                            customer.groupCode = (int)reader["groupCode"];
                            customer.groupName = (string)reader["groupName"];
                            customer.customerId = (string)reader["customerId"];
                            customer.name = (string)reader["name"];
                            customers.Add(customer);
                        }
                    }

                    connection.Close();

                }
                return customers;
            }
        }
    }
}
