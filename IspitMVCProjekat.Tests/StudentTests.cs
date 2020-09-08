using System;
using Xunit;
using IspitMVCProjekat.Controllers;
using Moq;
using IspitMVCProjekat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IspitMVCProjekat.Repositories;

namespace IspitMVCProjekat.Tests
{
    public class StudentTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly StudentiController _controller;


        public StudentTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _controller = new StudentiController(_mockRepo.Object);
        }

        //INDEX
        [Fact]
        public void Index_Student_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }



        //CREATE
        [Fact]
        public void Create_Student_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_Student_RedirectsToIndexAction()
        {
            var student = new Student
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };

            var result = _controller.Create(student);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        [Fact]
        public void Create_Student_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Broj indexa", "Broj indexa je obavezan");
            _controller.ModelState.AddModelError("Ime", "Ime je obavezno");
            _controller.ModelState.AddModelError("Prezime", "Prezime je obavezno");
            //_controller.ModelState.AddModelError("Adresa", "Aderesa je obavezna");
            //_controller.ModelState.AddModelError("Grad", "Grad je obavezan");

            var student = new Student
            {
                //BrojIndexa = "S20",
                //Ime = "Petar",
                //Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };

            var result = _controller.Create(student);

            _mockRepo.Verify(c => c.Create(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void Create_Student_ValidModel_ReturnsView()
        {
            Student stud = null;

            _mockRepo.Setup(c => c.Create
            (It.IsAny<Student>()))
                .Callback<Student>(s => stud = s);


            var student = new Student
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };

            _controller.Create(student);

            _mockRepo.Verify(c => c.Create(It.IsAny<Student>()), Times.Once);

            Assert.Equal(stud.BrojIndexa, student.BrojIndexa);
            Assert.Equal(stud.Ime, student.Ime);
            Assert.Equal(stud.Prezime, student.Prezime);
            Assert.Equal(stud.Adresa, student.Adresa);
            Assert.Equal(stud.Grad, student.Grad);
        }


        //EDIT
        [Fact]
        public void Edit_Student_ReturnBrojIndexa()
        {
            string brIndexa = "S20";

            var student = new Student()
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"

            };

            _mockRepo.Setup(e => e.Edit(brIndexa)).Returns(student);

            var expectedModel = new Student()
            {
                BrojIndexa = student.BrojIndexa,
                Ime = student.Ime,
                Prezime = student.Prezime,
                Adresa = student.Adresa,
                Grad = student.Grad
            };

            var actual = _controller.Edit(brIndexa);

            var actualModel = actual.Model as Student;

            Assert.Equal(expectedModel.BrojIndexa, actualModel.BrojIndexa);
            Assert.Equal(expectedModel.Ime, actualModel.Ime);
            Assert.Equal(expectedModel.Prezime, actualModel.Prezime);
            Assert.Equal(expectedModel.Adresa, actualModel.Adresa);
            Assert.Equal(expectedModel.Grad, actualModel.Grad);

        }


        [Fact]
        public void Edit_Student_InvalidModel()
        {

            _controller.ModelState.AddModelError("Broj indexa", "Broj indexa je obavezan");
            _controller.ModelState.AddModelError("Ime", "Ime je obavezno");
            _controller.ModelState.AddModelError("Prezime", "Prezime je obavezno");
            //_controller.ModelState.AddModelError("Adresa", "Aderesa je obavezna");
            //_controller.ModelState.AddModelError("Grad", "Grad je obavezan");

            var student = new Student
            {
                //BrojIndexa = "S20",
                //Ime = "Petar",
                //Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };


            _controller.Edit(student);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Student>()), Times.Never);


        }




        [Fact]
        public void Edit_Student_ValidModel()
        {
            Student stud = null;

            _mockRepo.Setup(e => e.Edit(It.IsAny<Student>()))
                .Callback<Student>(s => stud = s);

            var student = new Student()
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"

            };

            _controller.Edit(student);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Student>()), Times.Once);


            Assert.Equal(stud.BrojIndexa, student.BrojIndexa);
            Assert.Equal(stud.Ime, student.Ime);
            Assert.Equal(stud.Prezime, student.Prezime);
            Assert.Equal(stud.Adresa, student.Adresa);
            Assert.Equal(stud.Grad, student.Grad);

        }


        [Fact]
        public void Edit_Student_RedirectsToIndexAction()
        {
            var student = new Student
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };

            var result = _controller.Edit(student);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        [Fact]
        public void Delete_Student_ReturnBrojIndexa()
        {
            string brIndexa = "S20";


            var student = new Student()
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"

            };

            _mockRepo.Setup(d => d.Delete(brIndexa)).Returns(student);

            var expectedModel = new Student()
            {
                BrojIndexa = student.BrojIndexa,
                Ime = student.Ime,
                Prezime = student.Prezime,
                Adresa = student.Adresa,
                Grad = student.Grad
            };

            var actual = _controller.Delete(brIndexa);
            

            var actualModel = actual.Model as Student;

            Assert.Equal(expectedModel.BrojIndexa, actualModel.BrojIndexa);
            Assert.Equal(expectedModel.Ime, actualModel.Ime);
            Assert.Equal(expectedModel.Prezime, actualModel.Prezime);
            Assert.Equal(expectedModel.Adresa, actualModel.Adresa);
            Assert.Equal(expectedModel.Grad, actualModel.Grad);


        }


        [Fact]
        public void DeleteConfirmed_Student_ShouldWork()
        {

            var student = new Student()
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"

            };

            _mockRepo.Setup(d => d.DeleteConfirmed(student));

            _controller.DeleteConfirmed(student);

            _mockRepo.Verify(e => e.DeleteConfirmed(It.IsAny<Student>()), Times.Once);  

        }

        [Fact]
        public void Delete_Student_RedirectsToIndexAction()
        {
            var student = new Student
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"
            };

            var result = _controller.DeleteConfirmed(student);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }



        [Fact]
        public void Details_Student_ReturnBrojIndexa()
        {
            string brIndexa = "S20";


            var student = new Student()
            {
                BrojIndexa = "S20",
                Ime = "Petar",
                Prezime = "Peric",
                Adresa = "Pere Stajica 15",
                Grad = "Beograd"

            };

            _mockRepo.Setup(d => d.Details(brIndexa)).Returns(student);


            var expectedModelStudent = new Student()
            {
                BrojIndexa = student.BrojIndexa,
                Ime = student.Ime,
                Prezime = student.Prezime,
                Adresa = student.Adresa,
                Grad = student.Grad
            };


            var actual = _controller.Details(brIndexa);

            var actualModelStudent = actual.Model as Student;
            

            Assert.Equal(expectedModelStudent.BrojIndexa, actualModelStudent.BrojIndexa);
            Assert.Equal(expectedModelStudent.Ime, actualModelStudent.Ime);
            Assert.Equal(expectedModelStudent.Prezime, actualModelStudent.Prezime);
            Assert.Equal(expectedModelStudent.Adresa, actualModelStudent.Adresa);
            Assert.Equal(expectedModelStudent.Grad, actualModelStudent.Grad);


        }

















    }
}
