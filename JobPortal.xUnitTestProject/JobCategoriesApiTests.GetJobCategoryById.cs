using FluentAssertions;
using JobPortal.Controllers;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JobPortal.xUnitTestProject
{
    public partial class JobCategoriesApiTests
    {
        [Fact]
        public void GetCategoryById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategoryById_NotFoundResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();

            // using (var db = DbContextMocker.GetApplicationDbContext(dbName))
            // {
            // }           // db.Dispose(); implicitly called when you exit the USING Block


            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new JobCategoriesController(dbContext, logger);
            int findCategoryID = 900;

            // ACT
            IActionResult actionResultGet = apiController.GetJobCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategoryById_BadRequestResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new JobCategoriesController(dbContext, logger);
            int? findCategoryID = null;

            // ACT
            IActionResult actionResultGet = controller.GetJobCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_OkResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategoryById_OkResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new JobCategoriesController(dbContext, logger);
            int findCategoryID = 2;

            // ACT
            IActionResult actionResultGet = controller.GetJobCategory(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_CorrectResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategoryById_OkResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new JobCategoriesController(dbContext, logger);

            int findCategoryID = 2;
            JobCategory expectedCategory = DbContextMocker.TestData_JobCategories
                                        .SingleOrDefault(c => c.JobCategoryId == findCategoryID);

            // ACT
            IActionResult actionResultGet = controller.GetJobCategory(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if IActionResult (i.e., OkObjectResult) contains an object of the type Category
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<JobCategory>(okResult.Value);

            // Extract the category object from the result.
            JobCategory actualCategory = okResult.Value.Should().BeAssignableTo<JobCategory>().Subject;
            _testOutputHelper.WriteLine($"Found: CategoryID == {actualCategory.JobCategoryId}");

            // ASSERT - if category is NOT NULL
            Assert.NotNull(actualCategory);

            // ASSERT - if the CategoryId is containing the expected data.
            Assert.Equal<int>(expected: expectedCategory.JobCategoryId,
                              actual: actualCategory.JobCategoryId);

            // ASSERT - if the CateogoryName is correct 
            Assert.Equal(expected: expectedCategory.JobCategoryName,
                          actual: actualCategory.JobCategoryName);
        }
    }
}
