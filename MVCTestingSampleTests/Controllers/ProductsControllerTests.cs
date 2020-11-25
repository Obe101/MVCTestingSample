using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCTestingSample.Controllers;
using MVCTestingSample.Models;
using MVCTestingSample.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVCTestingSample.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        [TestMethod()]
        public async Task Index_ReturnsAViewResult_WithAListOfAllProducts()
        {
            //Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(GetProducts());

            ProductsController prodController = new ProductsController(mockRepo.Object);

            //Act
            IActionResult result = await prodController.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult)); //Ensure view is returned
            ViewResult viewResult = result as ViewResult;
            var model =viewResult.ViewData.Model;
            Assert.IsInstanceOfType(model, typeof(List<Product>)); // makes sure products list<product> passed to view

            //Ensure all products passes to view
            List<Product> productModel = model as List<Product>;
            Assert.AreEqual(3, productModel.Count);

        }

        private List<Product> GetProducts()
        {
            return  new List<Product>()
            {
                new Product()
                {
                    ProdId=1, Name = "Computer", Price = 199.99
                },
                new Product()
                {
                    ProdId=2, Name = "WebCam", Price = 49.99
                },
                new Product()
                {
                    ProdId=3, Name = "Desk", Price = 200.00
                }
            };
        }

        [TestMethod]
        public void Add_ReturnsAViewResult()
        {
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);

            var result = controller.Add();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public async Task AddPost_ReturnsARedirectAAndAddsProduct_WhenModelIsValid()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new ProductsController(mockRepo.Object);
            Product p = new Product
            {
                Name = "Widget",
                Price = 9.99
            };
            var result = await controller.Add(p);
            //Ensure user is successfully redirected  after adding product
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNull(redirectResult.ControllerName, "Controller name shouldn't be specified in the redirect");
           // Ensures the redirect is to Index Action
            Assert.AreEqual("Index", redirectResult.ActionName);

            mockRepo.Verify(); 
        }

        [TestMethod]
        public async Task AddPost_ReturnsViewWithModel_WhenModelStateIsInvalid()
        {
            var mockkRepo = new Mock<IProductRepository>();
            var controler = new ProductsController(mockkRepo.Object);
            var invalidProd = new Product
            {
                Name = null, // name is require to be valiid
                Price = 9.99,
                ProdId = 1

            };
            // Mark modelstate as invalid
            controler.ModelState.AddModelError("Name", "Reqiured");

            //Ensure view is returned
            var result = await controler.Add(invalidProd);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            //Ensure View model bound to view
            ViewResult viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Product));

            //Ensure invalid Product is passed back to view
            Product modelBoundProduct = viewResult.Model as Product;
            Assert.AreEqual(modelBoundProduct, invalidProd, "Invalid Product shoud be passd back to view");
        }
    }

}