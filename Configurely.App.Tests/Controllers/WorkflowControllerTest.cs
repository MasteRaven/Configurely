using Configurely.App.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Configurely.App.Tests.Controllers
{
    [TestClass]
    public class WorkflowControllerTest
    {
        [TestMethod]
        public async Task Index()
        {
            // Arrange
            WorkflowController controller = new WorkflowController();

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task Edit()
        {
            // Arrange
            WorkflowController controller = new WorkflowController();

            // Act
            ViewResult result = await controller.Edit(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }
    }
}
