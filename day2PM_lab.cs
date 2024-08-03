// Lab Exercise 15: Building a Complete SQL Data Access Layer in a C# Console App using ADO.NET

// Objectives
// - Learn to connect to a SQL database and implement a complete Data Access Layer (DAL) using ADO.NET in a .NET console application.
// - Implement advanced SQL operations, including parameterized queries, stored procedures, transactions, and error handling.
// - Integrate CRUD operations (Create, Read, Update, Delete) with additional features like filtering, sorting, and pagination.
// - Leverage GitHub Copilot to assist in writing efficient SQL queries, stored procedures, and database interaction code.

// Step 1: Setting Up the Project
// Initialize a new C# console application.
// Install necessary packages such as System.Data.SqlClient for SQL database interaction.

// Step 2: Designing the SQL Database
// Create a SQL database named "CompanyDB".
// Create tables to store information about employees, departments, and projects. Define relationships between the tables using foreign keys.

// Example Schema:
// - Employees: EmployeeID (PK), Name, Position, DepartmentID (FK), HireDate, Salary
// - Departments: DepartmentID (PK), DepartmentName
// - Projects: ProjectID (PK), ProjectName, DepartmentID (FK)
// - EmployeeProjects: EmployeeID (FK), ProjectID (FK)

// Step 3: Creating the SQL Database Client
// Create a service class that handles communication with the SQL database.
// Implement methods for the following operations:
// - Inserting new records into the Employees, Departments, and Projects tables.
// - Retrieving records with filtering, sorting, and pagination options.
// - Updating existing records.
// - Deleting records.
// - Executing stored procedures for complex queries.

// Example:
public class SqlDatabaseClient
{
    private readonly string _connectionString;

    public SqlDatabaseClient(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Method to insert a new employee record
    public void AddEmployee(string name, string position, int departmentId, DateTime hireDate, decimal salary)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Employees (Name, Position, DepartmentID, HireDate, Salary) VALUES (@name, @position, @departmentId, @hireDate, @salary)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@departmentId", departmentId);
                command.Parameters.AddWithValue("@hireDate", hireDate);
                command.Parameters.AddWithValue("@salary", salary);
                command.ExecuteNonQuery();
            }
        }
    }

    // Method to retrieve employees with filtering, sorting, and pagination
    public List<string> GetEmployees(string filter = null, string sortColumn = "Name", bool ascending = true, int pageIndex = 1, int pageSize = 10)
    {
        List<string> employees = new List<string>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT Name, Position FROM Employees WHERE (@filter IS NULL OR Name LIKE @filter) ORDER BY "
                + sortColumn + (ascending ? " ASC" : " DESC") + " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@filter", filter ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@offset", (pageIndex - 1) * pageSize);
                command.Parameters.AddWithValue("@pageSize", pageSize);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add($"{reader["Name"]} - {reader["Position"]}");
                    }
                }
            }
        }
        return employees;
    }

    // Add methods for Update, Delete, and executing stored procedures
}

// Step 4: Implementing Stored Procedures
// Create stored procedures in the SQL database to encapsulate complex queries or operations that involve multiple steps.
// Examples:
// - GetEmployeesByDepartment: Retrieves all employees for a specific department.
// - GetDepartmentBudget: Calculates the total salary of all employees in a department.

// Example Stored Procedure:
CREATE PROCEDURE GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT Name, Position FROM Employees WHERE DepartmentID = @DepartmentID;
END

// Modify the SqlDatabaseClient class to execute these stored procedures.

// Step 5: Handling Transactions and Errors
// Modify the SQL database client to handle transactions for operations that involve multiple steps (e.g., moving an employee from one department to another).
// Implement robust error handling using try-catch blocks and SQL transaction management.

// Example Transaction:
public void TransferEmployee(int employeeId, int newDepartmentId)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            // Update employee's department
            string query = "UPDATE Employees SET DepartmentID = @newDepartmentId WHERE EmployeeID = @employeeId";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@employeeId", employeeId);
                command.Parameters.AddWithValue("@newDepartmentId", newDepartmentId);
                command.ExecuteNonQuery();
            }

            // Commit the transaction
            transaction.Commit();
        }
        catch
        {
            // Rollback the transaction if an error occurs
            transaction.Rollback();
            throw;
        }
    }
}

// Step 6: Using GitHub Copilot to Assist
// Use GitHub Copilot to generate additional methods, stored procedures, and optimize existing code by providing suggestions for improving SQL queries and C# code structure.

// Step 7: Testing the Application
// Create a comprehensive testing suite to ensure that all CRUD operations, stored procedures, and transactions work correctly.
// Test different scenarios including successful transactions, rolled-back transactions, and error handling.

// Step 8: Reflection
// Discuss how GitHub Copilot assisted in the development process.
// Reflect on best practices for secure and efficient database interaction, including the use of stored procedures, parameterized queries, and transaction management.


// Lab Exercise 16: Implementing a Complex Feature in a .NET Core Web API without a Database

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