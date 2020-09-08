using IspitMVCProjekat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspitMVCProjekat.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private IspitDBContext _context;

        public StudentRepository(IspitDBContext context)
        {
            _context = context;
        }



        public Student Create(Student student)
        {
            _context.Add(student);
            _context.SaveChanges();

            return student;

        }

        public Student Delete(string brIndexa)
        {
            var student = _context.Student
                .FirstOrDefault(s => s.BrojIndexa == brIndexa);

            return student;
        }

        public Student DeleteConfirmed(Student student)
        {
            student = _context.Student.Find(student.BrojIndexa);
            _context.Student.Remove(student);
            _context.SaveChanges();

            return student;
        }

        public Student Details(string brIndexa)
        {
            var student = _context.Student.Find(brIndexa);

            return student;
        }

        public Student Edit(string brIndexa)
        {
            var student = _context.Student.Find(brIndexa);

            return student;
        }

        public Student Edit(Student student)
        {
            _context.Update(student);
            _context.SaveChanges();

            return student;

        }

        public IEnumerable<Student> Index()
        {

            return _context.Student.ToList();

        }

        public IQueryable<Ispit> Query(string brIndexa)
        {
            var query = _context.Ispit.AsNoTracking().Include(p => p.Predmet)
             .Where(i => i.BrojIndexa == brIndexa);

            return query;
        }

        public bool StudentExists(string brIndexa)
        {
            return _context.Student.Any(s => s.BrojIndexa == brIndexa);
        }
    }
}
