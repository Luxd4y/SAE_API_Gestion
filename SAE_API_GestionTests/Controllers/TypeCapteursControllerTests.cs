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
    public class TypeCapteursControllerTests
    {
        [TestMethod()]
        public void TypeCapteurControllerTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeCapteur>>();

            // Act
            var controller = new TypeCapteursController(mockRepository.Object);

            // Assert
            Assert.IsNotNull(controller, "Le contrôleur ne doit pas être nul.");
        }


        [TestMethod()]
        public async Task GetTypeCapteursTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteurList = new List<TypeCapteur>
            {
                new TypeCapteur { TypeCapteurId = 1, Nom = "C", },
                new TypeCapteur { TypeCapteurId = 2, Nom = "D" }
            };

            mockRepositoryDTO.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typeCapteurList);
            var controller = new TypeCapteursController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetTypeCapteurs();

            // Assert
            Assert.IsNotNull(result, "Le résultat de la requête ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeCapteur>>), "Le type de retour doit être ActionResult<IEnumerable<TypeCapteur>>.");
            Assert.AreEqual(typeCapteurList.Count, result.Value.Count(), "Le nombre d'éléments dans le résultat ne correspond pas.");
            CollectionAssert.AreEqual(typeCapteurList, result.Value.ToList(), "Les listes de typeCapteurs ne correspondent pas.");
        }

        [TestMethod()]
        public async Task GetTypeCapteurByIdTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteur = new TypeCapteur { TypeCapteurId = 1, Nom = "C" };

            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(typeCapteur);
            var controller = new TypeCapteursController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetTypeCapteurById(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeCapteur>), "Le type de retour doit être ActionResult<TypeCapteur>.");
            Assert.AreEqual(typeCapteur, result.Value, "Le typeCapteur retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task GetTypeCapteurByIdTest_NotFound()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<TypeCapteur>>();
            mockRepositoryDTO.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((TypeCapteur)null);
            var controller = new TypeCapteursController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetTypeCapteurById(0);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le type de retour doit être NotFoundResult.");
        }

        [TestMethod()]
        public async Task GetTypeCapteurByStringTest()
        {
            // Arrange
            var mockRepositoryDTO = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteur = new TypeCapteur { TypeCapteurId = 1, Nom = "C" };

            mockRepositoryDTO.Setup(repo => repo.GetByStringAsync("C")).ReturnsAsync(typeCapteur);
            var controller = new TypeCapteursController(mockRepositoryDTO.Object);

            // Act
            var result = await controller.GetTypeCapteurByString("C");

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeCapteur>), "Le type de retour doit être ActionResult<TypeCapteur>.");
            Assert.AreEqual(typeCapteur, result.Value, "Le typeCapteur retourné ne correspond pas à celui attendu.");
        }

        [TestMethod()]
        public async Task PutTypeCapteurTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteur = new TypeCapteur { TypeCapteurId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(typeCapteur);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<TypeCapteur>(), typeCapteur));

            var controller = new TypeCapteursController(mockRepository.Object);

            // Act
            var result = await controller.PutTypeCapteur(1, typeCapteur);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }

        [TestMethod()]
        public async Task PostTypeCapteurTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteur = new TypeCapteur { TypeCapteurId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.AddAsync(typeCapteur));
            var controller = new TypeCapteursController(mockRepository.Object);

            // Act
            var result = await controller.PostTypeCapteur(typeCapteur);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Le type de retour doit être CreatedAtActionResult.");
        }

        [TestMethod()]
        public async Task DeleteTypeCapteurTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeCapteur>>();
            var typeCapteur = new TypeCapteur { TypeCapteurId = 1, Nom = "C" };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(typeCapteur);
            mockRepository.Setup(repo => repo.DeleteAsync(typeCapteur));

            var controller = new TypeCapteursController(mockRepository.Object);

            // Act
            var result = await controller.DeleteTypeCapteur(1);

            // Assert
            Assert.IsNotNull(result, "Le résultat ne doit pas être nul.");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le type de retour doit être NoContentResult.");
        }


    }
}