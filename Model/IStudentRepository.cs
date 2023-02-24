using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentRestAPI.Model
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> Search(string name, Gender? gender);
        Task<Student> GetStudent(int studentId);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentByEmail(string email);
        Task<Student> AddStudent(Student student);
        Task DeleteStudent(int studentId);
        Task<Student>UpdateStudent (Student student);   

    }
}
