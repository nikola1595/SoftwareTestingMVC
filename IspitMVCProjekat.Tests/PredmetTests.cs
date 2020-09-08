using IspitMVCProjekat.Controllers;
using IspitMVCProjekat.Models;
using IspitMVCProjekat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IspitMVCProjekat.Tests
{
    public class PredmetTests
    {
        private readonly Mock<IPredmetRepository> _mockRepo;
        private readonly PredmetiController _controller;

        public PredmetTests()
        {
            _mockRepo = new Mock<IPredmetRepository>();
            _controller = new PredmetiController(_mockRepo.Object);
        }
        //INDEX
        [Fact]
        public void Index_Predmet_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }
        //CREATE
        [Fact]
        public void Create_Predmet_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_Predmet_RedirectsToIndexAction()
        {
            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            var result = _controller.Create(predmet);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }



        [Fact]
        public void Create_Predmet_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("PredmetId", "Sifra predmeta je obavezna");
            // _controller.ModelState.AddModelError("ImePredmeta", "Naziv predmeta je obavezan");


            var predmet = new Predmet
            {
                PredmetId = 7,
                //ImePredmeta = "C# programiranje"
            };


            var result = _controller.Create(predmet);

            _mockRepo.Verify(c => c.Create(It.IsAny<Predmet>()), Times.Never);

        }


        [Fact]
        public void Create_Predmet_ValidModel_ReturnsView()
        {
            Predmet pre = null;

            _mockRepo.Setup(c => c.Create
            (It.IsAny<Predmet>()))
                .Callback<Predmet>(p => pre = p);

            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };


            _controller.Create(predmet);

            _mockRepo.Verify(c => c.Create(It.IsAny<Predmet>()), Times.Once);

            Assert.Equal(pre.PredmetId, predmet.PredmetId);
            Assert.Equal(pre.ImePredmeta, predmet.ImePredmeta);

        }

        //DETAILS
        [Fact]
        public void Details_Predmet_ReturnPredmetID()
        {
            int? PredmetID = 5;

            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            _mockRepo.Setup(d => d.Details(PredmetID)).Returns(predmet);


            var expectedPredmetModel = new Predmet()
            {
                PredmetId = predmet.PredmetId,
                ImePredmeta = predmet.ImePredmeta,

            };


            var actual = _controller.Details(PredmetID);

            var actualModelPredmet = actual.Model as Predmet;


            Assert.Equal(expectedPredmetModel.PredmetId, actualModelPredmet.PredmetId);
            Assert.Equal(expectedPredmetModel.ImePredmeta, actualModelPredmet.ImePredmeta);

        }

        //EDIT
        [Fact]
        public void Edit_Predmet_ReturnPredmetID()
        {
            int? PredmetID = 5;

            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };


            _mockRepo.Setup(e => e.Edit(PredmetID)).Returns(predmet);

            var expectedModel = new Predmet
            {
                PredmetId = predmet.PredmetId,
                ImePredmeta = predmet.ImePredmeta
            };


            var actual = _controller.Edit(PredmetID);

            var actualModel = actual.Model as Predmet;

            Assert.Equal(expectedModel.PredmetId, actualModel.PredmetId);
            Assert.Equal(expectedModel.ImePredmeta, actualModel.ImePredmeta);

        }


        [Fact]
        public void Edit_Predmet_InvalidModel()
        {
            //int PredmetID = 5;

            _controller.ModelState.AddModelError("PredmetId", "Sifra predmeta je obavezna");
            // _controller.ModelState.AddModelError("ImePredmeta", "Naziv predmeta je obavezan");


            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };


            _controller.Edit(predmet);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Predmet>()), Times.Never);


        }


        [Fact]
        public void Edit_Predmet_ValidModel()
        {
            Predmet pre = null;

            _mockRepo.Setup(e => e.Edit(It.IsAny<Predmet>()))
                .Callback<Predmet>(p => pre = p);

            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            _controller.Edit(predmet);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Predmet>()), Times.Once);


            Assert.Equal(pre.PredmetId, predmet.PredmetId);
            Assert.Equal(pre.ImePredmeta, predmet.ImePredmeta);


        }


        [Fact]
        public void Edit_Predmet_RedirectsToIndexAction()
        {
            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            var result = _controller.Edit(predmet);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        //DELETE
        [Fact]
        public void Delete_Predmet_ReturnPredmetID()
        {
            int? PredmetID = 5;

            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            _mockRepo.Setup(d => d.Delete(PredmetID)).Returns(predmet);

            var expectedModel = new Predmet
            {
                PredmetId = predmet.PredmetId,
                ImePredmeta = predmet.ImePredmeta
            };

            var actual = _controller.Delete(PredmetID);


            var actualModel = actual.Model as Predmet;

            Assert.Equal(expectedModel.PredmetId, actualModel.PredmetId);
            Assert.Equal(expectedModel.ImePredmeta, actualModel.ImePredmeta);

        }


        [Fact]
        public void DeleteConfirmed_Predmet_ShouldWork()
        {

            var predmet = new Predmet
            {
                PredmetId = 5,
                ImePredmeta = "C# programiranje"
            };

            _mockRepo.Setup(d => d.DeleteConfirmed(predmet.PredmetId));

            _controller.DeleteConfirmed(predmet.PredmetId);

            _mockRepo.Verify(e => e.DeleteConfirmed(predmet.PredmetId), Times.Once);

        }

        [Fact]
        public void Delete_Predmet_RedirectsToIndexAction()
        {
            var predmet = new Predmet
            {
                PredmetId = 7,
                ImePredmeta = "C# programiranje"
            };

            var result = _controller.DeleteConfirmed(predmet.PredmetId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }



    }
}
