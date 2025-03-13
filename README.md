# E-commerce Plants

A modern e-commerce platform built with ASP.NET Core 8.0 for selling plants and related products.

## Description

E-commerce Plants is a full-featured online store specializing in plants and gardening supplies. This project demonstrates modern web development practices using ASP.NET Core 8.0 and follows clean architecture principles.

### Key Highlights

- **Modern Architecture**: Built with ASP.NET Core 8.0, following MVC pattern and clean architecture principles
- **Secure & Scalable**: Implements best practices for security and scalability
- **User-Friendly**: Intuitive interface for both customers and administrators
- **Rich Features**: Complete e-commerce solution with product management, shopping cart, and order processing
- **Responsive Design**: Mobile-first approach ensuring great experience across all devices

### Target Users

- Plant enthusiasts looking to purchase plants and gardening supplies
- Garden centers and plant nurseries wanting to expand their online presence
- Administrators managing product inventory and orders
- Customers seeking a seamless online shopping experience

## Features

- 🛍️ Product catalog with categories
- 👤 User authentication and authorization
- 🛒 Shopping cart functionality
- 📱 Responsive design
- 🖼️ Image gallery for products
- 👨‍💼 Admin dashboard for product management
- 🔍 Search functionality
- 💳 Secure payment processing

## Tech Stack

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server
- Bootstrap 5
- jQuery
- CKEditor
- Dropzone.js

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or later (recommended)

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/yourusername/E-commercePlants.git
```

2. Navigate to the project directory:
```bash
cd E-commercePlants
```

3. Update the connection string in `appsettings.json` with your SQL Server details.

4. Run the database migrations:
```bash
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

6. Open your browser and navigate to `https://localhost:5001`

## Project Structure

- `Areas/` - Contains admin area and related views
- `Controllers/` - Application controllers
- `Data/` - Database context and configurations
- `Models/` - Data models and view models
- `Views/` - Application views
- `wwwroot/` - Static files (CSS, JavaScript, images)

## Admin Features

The admin area provides functionality to:
- Manage products (CRUD operations)
- Upload product images
- Manage product categories
- View and manage orders
- Handle user management

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Acknowledgments

- Bootstrap for the UI framework
- Font Awesome for icons
- CKEditor for rich text editing
- Dropzone.js for file uploads