using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmsaCollege.Models {
    public interface IRepository {
        IQueryable<Course> Courses { get; }

        IQueryable<Student> Students { get; }

        IQueryable<Student> GetStudents(int courseID);

        Student GetStudentById(int courseID);
        Student GetStudentByCode(string cod);
        void SaveCourse(Course course);
        void AddStudenttoCourse(int courseID,int studentid);
        void Update(Course course);

        Course GetById(int id);

        void Delete(Course course);

        void SaveStudent(Student student);
    }
}
