using ImTools;
using IspitMVCProjekat.Controllers;
using IspitMVCProjekat.Models;
using IspitMVCProjekat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IspitMVCProjekat.Tests
{
    public class IspitTests
    {
        private readonly Mock<I_IspitRepository> _mockRepo;
        private readonly IspitiController _controller;

        public IspitTests()
        {
            _mockRepo = new Mock<I_IspitRepository>();
            _controller = new IspitiController(_mockRepo.Object);
        }
        //INDEX
        [Fact]
        public void Index_Ispit_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        //CREATE
        [Fact]
        public void Create_Ispit_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_Ispit_RedirectsToIndexAction()
        {
            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            string student = "S20";
            string predmet = "C# programiranje";


            var result = _controller.Create(ispit, student, predmet);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        [Fact]
        public void Create_Ispit_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Broj indexa", "Broj indexa je obavezan");
            //_controller.ModelState.AddModelError("Predmet", "Predmet je obavezan");
            // _controller.ModelState.AddModelError("Ocena", "Ocena je obavezna");

            var ispit = new Ispit
            {
                IspitId = 5,
                //BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            string student = "S20";
            string predmet = "C# programiranje";

            var result = _controller.Create(ispit, student, predmet);

            _mockRepo.Verify(c => c.Create(It.IsIn<Ispit>(), student, predmet), Times.Never);

        }

        [Fact]
        public void Create_Ispit_ValidModel_ReturnsView()
        {
            Ispit ispit = null;

            string student = "S20";
            string predmet = "C# programiranje";


            _mockRepo.Setup(c => c.Create
            (It.IsAny<Ispit>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback((Ispit i, string s, string p) =>
                {
                    ispit = i;
                    student = s;
                    predmet = p;

                });


            var ispitModel = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _controller.Create(ispitModel, student, predmet);

            _mockRepo.Verify(c => c.Create(It.IsAny<Ispit>(), student, predmet), Times.Once);

            Assert.Equal(ispit.IspitId, ispitModel.IspitId);
            Assert.Equal(ispit.BrojIndexa, ispitModel.BrojIndexa);
            Assert.Equal(ispit.PredmetId, ispitModel.PredmetId);
            Assert.Equal(ispit.Ocena, ispitModel.Ocena);
        }

        //EDIT
        [Fact]
        public void Edit_Ispit_ReturnIspitID()
        {
            int ispitID = 5;

            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _mockRepo.Setup(e => e.GetElementById(ispitID)).Returns(ispit);

            var expectedModel = new Ispit()
            {
                IspitId = ispit.IspitId,
                BrojIndexa = ispit.BrojIndexa,
                PredmetId = ispit.PredmetId,
                Ocena = ispit.Ocena
            };

            var actual = _controller.Edit(ispitID);

            var actualModel = actual.Model as Ispit;

            Assert.Equal(expectedModel.IspitId, actualModel.IspitId);
            Assert.Equal(expectedModel.BrojIndexa, actualModel.BrojIndexa);
            Assert.Equal(expectedModel.PredmetId, actualModel.PredmetId);
            Assert.Equal(expectedModel.Ocena, actualModel.Ocena);


        }



        [Fact]
        public void Edit_Ispit_InvalidModel()
        {

            _controller.ModelState.AddModelError("Broj indexa", "Broj indexa je obavezan");
            //_controller.ModelState.AddModelError("Predmet", "Predmet je obavezan");
            // _controller.ModelState.AddModelError("Ocena", "Ocena je obavezna");

            var ispit = new Ispit
            {
                IspitId = 5,
                //BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            string student = "S20";
            string predmet = "C# programiranje";


            _controller.Edit(ispit, student, predmet);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Ispit>(), student, predmet), Times.Never);
        }

        [Fact]
        public void Edit_Ispit_ValidModel()
        {
            Ispit ispit = null;

            string student = "S20";
            string predmet = "C# programiranje";

            _mockRepo.Setup(c => c.Edit
            (It.IsAny<Ispit>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback((Ispit i, string s, string p) =>
                {
                    ispit = i;
                    student = s;
                    predmet = p;

                });

            var ispitModel = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _controller.Edit(ispitModel, student, predmet);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Ispit>(), student, predmet), Times.Once);


            Assert.Equal(ispit.IspitId, ispitModel.IspitId);
            Assert.Equal(ispit.BrojIndexa, ispitModel.BrojIndexa);
            Assert.Equal(ispit.PredmetId, ispitModel.PredmetId);
            Assert.Equal(ispit.Ocena, ispitModel.Ocena);

        }


        [Fact]
        public void Edit_Ispit_RedirectsToIndexAction()
        {
            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            string student = "S20";
            string predmet = "C# programiranje";

            var result = _controller.Edit(ispit, student, predmet);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        [Fact]
        public void Details_Ispit_ReturnIspitID()
        {
            int? IspitID = 7;

            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _mockRepo.Setup(d => d.GetElementById(IspitID)).Returns(ispit);

            var expectedModelIspit = new Ispit()
            {
                IspitId = ispit.IspitId,
                BrojIndexa = ispit.BrojIndexa,
                PredmetId = ispit.PredmetId,
                Ocena = ispit.Ocena
            };

            var actual = _controller.Details(IspitID);

            var actualModelIspit = actual.Model as Ispit;

            Assert.Equal(expectedModelIspit.IspitId, actualModelIspit.IspitId);
            Assert.Equal(expectedModelIspit.BrojIndexa, actualModelIspit.BrojIndexa);
            Assert.Equal(expectedModelIspit.PredmetId, actualModelIspit.PredmetId);
            Assert.Equal(expectedModelIspit.Ocena, actualModelIspit.Ocena);

        }

        //DELETE
        [Fact]
        public void Delete_Ispit_ReturnIspitID()
        {
            int? IspitID = 7;

            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _mockRepo.Setup(d => d.GetElementById(IspitID)).Returns(ispit);

            var expectedModelIspit = new Ispit()
            {
                IspitId = ispit.IspitId,
                BrojIndexa = ispit.BrojIndexa,
                PredmetId = ispit.PredmetId,
                Ocena = ispit.Ocena
            };

            var actual = _controller.Delete(IspitID);


            var actualModel = actual.Model as Ispit;

            Assert.Equal(expectedModelIspit.IspitId, actualModel.IspitId);
            Assert.Equal(expectedModelIspit.BrojIndexa, actualModel.BrojIndexa);
            Assert.Equal(expectedModelIspit.PredmetId, actualModel.PredmetId);
            Assert.Equal(expectedModelIspit.Ocena, actualModel.Ocena);

        }


        [Fact]
        public void DeleteConfirmed_Ispit_ShouldWork()
        {
            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            _mockRepo.Setup(d => d.DeleteConfirmed(ispit));

            _controller.DeleteConfirmed(ispit);

            _mockRepo.Verify(e => e.DeleteConfirmed(It.IsAny<Ispit>()), Times.Once);

        }


        [Fact]
        public void Delete_Ispit_RedirectsToIndexAction()
        {
            var ispit = new Ispit
            {
                IspitId = 5,
                BrojIndexa = "S20",
                PredmetId = 5,
                Ocena = 5
            };

            var result = _controller.DeleteConfirmed(ispit);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }





    }
}
