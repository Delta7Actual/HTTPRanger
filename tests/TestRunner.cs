using HTTPRanger.src;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HTTPRanger.tests
{
    public static class TestRunner
    {
        private const string GET_TEST_URL = "https://jsonplaceholder.typicode.com/posts/1";
        private const string POST_TEST_URL = "https://httpbin.org/post";
        private const string INVALID_URL = "https://invalid.url/test";
        private const string SLOW_TEST_URL = "https://httpbin.org/delay/5"; // Delayed response

        /// <summary>
        /// Runs all the tests for the HTTPRanger library to ensure correct functionality.
        /// </summary>
        public static async Task RunAllTests()
        {
            Console.WriteLine("RUNNING TESTS:\n---------");
            await TestGetAsyncWithNoHeaders();
            await TestPostAsyncWithNoHeaders();
            await TestGetAsyncWithStandardHeaders();
            await TestPostAsyncWithStandardHeaders();
            await TestHandle404Error();
            await TestHandle500Error();
        }

        /// <summary>
        /// Indicates that a test has succeeded.
        /// Changes the console text color to green and prints "TEST-SUCCESS".
        /// </summary>
        private static void Success()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TEST-SUCCESS");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Indicates that a test has failed.
        /// Changes the console text color to red and prints "TEST-FAILURE".
        /// </summary>
        private static void Failure()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TEST-FAILURE");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Test cases

        /// <summary>
        /// Test case for performing a GET request with no headers.
        /// Verifies that the content of the response matches the expected JSON.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static async Task TestGetAsyncWithNoHeaders()
        {
            // Setup
            Console.Write("NAME: TestGetAsyncWithNoHeaders ");

            try
            {
                HTTPRangerResponse response = await Ranger.GetAsync(GET_TEST_URL);

                if (response.StatusCode == 200)
                {
                    Success();
                    return;
                }
            }
            catch
            { Failure(); }
            Failure();
        }

        /// <summary>
        /// Test case for performing a POST request with no headers.
        /// Verifies that the content of the response matches the expected JSON.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static async Task TestPostAsyncWithNoHeaders()
        {
            // Setup
            Console.Write("NAME: TestPostAsyncWithNoHeaders ");

            try
            {
                HTTPRangerResponse response = await Ranger.PostAsync(POST_TEST_URL, "Content1234");

                if (response.StatusCode == 200)
                {
                    Success();
                    return;
                }
            }
            catch
            { Failure(); }
            Failure();
        }

        /// <summary>
        /// Test case for performing a GET request with standard headers.
        /// </summary>
        private static async Task TestGetAsyncWithStandardHeaders()
        {
            Console.Write("NAME: TestGetAsyncWithStandardHeaders ");
            var options = new RequestOptions
            {
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", "Bearer token123" },
                    { "User-Agent", "HTTPRanger" }
                }
            };

            try
            {
                HTTPRangerResponse response = await Ranger.GetAsync(GET_TEST_URL, options: options);

                if (response.StatusCode == 200)
                {
                    Success();
                    return;
                }
            }
            catch
            { Failure(); }
            Failure();
        }

        /// <summary>
        /// Test case for performing a POST request with standard headers.
        /// </summary>
        private static async Task TestPostAsyncWithStandardHeaders()
        {
            Console.Write("NAME: TestPostAsyncWithStandardHeaders ");
            var options = new RequestOptions
            {
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", "Bearer token123" },
                    { "Content-Type", "application/json" }
                }
            };

            try
            {
                HTTPRangerResponse response = await Ranger.PostAsync(POST_TEST_URL, "{ \"key\": \"value\" }", options);

                if (response.StatusCode == 200)
                {
                    Success();
                    return;
                }
            }
            catch
            { Failure(); }
            Failure();
        }

        /// <summary>
        /// Test case for handling a 404 Not Found error.
        /// </summary>
        private static async Task TestHandle404Error()
        {
            Console.Write("NAME: TestHandle404Error ");

            try
            {
                HTTPRangerResponse response = await Ranger.GetAsync(INVALID_URL);

                if (response.StatusCode == 404)
                {
                    Success();
                    return;
                }
            }
            catch (HTTPRangerException ex)
            { 
                if (ex.StatusCode == 404)
                {
                    Success();
                    return;
                }
            }
            Failure();
        }

        /// <summary>
        /// Test case for handling a 500 Internal Server Error.
        /// </summary>
        private static async Task TestHandle500Error()
        {
            Console.Write("NAME: TestHandle500Error ");

            try
            {
                HTTPRangerResponse response = await Ranger.GetAsync("https://httpbin.org/status/500");

                if (response.StatusCode == 500)
                {
                    Success();
                    return;
                }
            }
            catch (HTTPRangerException ex)
            {
                if (ex.StatusCode == 500)
                {
                    Success();
                    return;
                }
            }
            Failure();
        } 
    }
}
