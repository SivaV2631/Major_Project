using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace JobPortal.xUnitTestProject
{
    public partial class JobCategoriesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public JobCategoriesApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
