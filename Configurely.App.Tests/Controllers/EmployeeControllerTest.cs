using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Configurely.App.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configurely.App.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public async Task Index()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Edit(2, "Edit") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewBag);
            Assert.IsNotNull(result.ViewBag.id);
        }

        [TestMethod]
        public async Task GetEmployeeData()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            string result = await controller.GetEmployeeData(2) as string;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);

        }
    }
}
