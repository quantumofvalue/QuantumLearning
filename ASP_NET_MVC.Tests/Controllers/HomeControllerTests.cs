using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ASP_NET_MVC;
using ASP_NET_MVC.Controllers;
using ASP_NET_MVC.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ASP_NET_MVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private static HttpContextBase GetHttpContextMock(string path)
        {
            var context = MockRepository.GenerateStub<HttpContextBase>();
            context.Stub(x => x.Request).Return(MockRepository.GenerateStub<HttpRequestBase>());
            context.Request.Stub(x => x.PathInfo).Return("");
            context.Request.Stub(x => x.AppRelativeCurrentExecutionFilePath).Return(path);
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
            var context = GetHttpContextMock("~/");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            AssertRouteData(routeData, "Home", "Index", "");
        }

        [TestMethod]
        public void HomeControllerCreateActionIsCorrectlyConnected()
        {
            // Arrange
            var context = GetHttpContextMock("~/Home/Create");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            AssertRouteData(routeData, "Home", "Create", "");
        }

        [TestMethod]
        public void IndexActionRendersIndexView()
        {
            MockRepository mocks = new MockRepository();

            ItemRepository itemRepositoryMock = mocks.StrictMock<ItemRepository>();

            HomeController homeController = new HomeController(itemRepositoryMock);

            ViewResult result = homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexActionProvidesTheItemsToTheView()
        {
            MockRepository mocks = new MockRepository();
            ItemRepository itemRepositoryMock = mocks.StrictMock<ItemRepository>();

            List<Item> expectedItems = new List<Item>();
            expectedItems.Add(new Item { ID = 1, ItemText = "Item1" });
            expectedItems.Add(new Item { ID = 2, ItemText = "Item2" });

            using (mocks.Record())
            {
                Expect.Call(itemRepositoryMock.GetAllItems()).Return(expectedItems);
            }

            using (mocks.Playback())
            {
                HomeController homeController = new HomeController(itemRepositoryMock);
                ViewResult result = homeController.Index() as ViewResult;
                
                IEnumerable<Item> actualItems = (IEnumerable<Item>)result.ViewData.Model;
                Assert.IsNotNull(actualItems);
                CollectionAssert.AreEqual(actualItems.ToList(), expectedItems);
            }
        }

        [TestMethod]
        public void CreateActionCreatesNewItem()
        {
            MockRepository mocks = new MockRepository();
            ItemRepository itemRepositoryMock = mocks.StrictMock<ItemRepository>();

            Item item = new Item { ID = 1, ItemText = "Item1" };

            using (mocks.Record())
            {
                Expect.Call(delegate { itemRepositoryMock.CreateNewItem(item); });
            }

            using (mocks.Playback())
            {
                HomeController homeController = new HomeController(itemRepositoryMock);
                RedirectToRouteResult redirectResult = homeController.Create(item) as RedirectToRouteResult;
                Assert.IsNotNull(redirectResult);
                Console.Write(redirectResult.RouteValues);
                Assert.AreEqual("Index", redirectResult.RouteValues["Action"]);
            }
        }
    }
}
