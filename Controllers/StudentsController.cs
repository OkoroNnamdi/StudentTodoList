using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRestAPI.Model;

namespace StudentRestAPI.Controllers
{
    public class StudentsController : Controller

    {
        private readonly IStudentRepository studentRepository;
        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Student>>> search(string name, Gender? gender)
        {
            try
            {
                var result = await studentRepository.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");


                throw;
            }
        }
        [HttpGet("student")]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await studentRepository.GetStudents());
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from database");

            }
        }
        [HttpGet("(id:int)")]
        public async Task<ActionResult<Student>>GetStudent(int id)
        {
            try
            {
                var result = await studentRepository.GetStudent(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving students by Id from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>>createStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest();
                }
                var stud = await studentRepository.GetStudentByEmail(student.Email);
                if (stud != null)
                {
                    ModelState.AddModelError("Email", "Student email already in use");
                    return BadRequest(ModelState);

                }
                var createdStudent = await studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudent),
                    new { id = createdStudent.StudentId }, createdStudent);
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error creating new students record ");

            }
        }
        [HttpPut("(id:int)")]
        public async Task <ActionResult<Student>>UpdateStudent(int id, Student student)
        {
            try
            {
                if (id != student.StudentId)
                    return BadRequest("student ID Mismatch");
                var studentToUpdate = await studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                {
                    return NotFound($"student with Id ={id} not found");
                }
                return await studentRepository.UpdateStudent(student);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating students record ");

                
            }


        }
        [HttpDelete("(id:int)")]
        public async Task <ActionResult>DeleteStudent(int id)
        {
            try
            {
                var studentToDelete = await studentRepository.GetStudent(id);
                if (studentToDelete == null)
                {
                    return BadRequest($"student with id = {id} not found");
                }
                await studentRepository.DeleteStudent(id);

                return Ok($"Student with Id = {id} delete");

            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error deleting students record ");

            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
