using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IspitMVCProjekat.Models;
using IspitMVCProjekat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IspitMVCProjekat.Controllers
{
    public class PredmetiController : Controller
    {
        private IPredmetRepository _repo;

        

        public PredmetiController(IPredmetRepository repo)
        {
            
            _repo = repo;
        }
        public IActionResult Index()
        {
            var predmeti = _repo.Index();

            return View(predmeti);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PredmetId, ImePredmeta")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                if (!_repo.PredmetExists(predmet.ImePredmeta))
                {
                    _repo.Create(predmet);
                    return RedirectToAction(nameof(Index));
                }   
            }
            return View("ErrorInsertingPredmet");
        }


        public ViewResult Details(int? PredmetID)
        {
            var predmet = _repo.Details(PredmetID);

            if (PredmetID == null || predmet == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            return View(predmet);
        }


        public ViewResult Edit(int? PredmetID)
        {
            var predmet = _repo.Edit(PredmetID);

            if (PredmetID == null || predmet == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            return View(predmet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("PredmetId,ImePredmeta")] Predmet predmet)
        {
            if (predmet.PredmetId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repo.Edit(predmet);   
            }

            return RedirectToAction(nameof(Index));
        }


        public ViewResult Delete(int? PredmetID)
        {
            var predmet = _repo.Delete(PredmetID);

            if (PredmetID == null || predmet == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }


            return View(predmet);
        }




        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int PredmetID)
        {
            _repo.DeleteConfirmed(PredmetID);

            return RedirectToAction(nameof(Index));
        }




    }
}



