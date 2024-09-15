using HTTPRanger.src;
using System;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace HTTPRanger.tests
{
    public static class TestRunner
    {
        private const string GET_TEST_URL = "https://jsonplaceholder.typicode.com/posts/1";
        private const string POST_TEST_URL = "https://httpbin.org/post";

        /// <summary>
        /// Runs all the tests for the HTTPRanger library to ensure correct functionality.
        /// </summary>
        public static async Task RunAllTests()
        {
            Console.WriteLine("RUNNING TESTS:\n---------");
            await TestGetAsyncWithNoHeaders();
            await TestPostAsyncWithNoHeaders();

            // TO ADD A TEST SIMPLY CALL IT WITH AWAIT -> 'await Test();'
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

        /// <summary>
        /// Parses a json string into a JsonDocument
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>JsonDocument representing the json string inserted</returns>
        private static JsonDocument GetJsonDocumentByJsonString(string jsonString)
        {
            try
            {
                return JsonDocument.Parse(jsonString);
            }
            catch (Exception ex)
            {
                throw new HTTPRangerException(ex.Message);
            }
        }

        /// <summary>
        /// Compares two JSON strings by parsing them into JSON documents and comparing the contents.
        /// </summary>
        /// <param name="json1">The first JSON string to compare.</param>
        /// <param name="json2">The second JSON string to compare.</param>
        /// <returns><c>true</c> if the two JSON documents are equivalent; otherwise, <c>false</c>.</returns>
        private static bool CompareJson(string json1, string json2)
        {
            using (var doc1 = GetJsonDocumentByJsonString(json1))
            using (var doc2 = GetJsonDocumentByJsonString(json2))
            {
                return AreJsonElementsEqual(
                    doc1.RootElement,
                    doc2.RootElement
                );
            }
        }

        /// <summary>
        /// Recursively compares two JsonElement objects to determine if they are equivalent.
        /// </summary>
        /// <param name="elem1">The first JsonElement to compare.</param>
        /// <param name="elem2">The second JsonElement to compare.</param>
        /// <returns><c>true</c> if the two JsonElement objects are equivalent; otherwise, <c>false</c>.</returns>
        private static bool AreJsonElementsEqual(JsonElement elem1, JsonElement elem2)
        {
            // Check if the value kinds are different
            if (elem1.ValueKind != elem2.ValueKind)
                return false;

            // Compare based on the value kind
            switch (elem1.ValueKind)
            {
                case JsonValueKind.Object:
                    // Compare JSON objects (unordered)
                    var properties1 = elem1.EnumerateObject();
                    var properties2 = elem2.EnumerateObject();

                    // Check that both have the same number of properties
                    if (properties1.Count() != properties2.Count())
                        return false;

                    // Compare each property
                    foreach (var prop1 in properties1)
                    {
                        if (!elem2.TryGetProperty(prop1.Name, out JsonElement prop2))
                            return false;

                        if (!AreJsonElementsEqual(prop1.Value, prop2))
                            return false;
                    }
                    return true;

                case JsonValueKind.Array:
                    // Compare JSON arrays
                    var array1 = elem1.EnumerateArray();
                    var array2 = elem2.EnumerateArray();

                    if (array1.Count() != array2.Count())
                        return false;

                    for (int i = 0; i < array1.Count(); i++)
                    {
                        if (!AreJsonElementsEqual(array1.ElementAt(i), array2.ElementAt(i)))
                            return false;
                    }
                    return true;

                case JsonValueKind.String:
                    // Compare strings
                    return elem1.GetString() == elem2.GetString();

                case JsonValueKind.Number:
                    // Compare numbers
                    return elem1.GetDouble() == elem2.GetDouble();

                case JsonValueKind.True:
                case JsonValueKind.False:
                    // Compare booleans
                    return elem1.GetBoolean() == elem2.GetBoolean();

                case JsonValueKind.Null:
                    // Compare null values
                    return true;

                default:
                    // If none of the above, return false
                    return false;
            }
        }

        // ADD TEST CASES BELOW
        // MAKE SURE TESTS ARE ASYNCHRONOUS AND STATIC
        // MAKE SURE TESTS HAVE VALID SUCCESS AND FAILURE STATES
        // MAKE SURE TO OUTPUT THE NAME OF THE TEST IN ITS START

        /// <summary>
        /// Test case for performing a GET request with no headers.
        /// Verifies that the content of the response matches the expected JSON.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static async Task TestGetAsyncWithNoHeaders()
        {
            // Setup
            Console.Write("NAME: TestGetAsyncWithNoHeaders ");
            string expectedResponse = "{\r\n  \"userId\": 1,\r\n  \"id\": 1,\r\n  \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\",\r\n  \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"\r\n}";

            HTTPRangerResponse response = await Ranger.GetAsync(GET_TEST_URL);

            if (response.StatusCode == 200)
            {
                if (CompareJson(expectedResponse, response.Content))
                {
                    Success();
                    return;
                }
            }
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
            string expectedResponse = "{\r\n  \"args\": {},\r\n  \"data\": \"Content1234\",\r\n  \"files\": {},\r\n  \"form\": {}\r\n}";

            HTTPRangerResponse response = await Ranger.PostAsync(POST_TEST_URL, "Content1234");

            if (response.StatusCode == 200)
            {
                // Parse json strings
                var expectedData =
                    GetJsonDocumentByJsonString(expectedResponse).RootElement.GetProperty("data").GetString();
                var responseData =
                    GetJsonDocumentByJsonString(response.Content).RootElement.GetProperty("data").GetString();

                if (expectedData == responseData)
                {
                    Success();
                    return;
                }
            }
            Failure();
        }
    }
}
