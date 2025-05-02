using EmployeeCrud.Models;
using Microsoft.Data.SqlClient;
namespace EmployeeCrud.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> GetEmployeeById(int id);
        public void AddEmployee(Employee employee);
        public void DeleteEmployee(int id);
        public void UpdateEmployee(Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        public string _connectionString;
        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "select *from employee";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = (int)reader["EmployeeId"],
                                Name = reader["EmployeeName"].ToString(),
                                Salary = (int)reader["Salary"]
                            });
                        }
                    }
                }

            }
            return employees;
        }
        public IEnumerable<Employee> GetEmployeeById(int id)
        {
            var employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "Select EmployeeId,EmployeeName,Salary from employee where EmployeeId=@Id ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = (int)reader["EmployeeId"],
                                Name = reader["EmployeeName"].ToString(),
                                Salary = (int)reader["Salary"]

                            });
                        }

                    }
                }
            }
            return employees;
        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "insert into employee(EmployeeId,EmployeeName,Salary) values (@EmployeeId,@EmployeeName,@Salary)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.Id);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.ExecuteNonQuery();
                }


            }
        }
        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "Delete from employee where EmployeeId=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = "update employee set EmployeeId=@EmployeeId,EmployeeName=@EmployeeName,Salary=@Salary where EmployeeId=@Id ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.Id);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No employee found with the given ID.");
                    }
                }
            }
        }
    }
}
