using IspitMVCProjekat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspitMVCProjekat.Repositories
{
    public class IspitRepository : I_IspitRepository
    {
        private IspitDBContext _context;

        public IspitRepository(IspitDBContext context)
        {
            _context = context;
        }

        public Student StudentImeFunc(string brIndexa)
        {
            return _context.Student.SingleOrDefault(s => s.BrojIndexa == brIndexa);
        }

        public Predmet PredmetImeFunc(string Predmet)
        {
            return _context.Predmet.Single(p => p.ImePredmeta == Predmet);
        }


        public Ispit Create(Ispit ispit, string Student, string Predmet)
        {
            var StudentIme = StudentImeFunc(Student);
            var PredmetIme = PredmetImeFunc(Predmet);
            if(StudentIme == null || PredmetIme == null)
            {
                return ispit;
            }
            else
            {
                ispit.BrojIndexa = StudentIme.BrojIndexa;
                ispit.PredmetId = PredmetIme.PredmetId;

                _context.Add(ispit);
                _context.SaveChanges();

                return ispit;
            }
            
        }

        public Ispit DeleteConfirmed(Ispit ispit)
        {
            var selectedIspit = _context.Ispit.Find(ispit.IspitId);
            _context.Ispit.Remove(selectedIspit);
            _context.SaveChanges();

            return selectedIspit;
        }


        public Ispit GetElementById(int? IspitID)
        {

            var ispit = _context.Ispit.Include(s => s.BrojIndexaNavigation)
                .Include(p => p.Predmet).FirstOrDefault(i => i.IspitId == IspitID);

            return ispit;
        }

        public Ispit Edit(Ispit ispit,string Student, string Predmet)
        {
            var StudentIme = StudentImeFunc(Student);
            var PredmetIme = PredmetImeFunc(Predmet);

            if (StudentIme == null || PredmetIme == null)
            {
                return ispit;
            }
            else
            {
                ispit.BrojIndexa = StudentIme.BrojIndexa;
                ispit.PredmetId = PredmetIme.PredmetId;

                _context.Update(ispit);
                _context.SaveChanges();

                return ispit;
            }
            
        }

        public IEnumerable<Ispit> Index()
        {
            var ispiti = _context.Ispit
               .Include(s => s.BrojIndexaNavigation)
               .Include(p => p.Predmet)
               .ToList();

            return ispiti;
        }

        public IEnumerable<Predmet> PredmetiToList()
        {
            return _context.Predmet.ToList();
        }

        public IEnumerable<Student> StudentiToList()
        {
            return _context.Student.ToList();
        }


     

    }
}
