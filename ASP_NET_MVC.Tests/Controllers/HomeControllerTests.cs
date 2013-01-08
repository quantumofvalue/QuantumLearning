using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ASP_NET_MVC;
using ASP_NET_MVC.Controllers;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ASP_NET_MVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private static HttpContextBase GetMockHttpContext(string path)
        {
            var context = MockRepository.GenerateStub<HttpContextBase>();
            context.Stub(x => x.Request).Return(MockRepository.GenerateStub<HttpRequestBase>());
            context.Request.Stub(x => x.PathInfo).Return("");
            context.Request.Stub(x => x.AppRelativeCurrentExecutionFilePath).Return("~/");
            return context;
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
        public void DeafulatRouteIsConnectedToTheHomeControllerIndexAction()
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

        [TestMethod]
        public void IndexActionRendersIndexView()
        {
            HomeController homeController = new HomeController();

            ViewResult result = homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
