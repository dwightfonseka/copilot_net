//Objectives: Create a .NET Web API project that retrieves stock data from the Alpha Vantage API and returns it to the user in JSON format.
//stages: There are 5 stages (stageA, stageB, stageC, stageD, stageE)

//input: Data retrived from Alphavantage API, stock symbol defined by user
//output: Console.print the first 10 rows
//Implement stage D, stage E and stage F for each class.

//Stage A Create a .NET Web API Project:
//Set up a new ASP.NET Core Web API project.
//Configure the necessary dependencies for making HTTP requests.

//Stage B Define Endpoints for Stock Data:
//Create a StockController with an endpoint (e.g., /stock) to handle user queries.
//Implement logic for making HTTP requests to the Alpha Vantage API https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=demo
//using API key (cbhdbcdhcbdxxxx123434)

//Stage C JSON Response Formatting:
//Ensure the server responds with JSON-formatted stock data retrieved from Alpha Vantage.
//Use data models to define the structure of the response data.

//Stage D Code Optimization
//Refactor the code
//Ensure algorithm is optimal for memory usage effeciency and latecy
//Optimize code to improve API request efficiency and reducing memory usage using isPrime
//Ensure the code follows best practices for readability and maintainability.

//Stage E Implement Error Handling:
//Ensure the server handles errors such as invalid queries, API request failures, and network issues.
//Use try-catch blocks and return appropriate HTTP status codes (e.g., 400 for bad requests, 500 for server errors).

//Stage F Documentation
//Ensure proper documentataion of the components functionality

