using System.Runtime.CompilerServices;
using AventStack.ExtentReports;
using NUnit.Framework;
using Optional;

namespace SeleniumCsharpTests.Utility.ReportManager
{
    [TestFixture]
    public sealed class ExtentTestManager
    {
        // Static variables declared with [ThreadStatic] attribute are stored separately 
        // for each thread to avoid conflicts when multiple threads are running in parallel.
        // In this case, we use ThreadStatic to store the parent and child test instances 
        // for each test case execution thread.
        
        [ThreadStatic] 
        private static ExtentTest _parentTest;

        [ThreadStatic] 
        private static ExtentTest _childTest;

        /// <summary>
        /// Creates a parent test instance with a given name and description.
        /// </summary>
        /// <param name="testName">Name of the parent test</param>
        /// <param name="description">Optional description for the parent test</param>
        /// <returns>The created parent test instance</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, Option<string> description)
        {
            // Match method is used to execute a function depending on whether the Option has a value or not.
            // In this case, we create a parent test instance with or without a description based on 
            // whether the description option has a value or not.
            description.Match(
                some: (value) =>
                {
                    _parentTest = ExtentReportService.GetExtentReports().CreateTest(testName, value);
                },
                none: () =>
                {
                    _parentTest = ExtentReportService.GetExtentReports().CreateTest(testName);
                }
            );

            return _parentTest;
        }

        /// <summary>
        /// Creates a child test instance with a given name and description under the parent test.
        /// </summary>
        /// <param name="testName">Name of the child test</param>
        /// <param name="description">Optional description for the child test</param>
        /// <returns>The created child test instance</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, Option<string> description)
        {
            // Match method is used to execute a function depending on whether the Option has a value or not.
            // In this case, we create a child test instance with or without a description based on 
            // whether the description option has a value or not.
            description.Match(
                some: (value) =>
                {
                    _childTest = _parentTest.CreateNode(testName, value);
                },
                none: () =>
                {
                    _childTest = _parentTest.CreateNode(testName);
                });

            return _childTest;
        }

        /// <summary>
        /// Gets the current child test instance.
        /// </summary>
        /// <returns>The current child test instance</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}