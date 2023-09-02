using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Data;

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

        // GetAllUsersInfo ==> Using query
        //public HttpResponseMessage GetAllUsersInfo()
        //{
        //    List<EmpInfo> usersInfo = new List<EmpInfo>();

        //    string query = "SELECT Users.ID AS UserID, Users.Username, Users.NationalNumber, Users.Email, Users.Phone, Users.IsActive FROM Users;";

        //    using (SqlDataReader reader = dataAccess.ExecuteReader(query))
        //    {
        //        while (reader.Read())
        //        {
        //            int userNatNum = Convert.ToInt32(reader["NationalNumber"]);

        //            EmpInfo userInfo = new EmpInfo
        //            {
        //                Username = reader["Username"].ToString(),
        //                NationalNumber = Convert.ToInt32(reader["NationalNumber"]),
        //                Email = reader["Email"].ToString(),
        //                Phone = reader["Phone"].ToString(),
        //                IsActive = Convert.ToBoolean(reader["IsActive"])
        //            };

        //            if (userInfo.IsActive)
        //            {
        //                decimal avgSalary = AvgSalaries(userNatNum);
        //                userInfo.AvgSalary = avgSalary;

        //                decimal largestSalary = LargerSalary(userNatNum);
        //                userInfo.LargestSalary = largestSalary;

        //                string salaryStatus = GetUserSalaryStatus(userNatNum);
        //                userInfo.UserSalaryStatus = salaryStatus;
        //            }
        //            else
        //            {
        //                userInfo.AvgSalary = 0;
        //                userInfo.LargestSalary = 0;
        //                userInfo.UserSalaryStatus = "The user with the provided national number is no longer active in the database";
        //            }

        //            usersInfo.Add(userInfo);
        //        }
        //    }

        //    string jsonResult = JsonConvert.SerializeObject(usersInfo);

        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //    response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

        //    return response;
        //}

        
        
        
        
        
        // GetAllUsersInfo ==> Using SP
        public HttpResponseMessage GetAllUsersInfo()
        {
            List<EmpInfo> usersInfo = new List<EmpInfo>();

            SqlParameter[] parameters = new SqlParameter[]
            {
               
            };

            using (SqlDataReader reader = dataAccess.ExecuteStoredProcedure("GetAllUsersInfo", parameters))
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
                        userInfo.UserSalaryStatus = "The user with the provided national number is no longer active in the database";
                    }

                    usersInfo.Add(userInfo);
                }
            }

            string jsonResult = JsonConvert.SerializeObject(usersInfo);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

            return response;
        }


        // GetUserByNatId ==> Using query
        //public HttpResponseMessage GetUserByNatId(int userNatNum)
        //{
        //    EmpInfo userInfo = null;

        //    string query = $"SELECT Users.ID AS UserID, Users.Username, Users.NationalNumber, Users.Email, Users.Phone, Users.IsActive FROM Users WHERE Users.NationalNumber = {userNatNum};";

        //    using (SqlDataReader reader = dataAccess.ExecuteReader(query))
        //    {
        //        if (reader.Read())
        //        {
        //            userInfo = new EmpInfo
        //            {
        //                UserID = Convert.ToInt32(reader["UserID"]),
        //                Username = reader["Username"].ToString(),
        //                NationalNumber = Convert.ToInt32(reader["NationalNumber"]),
        //                Email = reader["Email"].ToString(),
        //                Phone = reader["Phone"].ToString(),
        //                IsActive = Convert.ToBoolean(reader["IsActive"])
        //            };
        //        }
        //    }

        //    if (userInfo != null)
        //    {
        //        decimal avgSalary = AvgSalaries(userNatNum);
        //        userInfo.AvgSalary = avgSalary;

        //        decimal largestSalary = LargerSalary(userNatNum);
        //        userInfo.LargestSalary = largestSalary;

        //        string salaryStatus = GetUserSalaryStatus(userNatNum);
        //        userInfo.UserSalaryStatus = salaryStatus;

        //        string jsonResult = JsonConvert.SerializeObject(userInfo);

        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //        response.Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

        //        return response;
        //    }
        //    else
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.NotFound);
        //    }
        //}


        public HttpResponseMessage GetUserByNatId(int userNatNum)
        {
            EmpInfo userInfo = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@NationalNumber", userNatNum)
            };

            using (SqlDataReader reader = dataAccess.ExecuteStoredProcedure("GetAllUserInfo", parameters))
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


        // AvgSalaries using query
        //public decimal AvgSalaries(int userNatNum)
        //{
        //    string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {userNatNum};";

        //    using (SqlDataReader reader = dataAccess.ExecuteReader(query))
        //    {
        //        List<decimal> salaries = new List<decimal>();
        //        decimal sum = 0;

        //        while (reader.Read())
        //        {
        //            decimal salary = Convert.ToDecimal(reader["Salary"]);
        //            salaries.Add(salary);
        //            sum += salary;
        //        }

        //        if (salaries.Count > 0)
        //        {
        //            decimal averageSalary = sum / salaries.Count;
        //            return averageSalary;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        public decimal AvgSalaries(int userNatNum)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@NationalNumber", userNatNum)
            };

            using (SqlDataReader reader = dataAccess.ExecuteStoredProcedure("GetSalariesByNationalNumber", parameters))
            {
                List<decimal> salaries = new List<decimal>();

                while (reader.Read())
                {
                  
                    decimal salary = Convert.ToDecimal(reader["Salary"]);
                    salaries.Add(salary);
                }

                if (salaries.Count > 0)
                {
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

        // LargerSalary using query
        //public decimal LargerSalary(int userNatNum)
        //{
        //    string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {userNatNum};";

        //    using (SqlDataReader reader = dataAccess.ExecuteReader(query))
        //    {
        //        List<decimal> salaries = new List<decimal>();
        //        decimal largestSalary = 0;

        //        while (reader.Read())
        //        {
        //            decimal salary = Convert.ToDecimal(reader["Salary"]);
        //            salaries.Add(salary);

        //            if (salary > largestSalary)
        //            {
        //                largestSalary = salary;
        //            }
        //        }

        //        return largestSalary;
        //    }
        //}

        public decimal LargerSalary(int userNatNum)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@NationalNumber", userNatNum)
            };

            using (SqlDataReader reader = dataAccess.ExecuteStoredProcedure("GetSalariesByNationalNumber", parameters))
            {
                List<decimal> salaries = new List<decimal>();
                decimal largestSalary = 0;

                while (reader.Read())
                {
                    decimal salary = Convert.ToDecimal(reader["Salary"]);
                    salaries.Add(salary);

                    if (salary > largestSalary)
                    {
                        largestSalary = salary;
                    }
                }

                return largestSalary;
            }
        }


        // GetUserSalaryStatus using query
        //public string GetUserSalaryStatus(int nationalId)
        //{
        //    string query = $"SELECT Salaries.Salary FROM Users INNER JOIN Salaries ON Users.ID = Salaries.UserID WHERE Users.NationalNumber = {nationalId};";

        //    using (SqlDataReader reader = dataAccess.ExecuteReader(query))
        //    {
        //        List<decimal> salaries = new List<decimal>();
        //        decimal totalSalary = 0;

        //        while (reader.Read())
        //        {
        //            decimal salary = Convert.ToDecimal(reader["Salary"]);
        //            salaries.Add(salary);
        //        }

        //        foreach (var salary in salaries)
        //        {
        //            totalSalary += salary;
        //        }

        //        string status = "GREEN";

        //        if (totalSalary < 2000)
        //        {
        //            status = "RED";
        //        }
        //        else if (totalSalary == 2000)
        //        {
        //            status = "ORANGE";
        //        }

        //        return status;
        //    }
        //}

        public string GetUserSalaryStatus(int nationalId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@NationalNumber", nationalId)
            };

            using (SqlDataReader reader = dataAccess.ExecuteStoredProcedure("GetSalariesByNationalNumber", parameters))
            {
                List<decimal> salaries = new List<decimal>();
                decimal totalSalary = 0;

                while (reader.Read())
                {
                    decimal salary = Convert.ToDecimal(reader["Salary"]);
                    salaries.Add(salary);

                    totalSalary += salary;
                }

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
