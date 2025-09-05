# UserManagementApi

A simple ASP.NET Core Web API for managing users.  
Supports basic CRUD operations (Create, Read, Update, Delete) with an in-memory data store.  
Includes Swagger UI for easy API testing.

## Features

- List all users
- Get a user by ID
- Create a new user
- Update an existing user
- Delete a user
- API documentation via Swagger UI

## Technologies

- ASP.NET Core (.NET 6+)
- Minimal API
- Swagger (OpenAPI)

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (recommended)

### Running the API

1. **Clone the repository:**
   ```sh
   git clone https://github.com/<your-username>/UserManagementApi.git
   cd UserManagementApi
   ```

2. **Restore dependencies and run:**
   ```sh
   dotnet restore
   dotnet run
   ```

3. **Access Swagger UI:**
   [http://localhost:5232/swagger](http://localhost:5232/swagger)

### API Endpoints

| Verb   | Endpoint         | Description              |
|--------|------------------|--------------------------|
| GET    | `/users`         | Get all users            |
| GET    | `/users/{id}`    | Get user by ID           |
| POST   | `/users`         | Create new user          |
| PUT    | `/users/{id}`    | Update user by ID        |
| DELETE | `/users/{id}`    | Delete user by ID        |

### Example User JSON

```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "9876543210"
}
```

## Project Structure

```
UserManagementApi/
│
├── Controllers/
├── Middleware/
├── Models/
├── wwwroot/
│   └── index.html
├── Program.cs
├── README.md
└── UserManagementApi.csproj
```

## Custom Homepage

A simple homepage is served at `/` via `wwwroot/index.html`.

## License

This project is licensed under the MIT License.

---
