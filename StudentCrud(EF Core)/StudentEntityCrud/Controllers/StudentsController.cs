using Microsoft.AspNetCore.Mvc;
using StudentEntityCrud;
using System.Collections.Generic;
using System.Linq;
using StudentEntityCrud.Models;
using StudentEntityCrud.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentsService _studentsService;

    public StudentController(IStudentsService studentService)
    {
        _studentsService = studentService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return _studentsService.GetStudents();
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _studentsService.GetStudent(id);
        if (student == null)
        {
            return NotFound();
        }
        return  Ok(student);
    }
    [HttpPost]
    public IActionResult CreateStudent(Student student)
    {
        _studentsService.CreateStudent(student);
        return Ok("Student Added Successfully");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, Student student)
    {
        _studentsService.UpdateStudent(id, student);
        return Ok("Student Data Updated");
    }


    // DELETE: api/student/5
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        _studentsService.DeleteStudent(id);
        return Ok("Student Deleted Successfully");
    }
}
