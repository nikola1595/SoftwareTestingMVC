using IspitMVCProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspitMVCProjekat.Repositories
{
    public interface IPredmetRepository
    {
        IEnumerable<Predmet> Index();

        Predmet Create(Predmet predmet);
        Predmet Edit(int? PredmetID);
        Predmet Edit(Predmet predmet);
        Predmet Delete(int? PredmetID);
        Predmet DeleteConfirmed(int PredmetID);
        Predmet Details(int? PredmetID);

        bool PredmetExists(string predmet);
    }
}
