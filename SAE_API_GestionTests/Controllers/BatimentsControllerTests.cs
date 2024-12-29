using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_API_Gestion.Controllers;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API_Gestion.Controllers.Tests
{
    [TestClass()]
    public class BatimentsControllerTests
    {
        [TestMethod()]
        public void BatimentControllerTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Batiment>>();

            // Act
            var controller = new BatimentsController(mockRepository.Object);

            // Assert
            Assert.IsNotNull(controller, "Le contrôleur ne doit pas être nul.");
        }


        [TestMethod()]
        public async Task GetBatimentTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Batiment>>();
            var batimentList = new List<Batiment>
            {
                new Batiment { BatimentId = 1, Nom = "C", },
                new Batiment { BatimentId = 2, Nom = "D" }
            };

            mockRepositoryDTO.Setup(repo => repo.GetAllAsync()).ReturnsAsync(batimentList);
            var controller = new BatimentsController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetBatiments();

            // Assert
            Assert.IsNotNull(result, "Le résultat de la requête ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Batiment>>), "Le type de retour doit être ActionResult<IEnumerable<Batiment>>.");
            Assert.AreEqual(batimentList.Count, result.Value.Count(), "Le nombre d'éléments dans le résultat ne correspond pas.");
            CollectionAssert.AreEqual(batimentList, result.Value.ToList(), "Les listes de batiments ne correspondent pas.");
        }

        [TestMethod()]
        public async Task GetBatimentByIdTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Batiment>>();
            var batiment = new Batiment { BatimentId = 1, Nom = "C" };

            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(batiment);
            var controller = new BatimentsController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetBatimentById(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Batiment>), "Le type de retour doit être ActionResult<Batiment>.");
            Assert.AreEqual(batiment, result.Value, "Le batiment retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task GetBatimentByIdTest_NotFound()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Batiment>>();
            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Batiment)null);
            var controller = new BatimentsController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetBatimentById(0);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le type de retour doit être NotFoundResult.");
        }

        [TestMethod()]
        public async Task GetBatimentByStringTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Batiment>>();
            var batiment = new Batiment { BatimentId = 1, Nom = "C" };

            mockRepositoryDTO.Setup(repo => repo.GetByStringAsync("C")).ReturnsAsync(batiment);
            var controller = new BatimentsController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetBatimentByString("C");

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Batiment>), "Le type de retour doit être ActionResult<Batiment>.");
            Assert.AreEqual(batiment, result.Value, "Le batiment retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task PutBatimentTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Batiment>>();
            var batiment = new Batiment { BatimentId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(batiment);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Batiment>(), batiment));

            var controller = new BatimentsController(mockRepository.Object);

            // Act
            var result = await controller.PutBatiment(1, batiment);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }

        [TestMethod()]
        public async Task PostBatimentTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Batiment>>();
            var batiment = new Batiment { BatimentId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.AddAsync(batiment));
            var controller = new BatimentsController(mockRepository.Object);

            // Act
            var result = await controller.PostBatiment(batiment);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Le type de retour doit être CreatedAtActionResult.");
        }

        [TestMethod()]
        public async Task DeleteBatimentTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Batiment>>();
            var batiment = new Batiment { BatimentId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(batiment);
            mockRepository.Setup(repo => repo.DeleteAsync(batiment));

            var controller = new BatimentsController(mockRepository.Object);

            // Act
            var result = await controller.DeleteBatiment(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }


    }
}