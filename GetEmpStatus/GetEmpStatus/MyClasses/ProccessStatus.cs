using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace GetEmpStatus.MyClasses
{
    public class ProcessStatus
    {
        private DataAccess dataAccess;
        private string connectionString;

        public ProcessStatus(string connectionString)
        {
            this.connectionString = connectionString;
            dataAccess = new DataAccess(connectionString);
        }

        public HttpResponseMessage GetAllUsersInfo()
        {
            List<EmpInfo> usersInfo = new List<EmpInfo>();

            string query = "SELECT Users.ID AS UserID, Users.Username, Users.NationalNumber, Users.Email, Users.Phone, Users.IsActive FROM Users;";
            using (SqlDataReader reader = dataAccess.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    int userNatNum = Convert.ToInt32(reader["NationalNumber"]);

                    EmpInfo userInfo = new EmpInfo
                    {
                        Username = reader["Username"].ToString(),
                        NationalNumber = Convert.ToInt32(reader["NationalNumber"]),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };

                    if (userInfo.IsActive)
                    {
                        decimal avgSalary = AvgSalaries(userNatNum);
                        userInfo.AvgSalary = avgSalary;

                        decimal largestSalary = LargerSalary(userNatNum);
                        userInfo.LargestSalary = largestSalary;

                        string salaryStatus = GetUserSalaryStatus(userNatNum);
                        userInfo.UserSalaryStatus = salaryStatus;
                    }
                    else
                    {
                        userInfo.AvgSalary = 0;
                        userInfo.LargestSalary = 0;
                        userInfo.UserSalaryStatus = "The user with the provided natioanl number is no longer active in the database";
                    }

                    usersInfo.Add(userInfo);
                }
               
            }

            string jsonResult = JsonConvert.SerializeObject(usersInfo);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

            return response;
        }


        public HttpResponseMessage GetUserByNatId(int userNatNum)
        {
            EmpInfo userInfo = null;

            string query = $"SELECT Users.ID AS UserID, Users.Username, Users.NationalNumber, Users.Email, Users.Phone, Users.IsActive FROM Users WHERE Users.NationalNumber = {userNatNum};";

            using (SqlDataReader reader = dataAccess.ExecuteReader(query))
            {
                if (reader.Read())
                {
                    userInfo = new EmpInfo
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        NationalNumber = Convert.ToInt32(reader["NationalNumber"]),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }


            if (userInfo != null)
            {
                decimal avgSalary = AvgSalaries(userNatNum);
                userInfo.AvgSalary = avgSalary;

                decimal largestSalary = LargerSalary(userNatNum);
                userInfo.LargestSalary = largestSalary;

                string salaryStatus = GetUserSalaryStatus(userNatNum);
                userInfo.UserSalaryStatus = salaryStatus;

                string jsonResult = JsonConvert.SerializeObject(userInfo);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public decimal AvgSalaries(int userNatNum)
        {
            string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {userNatNum};";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlDataReader reader = dataAccess.ExecuteReader(query))
                {
                    List<decimal> salaries = new List<decimal>();

                    while (reader.Read())
                    {
                        decimal salary = Convert.ToDecimal(reader["Salary"]);
                        salaries.Add(salary);
                    }

                    reader.Close();

                    if (salaries.Count > 0)
                    {
                        //decimal averageSalary = salaries.Average();
                        decimal sum = 0;

                        foreach (var salary in salaries)
                        {
                            sum += salary;
                        }

                        decimal averageSalary = sum / salaries.Count;
              
                        return averageSalary;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public decimal LargerSalary(int userNatNum)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {userNatNum};";

                using (SqlDataReader reader = dataAccess.ExecuteReader(query))
                {
                    List<decimal> salaries = new List<decimal>();

                    while (reader.Read())
                    {
                        decimal salary = Convert.ToDecimal(reader["Salary"]);
                        salaries.Add(salary);
                    }

                    reader.Close();

                    if (salaries.Count > 0)
                    {
                        //decimal largestSalary = salaries.Max();
                        decimal largestSalary = 0;

                        foreach (var salary in salaries)
                        {
                            if (salary > largestSalary)
                            {
                                largestSalary = salary;
                            }
                        }
                        return largestSalary;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public string GetUserSalaryStatus(int nationalId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {nationalId};";

                using (SqlDataReader reader = dataAccess.ExecuteReader(query))
                {
                    List<decimal> salaries = new List<decimal>();

                    while (reader.Read())
                    {
                        decimal salary = Convert.ToDecimal(reader["Salary"]);
                        salaries.Add(salary);
                    }

                    reader.Close();

                    decimal totalSalary = salaries.Sum();
                    string status = "GREEN";

                    if (totalSalary < 2000)
                    {
                        status = "RED";
                    }
                    else if (totalSalary == 2000)
                    {
                        status = "ORANGE";
                    }

                    return status;
                }
            }
        }


    }
}
