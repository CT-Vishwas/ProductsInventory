# ProjectInventory.API

ProjectInventory.API is a RESTful web service designed to manage product inventories efficiently. It provides endpoints for creating, reading, updating, and deleting product records, as well as tracking inventory levels.

## Features

- CRUD operations for products
- Inventory tracking and management
- API authentication and authorization
- Error handling and validation

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Database (SQL Server, PostgreSQL, etc.)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/vishwasks32/productsinventory.git
    ```
2. Navigate to the project directory:
    ```bash
    cd ProductsInventory.Api
    ```
3. Restore dependencies and build the project:
    ```bash
    dotnet restore
    dotnet build
    ```

### Configuration

- Update `appsettings.json` with your database connection string and other settings.

### Running the API

1. Run the migrations & updates

```bash
dotnet ef database update
```

2. Run the app
```bash
dotnet run
```

The API will be available at `http://localhost:5155` by default.

## API Endpoints

| Method | Endpoint           | Description                |Authentication|
|--------|--------------------|----------------------------|--------------|
| GET    | /api/products      | List all products          | Not Required |
| GET    | /api/products/{id} | Get product by ID          | Not Required |
| POST   | /api/products      | Create a new product       | Requires "admin" role |
| PUT    | /api/products/{id} | Update an existing product | Requires "admin" role |
| DELETE | /api/products/{id} | Delete a product           | Requires "admin" role |

## Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements.

## License

This project is licensed under the MIT License.