Product Management System
A web-based Product Management System built with ASP.NET Core MVC, Entity Framework Core, and MySQL. It helps users maintain a product catalogue, track stock, and manage product images.
Features
- View all products in an inventory table.
- Add new products with name, price, category, stock quantity, and description.
- Upload product images in JPG, JPEG, PNG, or WEBP format (maximum 5 MB).
- Edit product details and replace an existing product image.
- Delete products and their uploaded images.
- Search the displayed product list by product name, category, or price.
- View inventory summary cards:
  - Total number of products
  - Total inventory value
  - Low-stock count (1–5 items)
  - Out-of-stock count
Technology Stack
- ASP.NET Core MVC
- C#
- Entity Framework Core
- Pomelo.EntityFrameworkCore.MySql
- MySQL
- Razor Views
- HTML, CSS, JavaScript
- Bootstrap
Project Structure
ProductManagementSystem/
├── Controllers/
│   ├── HomeController.cs
│   └── ProductsController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/
├── Models/
│   ├── ErrorViewModel.cs
│   └── Product.cs
├── Views/
│   ├── Home/
│   ├── Products/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── images/
│   ├── js/
│   └── lib/
├── appsettings.json
├── Program.cs
└── ProductManagementSystem.csproj
Requirements
Install the following before running the project:
- .NET SDK compatible with the project's target framework (net10.0)
- MySQL Server 8.0 or a compatible MySQL server
- Visual Studio Code or Visual Studio
- .NET Entity Framework Core CLI (dotnet-ef) for database migrations, if needed
Setup and Run
1. Clone the repository
git clone https://github.com/YOUR-USERNAME/ProductManagementSystem.git
cd ProductManagementSystem
Replace YOUR-USERNAME with the GitHub account that owns the repository.
2. Create the MySQL database
Open MySQL Workbench or a MySQL terminal and run:
CREATE DATABASE productdb;
3. Configure the database connection
Open appsettings.json and configure the DefaultConnection connection string for your local MySQL server.

Example (replace the values with your own local credentials):
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=productdb;User=YOUR_MYSQL_USER;Password=YOUR_MYSQL_PASSWORD;"
  }
}
Security: Do not commit real database passwords or other secrets to GitHub. Prefer .NET User Secrets or environment variables for local credentials. If a password has already been pushed publicly, change/rotate it.
4. Restore dependencies
dotnet restore
5. Apply database migrations
The repository includes Entity Framework Core migrations. If the EF CLI is installed, run:
dotnet ef database update
If dotnet ef is not installed, install a tool version compatible with the EF Core packages used by the project.
6. Run the application
dotnet run
Open the local URL printed in the terminal (for example, https://localhost:xxxx or http://localhost:xxxx).

The default route opens the Products inventory page.
Using the Application
1. Open the Products page.
2. Select Add New Product to create a product.
3. Enter the product details and optionally choose an image.
4. Select Edit to update a product.
5. Select Delete to remove a product after reviewing the confirmation page.
6. Use the search field to filter the products currently displayed.
Uploaded images are stored under wwwroot/images. Product records are stored in the MySQL productdb database.
Image Upload Notes
- Supported file extensions: .jpg, .jpeg, .png, .webp
- Maximum file size: 5 MB
- Images are saved with generated file names in wwwroot/images.
Notes for Contributors
- Do not commit bin/ or obj/ build output.
- Do not commit local credentials, private configuration, or personal data.
- Test create, edit, delete, image upload, and database connectivity after making changes.
License
No license has been specified for this repository. Unless a license is added, the project should be treated as having all rights reserved by its owner.
