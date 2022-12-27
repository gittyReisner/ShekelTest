using Microsoft.AspNetCore.Mvc.RazorPages;
using ShekelTest.Models;
using System.Configuration;
using System.Data;

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


        //public static Customer AddCustomer(Customer customer)
        //{
        //    using (SqlConnection connection = new SqlConnection(GetConnectionString())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
        //        {
        //            /*cmd.CommandText = $"INSERT into Customer VALUES ({customer.CustomerId}, {customer.Name}, {customer.Address}, {customer.Phone})";
        //            connection.Open();
        //            cmd.ExecuteNonQuery();
        //            connection.Close();
        //            */


        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@customerId", SqlDbType.NVarChar).Value = customer.CustomerId;
        //        cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = customer.Name;
        //        cmd.Parameters.AddWithValue("@address", SqlDbType.Int).Value = customer.Address;
        //        cmd.Parameters.AddWithValue("@phone", SqlDbType.Int).Value = customer.Phone;
        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //        connection.Close();

        //    }
        //}

        public void get()
        {

        }

        /*public static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString())
            {
            using (SqlCommand cmd = new SqlCommand(connection))
            {
                cmd.CommandText = @"select groups.groupCode, groupName, customers.customerId, name, address, phone from groups
                    left join factoriesToCustomer on groups.groupCode = factoriesToCustomer.groupCode
                    left join customers on factoriesToCustomer.customerId = customers.customerId";
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new Customer();
                        var group = new Group();
                        group.GroupCode = Convert.ToInt32(reader["grouoCode"].ToString());
                        customer.CustomerId = reader["customerId"];
                        customer.Name = reader["name"];
                        customer.Address = reader["address"];
                        customer.Phone = reader["phone"]

                        customers.Add(customer);
                    }
                }

                connection.Close();

            }
            return customers;
        }
    } */

    }
}
