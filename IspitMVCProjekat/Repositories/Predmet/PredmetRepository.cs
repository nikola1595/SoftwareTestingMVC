using IspitMVCProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspitMVCProjekat.Repositories
{
    public class PredmetRepository : IPredmetRepository
    {
        private IspitDBContext _context;

        public PredmetRepository(IspitDBContext context)
        {
            _context = context;
        }

        public Predmet Create(Predmet predmet)
        {
            _context.Add(predmet);
            _context.SaveChanges();

            return predmet;
        }

        public Predmet Delete(int? PredmetID)
        {
            var predmet = _context.Predmet
               .FirstOrDefault(p => p.PredmetId == PredmetID);

            return predmet;
        }

        public Predmet DeleteConfirmed(int PredmetID)
        {
            var predmet = _context.Predmet.Find(PredmetID);
            _context.Predmet.Remove(predmet);
            _context.SaveChanges();

            return predmet;
        }

        public Predmet Details(int? PredmetID)
        {
            var predmet = _context.Predmet
               .FirstOrDefault(p => p.PredmetId == PredmetID);

            return predmet;
        }

        public Predmet Edit(int? PredmetID)
        {
            var predmet = _context.Predmet.Find(PredmetID);
            
            return predmet;
        }

        public Predmet Edit(Predmet predmet)
        {
            _context.Update(predmet);
            _context.SaveChanges();

            return predmet;
        }

        public IEnumerable<Predmet> Index()
        {
            return _context.Predmet.ToList();
        }

        public bool PredmetExists(string predmet)
        {
            return _context.Predmet.Any(p => p.ImePredmeta == predmet);
        }
    }
}
