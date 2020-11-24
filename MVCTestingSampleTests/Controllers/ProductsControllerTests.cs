﻿using Microsoft.AspNetCore.Mvc;
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
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
    }
}