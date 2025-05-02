using Microsoft.AspNetCore.Mvc;
using StudentEntityCrud.Models;

namespace StudentEntityCrud.Interfaces
{
    public interface IStudentsService
    {
        public List<Student> GetStudents();
        public List<Student> GetStudent(int id);
        public void CreateStudent(Student student);
        public void UpdateStudent(int id,Student student);
        public void DeleteStudent(int id);
    }
}
