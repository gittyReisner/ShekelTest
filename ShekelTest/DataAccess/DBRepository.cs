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


        public Customer AddCustomer(Customer addCustomer)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
                {
                    /*cmd.CommandText = $"INSERT into Customer VALUES ({addCustomer.CustomerId}, {addCustomer.Name}, {addCustomer.Address}, {addCustomer.Phone})
                     * InsertTo FactoriesToCustomers VALUES ({addCustomer.groupCode}, {addCustomer.factoryCode}, {addCustomer.customerId})";
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    */
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerId", SqlDbType.NVarChar).Value = addCustomer.CustomerId;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = addCustomer.Name;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.NVarChar).Value = addCustomer.Address;
                    cmd.Parameters.AddWithValue("@phone", SqlDbType.NVarChar).Value = addCustomer.Phone;
                    cmd.Parameters.AddWithValue("@groupCode", SqlDbType.Int).Value = addCustomer.GroupCode;
                    cmd.Parameters.AddWithValue("@factoryCode", SqlDbType.Int).Value = addCustomer.FactoryCode;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
            }
            return addCustomer;
        }

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                var CommandText = @"select groups.groupCode, groupName, customers.customerId, name, phone, address from groups
                    join factoriesToCustomer on groups.groupCode = factoriesToCustomer.groupCode
                    join customers on factoriesToCustomer.customerId = customers.customerId";
                using (SqlCommand cmd = new SqlCommand(CommandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer();
                            customer.GroupCode = (int)reader["groupCode"];
                            customer.CustomerId = (string)reader["customerId"];
                            customer.Name = (string)reader["name"];
                            customer.Phone = (string)reader["phone"];
                            customer.Address = (string)reader["address"];
                            customers.Add(customer);
                        }
                    }

                    connection.Close();

                }
                return customers;
            }
        }
        public List<Group> GetGroups()
        {
            var groups = new List<Group>();
            var allCustomers = GetCustomers();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                var CommandText = @"select * from groups";
                using (SqlCommand cmd = new SqlCommand(CommandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var group = new Group();
                            group.GroupCode = (int)reader["groupCode"];
                            group.GroupName = (string)reader["groupName"];
                            group.Customers = allCustomers.Where(customer => customer.GroupCode == group.GroupCode)?
                                .Select(x => new BaseCustomer { CustomerId = x.CustomerId, Name = x.Name })?.ToList();
                            groups.Add(group);
                        }
                    }

                    connection.Close();

                }
                return groups;
            }
        }
    }
}

