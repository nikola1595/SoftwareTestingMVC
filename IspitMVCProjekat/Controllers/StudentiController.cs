using System;
using IspitMVCProjekat.Models;
using IspitMVCProjekat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IspitMVCProjekat.Controllers
{
    public class StudentiController : Controller
    {
        private IStudentRepository _repo;

        public StudentiController(IStudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var studenti = _repo.Index();


            return View(studenti);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BrojIndexa,Ime,Prezime,Adresa,Grad")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (!_repo.StudentExists(student.BrojIndexa))
                {
                    _repo.Create(student);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View("ErrorInsertingStudent");

        }




        public ViewResult Edit(string brIndexa)
        {
            var student = _repo.Edit(brIndexa);

            if (brIndexa == null || student == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            return View(student);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("BrojIndexa,Ime,Prezime,Adresa,Grad")] Student student)
        {
            if (student.BrojIndexa == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repo.Edit(student);

            }
            return RedirectToAction(nameof(Index));
        }


        public ViewResult Delete(string brIndexa)
        {
            var SelectedStudent = _repo.Delete(brIndexa);

            if (brIndexa == null || SelectedStudent == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml"); ;
            }


            return View(SelectedStudent);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Student student)
        {
            _repo.DeleteConfirmed(student);
            return RedirectToAction(nameof(Index));
        }




        public ViewResult Details(string brIndexa)
        {
            var student = _repo.Details(brIndexa);

            if (brIndexa == null || student == null)
            {
                return View("~/Views/Shared/ErrNotFound.cshtml");
            }

            ViewBag.Ispiti = _repo.Query(brIndexa);

            return View(student);

        }


        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }









    }
}
