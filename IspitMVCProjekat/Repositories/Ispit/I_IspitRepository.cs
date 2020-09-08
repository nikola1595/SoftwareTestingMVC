using IspitMVCProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspitMVCProjekat.Repositories
{
    public interface I_IspitRepository
    {
        IEnumerable<Ispit> Index();
        IEnumerable<Predmet> PredmetiToList();
        IEnumerable<Student> StudentiToList();
        Ispit Create(Ispit ispit, string Student, string Predmet);
        Ispit GetElementById(int? IspitID);
        Ispit Edit(Ispit ispit,string Student, string Predmet);
        Ispit DeleteConfirmed(Ispit ispit);

        public Student StudentImeFunc(string brIndexa);

        public Predmet PredmetImeFunc(string Predmet);
    }
}
