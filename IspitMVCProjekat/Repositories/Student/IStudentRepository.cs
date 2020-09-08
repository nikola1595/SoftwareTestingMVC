using System.Collections.Generic;
using System.Linq;
using IspitMVCProjekat.Models;

namespace IspitMVCProjekat.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> Index();

        Student Create(Student student);
        Student Edit(string brIndexa);
        Student Edit(Student student);
        Student Delete(string brIndexa);
        Student DeleteConfirmed(Student student);
        Student Details(string brIndexa);

        bool StudentExists(string brIndexa);
        IQueryable<Ispit> Query(string brIndexa);
    }
}
