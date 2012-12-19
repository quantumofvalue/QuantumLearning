using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Learning_ASP_NET;
using Learning_ASP_NET.Controllers;

using Rhino.Mocks;

namespace Learning_ASP_NET.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        public static void AssertRouteData(RouteData routeData, string controller, string action, string id)
        {
            string str = routeData.Values["controller"].ToString();
            Assert.IsNotNull(routeData);
            Assert.AreEqual(controller, routeData.Values["controller"].ToString());
            Assert.AreEqual(action, routeData.Values["action"].ToString());
            Assert.AreEqual(id, routeData.Values["id"].ToString());
        }

        [TestMethod]
        public void DefaultRoute()
        {
            // Arrange
            var context = GetMockHttpContext("~/");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            AssertRouteData(routeData, "Home", "Index", "");
        }

        private static HttpContextBase GetMockHttpContext(string path)
        {
            var context = MockRepository.GenerateStub<HttpContextBase>();
            context.Stub(x => x.Request).Return(MockRepository.GenerateStub<HttpRequestBase>());
            context.Request.Stub(x => x.PathInfo).Return("");
            context.Request.Stub(x => x.AppRelativeCurrentExecutionFilePath).Return("~/");
            return context;
        }
    }
}
