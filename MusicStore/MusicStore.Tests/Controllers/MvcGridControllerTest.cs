using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicStore.Controllers;
using System.Web.Mvc;
using MvcJqGrid;

namespace MusicStore.Tests.Controllers
{
    [TestClass]
    class MvcGridControllerTest
    {

        [TestMethod]
        public void Index()
        {
            // Arrange
            MvcGridController controller = new MvcGridController();

            // Act
            JsonResult result = controller.GridDataBasic(new GridSettings() {PageSize = 10, PageIndex = 1}) ;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.Data);
        }
    }
}
