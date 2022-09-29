using FluentAssertions;
using JobPortal.Controllers;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JobPortal.xUnitTestProject
{
    /// <remarks>

    ///     Bad insertion data scenarios:

    ///     - Name is NULL

    ///     - Name is EMPTY STRING

    ///     - Name contains more characters than what is allowed

    ///     - NULL Category object

    ///     

    ///     LIMITATIONS OF WORKING WITH InMemory Database

    ///     - Relationships between Tables are not supported.

    ///     - EF Core DataAnnotation Validations are not supported.

    /// </remarks>
    public partial class JobCategoriesApiTests
    {
        [Fact]

        public void InsertCategory_OkResult()

        {

            // ARRANGE

            var dbName = nameof(JobCategoriesApiTests.InsertCategory_OkResult);

            var logger = Mock.Of<ILogger<JobCategoriesController>>();

            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var apiController = new JobCategoriesController(dbContext, logger);

            JobCategory categoryToAdd = new JobCategory

            {

                JobCategoryId = 5,

                JobCategoryName = "New Category",           // IF = null, then: INVALID!  CategoryName is REQUIRED
                Description = "Description 5"

            };




            // ACT

            IActionResult actionResultPost = apiController.PostJobCategory(categoryToAdd).Result;




            // ASSERT - check if the IActionResult is Ok

            Assert.IsType<OkObjectResult>(actionResultPost);




            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")

            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;

            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;

            Assert.Equal<int>(expectedStatusCode, actualStatusCode);




            // Extract the result from the IActionResult object.

            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;




            // ASSERT - if the result is a CreatedAtActionResult

            Assert.IsType<CreatedAtActionResult>(postResult.Value);




            // Extract the inserted Category object

            JobCategory actualCategory = (postResult.Value as CreatedAtActionResult).Value

                                      .Should().BeAssignableTo<JobCategory>().Subject;




            // ASSERT - if the inserted Category object is NOT NULL

            Assert.NotNull(actualCategory);




            Assert.Equal(categoryToAdd.JobCategoryId, actualCategory.JobCategoryId);

            Assert.Equal(categoryToAdd.JobCategoryName, actualCategory.JobCategoryName);

        }
    }
}
