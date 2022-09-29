using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using JobPortal.Controllers;
using JobPortal.Models;
using System.Collections.Generic;
using Xunit;

namespace JobPortal.xUnitTestProject
{
    public partial class JobCategoriesApiTests
    {
        [Fact]
        public void GetCategories_OkResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategories_OkResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new JobCategoriesController(dbContext, logger);

            // ACT
            IActionResult actionResult = apiController.GetJobCategories().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // --- Check if the HTTP Response Code is 200 "Ok"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetJobCategories_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(JobCategoriesApiTests.GetCategories_OkResult);
            var logger = Mock.Of<ILogger<JobCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new JobCategoriesController(dbContext, logger);

            // ACT: Call the API action method
            IActionResult actionResult = apiController.GetJobCategories().Result;

            // ASSERT: Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);


            // ACT: Extract the OkResult result 
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT: if the OkResult contains the object of the Correct Type
            Assert.IsAssignableFrom<List<JobCategory>>(okResult.Value);


            // ACT: Extract the Categories from the result of the action
            var categoriesFromApi = okResult.Value.Should().BeAssignableTo<List<JobCategory>>().Subject;

            // ASSERT: if the categories is NOT NULL
            Assert.NotNull(categoriesFromApi);

            // ASSERT: if the number of categories in the DbContext seed data
            //         is the same as the number of categories returned in the API Result
            Assert.Equal<int>(expected: DbContextMocker.TestData_JobCategories.Length,
                              actual: categoriesFromApi.Count);

            // ASSERT: Test the data received from the API against the Seed Data
            int ndx = 0;
            foreach (JobCategory category in DbContextMocker.TestData_JobCategories)
            {
                // ASSERT: check if the Category ID is correct
                Assert.Equal<int>(expected: category.JobCategoryId,
                                  actual: categoriesFromApi[ndx].JobCategoryId);

                // ASSERT: check if the Category Name is correct
                Assert.Equal(expected: category.JobCategoryName,
                             actual: categoriesFromApi[ndx].JobCategoryName);

                _testOutputHelper.WriteLine($"Compared Row # {ndx} successfully");

                ndx++;          // now compare against the next element in the array
            }
        }
    }
}
