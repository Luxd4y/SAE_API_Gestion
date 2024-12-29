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
    public class SallesControllerTests
    {
        [TestMethod()]
        public void SalleControllerTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Salle>>();

            // Act
            var controller = new SallesController(mockRepository.Object,null);

            // Assert
            Assert.IsNotNull(controller, "Le contrôleur ne doit pas être nul.");
        }


        [TestMethod()]
        public async Task GetSalleTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Salle>>();
            var salleList = new List<Salle>
            {
                new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" },
                new Salle { SalleId = 2, Nom = "D", BatimentId = 1, Type = "TP" }
            };

            mockRepositoryDTO.Setup(repo => repo.GetAllAsync()).ReturnsAsync(salleList);
            var controller = new SallesController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetSalles();

            // Assert
            Assert.IsNotNull(result, "Le résultat de la requête ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Salle>>), "Le type de retour doit être ActionResult<IEnumerable<Salle>>.");
            Assert.AreEqual(salleList.Count, result.Value.Count(), "Le nombre d'éléments dans le résultat ne correspond pas.");
            CollectionAssert.AreEqual(salleList, result.Value.ToList(), "Les listes de salles ne correspondent pas.");
        }

        [TestMethod()]
        public async Task GetSalleByIdTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Salle>>();
            var salle = new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" };

            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(salle);
            var controller = new SallesController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetSalleById(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Salle>), "Le type de retour doit être ActionResult<Salle>.");
            Assert.AreEqual(salle, result.Value, "La salle retournée ne correspond pas à celle attendue.");
        }

        [TestMethod()]
        public async Task GetSalleByIdTest_NotFound()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Salle>>();
            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Salle)null);
            var controller = new SallesController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetSalleById(0);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le type de retour doit être NotFoundResult.");
        }

        [TestMethod()]
        public async Task GetSalleByStringTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Salle>>();
            var salle = new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" };

            mockRepositoryDTO.Setup(repo => repo.GetByStringAsync("C")).ReturnsAsync(salle);
            var controller = new SallesController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetSalleByString("C");

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Salle>), "Le type de retour doit être ActionResult<Salle>.");
            Assert.AreEqual(salle, result.Value, "La salle retournée ne correspond pas à celle attendue.");
        }

        [TestMethod()]
        public async Task PutSalleTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Salle>>();
            var salle = new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(salle);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Salle>(), salle));

            var controller = new SallesController(mockRepository.Object,null);

            // Act
            var result = await controller.PutSalle(1, salle);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }

        [TestMethod()]
        public async Task PostSalleTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Salle>>();
            var salle = new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" };

            mockRepository.Setup(repo => repo.AddAsync(salle));
            var controller = new SallesController(mockRepository.Object,null);

            // Act
            var result = await controller.PostSalle(salle);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Le type de retour doit être CreatedAtActionResult.");
        }

        [TestMethod()]
        public async Task DeleteSalleTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Salle>>();
            var salle = new Salle { SalleId = 1, Nom = "C", BatimentId = 1, Type = "TP" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(salle);
            mockRepository.Setup(repo => repo.DeleteAsync(salle));

            var controller = new SallesController(mockRepository.Object,null);

            // Act
            var result = await controller.DeleteSalle(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }


    }
}