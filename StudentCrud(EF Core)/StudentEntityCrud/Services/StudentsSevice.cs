using StudentEntityCrud.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StudentEntityCrud;
using System.Collections.Generic;
using System.Linq;
using StudentEntityCrud.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace StudentEntityCrud.Services
{
    public class StudentsSevice : IStudentsService
    {
        private readonly AppDbContext _context;

        public StudentsSevice(AppDbContext context)
        {
            _context = context;
        }
        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }
        public List<Student> GetStudent(int id)
        {
            return _context.Students.Where(s => s.Id == id).ToList();
        }

        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(int id,Student student) {
            var existingStudent = _context.Students.FirstOrDefault(x=>x.Id==id);
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            _context.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
