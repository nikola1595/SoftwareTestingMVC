using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IspitMVCProjekat.Models;
using IspitMVCProjekat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IspitMVCProjekat.Controllers
{
    public class IspitiController : Controller
    {
        private I_IspitRepository _repo;

        public IspitiController(I_IspitRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var ispiti = _repo.Index();

            return View(ispiti);

        }



        public IActionResult Create()
        {

            ViewBag.Student = _repo.StudentiToList();
            ViewBag.Predmet = _repo.PredmetiToList();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IspitId, BrojIndexa, PredmetId, Ocena")] Ispit ispit, string Student, string Predmet)
        {


            _repo.Create(ispit, Student, Predmet);

            if (ispit.BrojIndexa == null || ispit.PredmetId == 0)
            {
                return View("ErrorIspit");
            }

            return RedirectToAction(nameof(Index));

        }


        public ViewResult Edit(int? IspitID)
        {
            var ispit = _repo.GetElementById(IspitID);

            if (IspitID == null || ispit == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            ViewBag.Student = _repo.StudentiToList();
            ViewBag.Predmet = _repo.PredmetiToList();

            return View(ispit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("IspitId, BrojIndexa, PredmetId, Ocena")] Ispit ispit, string Student, string Predmet)
        {
            if (ispit.IspitId == 0)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _repo.Edit(ispit, Student, Predmet);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("ErrorIspit");

            }


            return View(ispit);
        }


        public ViewResult Details(int? IspitID)
        {
            var ispit = _repo.GetElementById(IspitID);

            if (IspitID == null || ispit == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }


            return View(ispit);
        }



        public ViewResult Delete(int? IspitID)
        {
            var ispit = _repo.GetElementById(IspitID);

            if (IspitID == null || ispit == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            return View(ispit);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Ispit ispit)
        {
            _repo.DeleteConfirmed(ispit);
            return RedirectToAction(nameof(Index));
        }

    }
}
