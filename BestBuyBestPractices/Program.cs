using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);
            

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var departments = repo.GetAllDepartments();
            Print(departments);

            Console.WriteLine("Do you want to add a department????");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Department??");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }

            Console.WriteLine("Have a great day.");
            

        }

        private static void Print(IEnumerable<Department> departments)
        {

            foreach (var dept in departments)
            {
                Console.WriteLine($"ID: {dept.DepartmentID} Name: {dept.Name}");
            }
        }
    }
}
