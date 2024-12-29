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
    public class EquipementsControllerTests
    {
        [TestMethod()]
        public void EquipementControllerTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();

            // Act
            var controller = new EquipementsController(mockRepository.Object,null);

            // Assert
            Assert.IsNotNull(controller, "Le contrôleur ne doit pas être nul.");
        }


        [TestMethod()]
        public async Task GetEquipementsTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Equipement>>();
            var equipementList = new List<Equipement>
            {
                new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId=2,TypeCapteurId=1,DimensionX=12,DimensionY=15,DimensionZ=20,PositionX=30,PositionY=20,PositionZ=10},
                new Equipement { EquipementId = 1, Nom = "Fenetre", SalleId=1,TypeCapteurId=3,DimensionX=12,DimensionY=15,DimensionZ=25,PositionX=30,PositionY=22,PositionZ=12},
            };

            mockRepositoryDTO.Setup(repo => repo.GetAllAsync()).ReturnsAsync(equipementList);
            var controller = new EquipementsController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetEquipements();

            // Assert
            Assert.IsNotNull(result, "Le résultat de la requête ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Equipement>>), "Le type de retour doit être ActionResult<IEnumerable<Equipement>>.");
            Assert.AreEqual(equipementList.Count, result.Value.Count(), "Le nombre d'éléments dans le résultat ne correspond pas.");
            CollectionAssert.AreEqual(equipementList, result.Value.ToList(), "Les listes d'équipements ne correspondent pas.");
        }

        [TestMethod()]
        public async Task GetEquipementByIdTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Equipement>>();
            var equipement = new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId = 2, TypeCapteurId = 1, DimensionX = 12, DimensionY = 15, DimensionZ = 20, PositionX = 30, PositionY = 20, PositionZ = 10 };


            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(equipement);
            var controller = new EquipementsController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetEquipementById(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Equipement>), "Le type de retour doit être ActionResult<Batiment>.");
            Assert.AreEqual(equipement, result.Value, "L'équipement retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task GetBatimentByIdTest_NotFound()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Equipement>>();
            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Equipement)null);
            var controller = new EquipementsController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetEquipementById(0);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le type de retour doit être NotFoundResult.");
        }

        [TestMethod()]
        public async Task GetBatimentByStringTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<Equipement>>();
            var equipement = new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId = 2, TypeCapteurId = 1, DimensionX = 12, DimensionY = 15, DimensionZ = 20, PositionX = 30, PositionY = 20, PositionZ = 10 };

            mockRepositoryDTO.Setup(repo => repo.GetByStringAsync("Chauffage")).ReturnsAsync(equipement);
            var controller = new EquipementsController(mockRepositoryDTO.Object,null);

            // Act
            var result = await controller.GetEquipementByString("Chauffage");

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Equipement>), "Le type de retour doit être ActionResult<Equipement>.");
            Assert.AreEqual(equipement, result.Value, "L'équipement retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task PutEquipementTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var equipement = new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId = 2, TypeCapteurId = 1, DimensionX = 12, DimensionY = 15, DimensionZ = 20, PositionX = 30, PositionY = 20, PositionZ = 10 };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(equipement);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Equipement>(), equipement));

            var controller = new EquipementsController(mockRepository.Object,null);

            // Act
            var result = await controller.PutEquipement(1, equipement);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }

        [TestMethod()]
        public async Task PostEquipementTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var marque = new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId = 2, TypeCapteurId = 1, DimensionX = 12, DimensionY = 15, DimensionZ = 20, PositionX = 30, PositionY = 20, PositionZ = 10 };

            mockRepository.Setup(repo => repo.AddAsync(marque));
            var controller = new EquipementsController(mockRepository.Object,null);

            // Act
            var result = await controller.PostEquipement(marque);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Le type de retour doit être CreatedAtActionResult.");
        }

        [TestMethod()]
        public async Task DeleteEquipementTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var equipement = new Equipement { EquipementId = 1, Nom = "Chauffage", SalleId = 2, TypeCapteurId = 1, DimensionX = 12, DimensionY = 15, DimensionZ = 20, PositionX = 30, PositionY = 20, PositionZ = 10 };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(equipement);
            mockRepository.Setup(repo => repo.DeleteAsync(equipement));

            var controller = new EquipementsController(mockRepository.Object,null);

            // Act
            var result = await controller.DeleteEquipement(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }

    }
}