# HTTPRanger

HTTPRanger is a .NET library designed for handling HTTP requests with custom error handling and response management. This library provides easy-to-use methods for making GET and POST requests, complete with custom exception handling and detailed response information.

## Background

As a high school computer science student, I created the HTTPRanger library to simplify and streamline HTTP request handling for my own projects.

Recognizing the potential benefits for others, I decided to make the library open source. This way, not only can I enhance my own coding experience, but others can also use, improve, and contribute to the project, which can only further this library's performance and operation.

I do not have a lot of free time, but i do intend to continue to update and expand this library.

If you have a problem / suggestion regarding this project feel free to contact me here.

#### Contributions are always welcome!

## Features

-   **Custom Exception Handling:** Handles common HTTP errors with `HTTPRangerException`.
-   **Response Management:** Encapsulates HTTP responses in `HTTPRangerResponse`.
-   **Flexible Request Options:** Allows customization of requests with headers through `RequestOptions`.
-   **Built-in Tests:** Includes test cases for validating library functionality and error handling.

## Installation

You can install HTTPRanger by including the compiled `.dll` file directly in your project.

1. Download the **HTTPRanger.dll** file.
2. Add a reference to it in your project.

## Example Use

### Add `using` statements

```cs
using HTTPRanger;
using HTTPRanger.src;
```

### Perform a GET request

```cs
// Call Ranger.GetAsync(string url);
var getResponse = await Ranger.GetAsync(GET_TEST_URL);

// Print response status code (ex: 200/404...)
Console.WriteLine($"GET Status Code: {getResponse.StatusCode}");

// Print response content (ex: json/html...)
Console.WriteLine($"GET Content: {getResponse.Content}");
```

### Perform a POST request

```cs
// Define a json string for the request content
var postContent = "{ \"key\": \"value\" }";

// Call Ranger.PostAsync(string url, string content)
var postResponse = await Ranger.PostAsync(POST_TEST_URL, postContent);

// Print response status code (ex: 200/404...)
Console.WriteLine($"POST Status Code: {postResponse.StatusCode}");
```

## Library Contents

### `HTTPRangerException`

A custom exception class for handling HTTP-related errors.

-   Properties:
    -   `StatusCode` - The error's corresponding status code.
-   Constructors:
    -   `HTTPRangerException(int statusCode)` - Throws an exception with the corresponding status code's error message
    -   `HTTPRangerException(string message)` - Throws an exception with the status code 999 and the inserted error message.

# WIP - Documentation and further info will come soon!
