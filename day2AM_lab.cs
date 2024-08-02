```csharp
// Lab Exercise 11: Optimizing API Performance in a C# Console App using AlphaVantage API

// Objectives
// - Learn to consume and optimize the performance of a third-party API using the AlphaVantage API in a .NET console application.
// - Implement dependency injection to manage service lifetimes and dependencies.
// - Code an API wrapper for AlphaVantage to demonstrate optimized performance techniques.
// - Leverage GitHub Copilot to assist in writing efficient code and generating entire API controllers.

// Step 1: Setting Up the Project
// Initialize a new C# console application.
// Install necessary packages such as Microsoft.Extensions.DependencyInjection, System.Net.Http, and Newtonsoft.Json for HTTP requests and JSON handling.

// Step 2: Creating the AlphaVantage API Client
// Create a service class that handles communication with the AlphaVantage API.
// Implement methods to retrieve data from different AlphaVantage API endpoints.

public class AlphaVantageApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AlphaVantageApiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    // Method to get stock data
    public async Task<string> GetStockDataAsync(string symbol)
    {
        var response = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

// Step 3: Implementing Dependency Injection
// Register the AlphaVantageApiClient service in the dependency injection container.
// Configure the service provider to manage dependencies across the application.

var services = new ServiceCollection();
services.AddHttpClient<AlphaVantageApiClient>();
services.AddSingleton(new AlphaVantageApiClient(new HttpClient(), "YourAlphaVantageAPIKey"));
var serviceProvider = services.BuildServiceProvider();

// Step 4: Coding the API Wrapper
// Implement methods that utilize the AlphaVantageApiClient to fetch and process data.
// Focus on optimizing performance through techniques such as caching and asynchronous programming.

public class StockService
{
    private readonly AlphaVantageApiClient _apiClient;
    private readonly MemoryCache _cache;

    public StockService(AlphaVantageApiClient apiClient)
    {
        _apiClient = apiClient;
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    // Fetch and cache stock data
    public async Task<string> GetStockDataCachedAsync(string symbol)
    {
        if (!_cache.TryGetValue(symbol, out string stockData))
        {
            stockData = await _apiClient.GetStockDataAsync(symbol);
            _cache.Set(symbol, stockData, TimeSpan.FromMinutes(15));
        }
        return stockData;
    }
}

// Step 5: Optimizing API Performance
// Implement caching to reduce the number of API calls to AlphaVantage.
// Use asynchronous programming to improve the scalability of your API wrapper.

public async Task<string> GetStockDataOptimizedAsync(string symbol)
{
    // Check cache first
    if (_cache.TryGetValue(symbol, out string stockData))
    {
        return stockData;
    }

    // Fetch from API if not in cache
    stockData = await _apiClient.GetStockDataAsync(symbol);
    _cache.Set(symbol, stockData, TimeSpan.FromMinutes(15));
    return stockData;
}


// Lab Exercise 12: Error Handling, Retry Logic, and API Rate Limiting in a C# Console App

// Objectives
// - Learn to implement robust error handling for a .NET application consuming the AlphaVantage API.
// - Implement retry logic to handle transient errors gracefully.
// - Apply API rate limiting to control the number of API calls and prevent overloading third-party services.
// - Leverage GitHub Copilot to assist in writing efficient code and generating error handling and retry mechanisms.

// Step 1: Setting Up the Project
// Initialize a new C# console application.
// Install necessary packages such as Microsoft.Extensions.DependencyInjection, System.Net.Http, Polly for retry logic, and Newtonsoft.Json for JSON handling.

// Step 2: Creating the AlphaVantage API Client with Error Handling
// Create a service class that handles communication with the AlphaVantage API.
// Implement error handling to catch exceptions and handle API errors gracefully.

public class AlphaVantageApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AlphaVantageApiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    // Method to get stock data with error handling
    public async Task<string> GetStockDataAsync(string symbol)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            // Log and handle network-related errors
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Log and handle other errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return null;
        }
    }
}

// Step 3: Implementing Retry Logic
// Use the Polly library to implement retry logic for handling transient faults like network timeouts or rate limits.
// Configure retry policies to retry failed requests with exponential backoff.

public class AlphaVantageApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;

    public AlphaVantageApiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;

        // Define a retry policy with exponential backoff
        _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    // Method to get stock data with retry logic
    public async Task<string> GetStockDataWithRetryAsync(string symbol)
    {
        var response = await _retryPolicy.ExecuteAsync(() => 
            _httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}"));
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            Console.WriteLine($"Failed to retrieve data: {response.StatusCode}");
            return null;
        }
    }
}

// Step 4: Implementing API Rate Limiting
// Implement logic to limit the number of API requests to avoid hitting AlphaVantage’s rate limits.
// Use a simple counter or a more advanced approach using in-memory caches or distributed caches.

public class AlphaVantageApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
    private static readonly SemaphoreSlim RateLimitSemaphore = new SemaphoreSlim(5); // Allow 5 concurrent requests

    public AlphaVantageApiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    // Method to get stock data with retry and rate limiting
    public async Task<string> GetStockDataWithRateLimitingAsync(string symbol)
    {
        await RateLimitSemaphore.WaitAsync();
        try
        {
            var response = await _retryPolicy.ExecuteAsync(() => 
                _httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}"));
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine($"Failed to retrieve data: {response.StatusCode}");
                return null;
            }
        }
        finally
        {
            RateLimitSemaphore.Release();
        }
    }
}

// Step 5: Testing and Validating
// Test the implemented error handling, retry logic, and rate limiting in different scenarios, such as network failure, API rate limit exceeded, and invalid requests.


// Lab Exercise 13: Implementing Security and Authentication in a C# Console App

// Objectives
// - Learn to implement basic security measures for a .NET application consuming the AlphaVantage API.
// - Implement authentication to secure API requests using API keys or JWT tokens.
// - Leverage GitHub Copilot to assist in generating security and authentication mechanisms.

// Step 1: Setting Up the Project
// Initialize a new C# console application.
// Install necessary packages such as Microsoft.Extensions.DependencyInjection, System.Net.Http, and System.IdentityModel.Tokens.Jwt for handling JWT tokens.

// Step 2: Creating the AlphaVantage API Client with Basic API Key Authentication
// Create a service class that handles communication with the AlphaVantage API.
// Implement API key authentication by appending the API key to each request.

public class AlphaVantageApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AlphaVantageApiClient(HttpClient httpClient, string apiKey

)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    // Method to get stock data with API key authentication
    public async Task<string> GetStockDataAsync(string symbol)
    {
        var requestUri = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";
        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

// Step 3: Implementing JWT Token Authentication
// Implement JWT token authentication to secure API requests.
// Use the System.IdentityModel.Tokens.Jwt package to create and validate JWT tokens.

public string GenerateJwtToken()
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",
        audience: "yourdomain.com",
        claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, "user") },
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}

// Step 4: Authenticating API Requests Using JWT Tokens
// Secure API requests by including the JWT token in the HTTP headers.
// Validate the JWT token on the receiving end to ensure secure access.

public class AlphaVantageApiClientWithJwt
{
    private readonly HttpClient _httpClient;
    private readonly string _jwtToken;

    public AlphaVantageApiClientWithJwt(HttpClient httpClient, string jwtToken)
    {
        _httpClient = httpClient;
        _jwtToken = jwtToken;
    }

    // Method to get stock data with JWT token authentication
    public async Task<string> GetStockDataAsync(string symbol)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

// Step 5: Implementing Role-Based Authorization (Optional)
// Implement role-based authorization to restrict access to certain API functionalities based on the user’s role.
// Extend the JWT token generation and validation to include roles and claims.

public string GenerateJwtTokenWithRoles(string role)
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, "user"),
        new Claim(ClaimTypes.Role, role)
    };

    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",
        audience: "yourdomain.com",
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}

// Validate roles in API requests
public bool ValidateUserRole(string token, string requiredRole)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey")),
        ValidateIssuer = false,
        ValidateAudience = false
    }, out SecurityToken validatedToken);

    return principal.IsInRole(requiredRole);
}

// Step 6: Testing and Validating Authentication Implementation
// Test the implemented authentication mechanisms by making authenticated requests to the AlphaVantage API.
// Validate that only authenticated requests with valid tokens are allowed access.


// Lab Exercise 14: Implementing a Complex Feature in a .NET Core Web API without a Database

// Objectives
// - Learn to implement and manage data within a .NET Core Web API without relying on a database.
// - Implement dependency injection to manage service lifetimes and dependencies.
// - Code an API that demonstrates handling of a complex feature using in-memory data storage.
// - Leverage GitHub Copilot to assist in writing code and generating entire Web API controllers.

// Step 1: Setting Up the Project
// Initialize a new .NET Core Web API project.
// Install necessary packages such as Microsoft.Extensions.DependencyInjection and Swashbuckle.AspNetCore for API documentation.


// Step 2: Creating Models
// Define data models representing the entities in your application.
// Use attributes to enforce data validation and optimize serialization.

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }
}


// Step 3: Implementing In-Memory Data Storage
// Use an in-memory data structure (e.g., a List<Product>) to store and manage your data within the API.
// Create a service class to handle data operations like adding, updating, and retrieving products.

public class ProductService
{
    private readonly List<Product> _products = new List<Product>();

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existingProduct = GetById(product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }
    }
}


// Step 4: Implementing Dependency Injection
// Register the ProductService in the dependency injection container.
// Configure the service provider to manage dependencies across the application.

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ProductService>();
        services.AddControllers();
        services.AddSwaggerGen(); // For API documentation
    }
}


// Step 5: Coding the API
// Implement API endpoints for CRUD operations using the in-memory data storage.
// Use GitHub Copilot to generate controller actions and logic, leveraging comments to guide its suggestions.

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    // GET: api/product
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }

    // POST: api/product
    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        _productService.Add(product);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }
}


// Step 6: Handling Complex Feature Implementation
// Break down the complex feature into smaller methods or tasks, using the in-memory data storage.
// Use GitHub Copilot to assist with generating code for each part of the feature.

public class ProductService
{
    private readonly List<Product> _products = new List<Product>();

    // Method to handle a complex feature with Copilot's help
    public void HandleComplexFeature(Product product)
    {
        // Implementing part of the feature...
        // Comment guiding Copilot: "Check if product already exists and update or add new."
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }
        else
        {
            _products.Add(product);
        }
    }
}

// Step 7: Testing and Validating the API
// Test the implemented API using tools like Postman or Swagger.
// Validate that all requirements are met and that the API handles complex features correctly.
```