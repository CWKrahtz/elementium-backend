

#### Server Side Installation

   ```URL
   https://github.com/CWKrahtz/elementium-backend.git
   ```
1. Clone the repository:
   ```bash
    git clone https://github.com/CWKrahtz/elementium-backend.git
   ```
2. Navigate to the project directory:
   ```bash
    cd elementium-backend
   ```
3. Restore the .NET dependencies:
   ```bash
    dotnet restore
   ```
4. Set up your database (if possible). Ensure the connection string in `appsettings.json` is correct:
   ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
    }
   ```

5. Create Migration
   ```bash
    dotnet ef migrations add MigrationName
   ```

6. Apply any pending migrations:
   ```bash
    dotnet ef database update
   ```
### Running the Application
<p>To run the application locally, use the following command:</p>

```bash
  dotnet run
```
<p>The API should now be running at `https://localhost:5001` or `http://localhost:5000 for HTTP`.</p>

### Using Swagger
<p>This project uses <a href="https://swagger.io/">Swagger</a> for API documentation. Once the application is running, you can access the Swagger UI by navigating to:</p>

```
  https://localhost:5001/swagger
```
# Fronend Repository
#### Please refer to [This Repository](https://github.com/Bladeyboy54/Elementium-frontend) for the Frontend

## Build With
<ul>
  <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">.NET 6</a> - The web Framework used</li>
  <li><a href="https://swagger.io/">Swagger</a> - API documentation</li>
</ul>

<p>The Swagger UI provides a web-based interface to explore and interact with the API endpoints.</p>

## Contributing
<p>If you'd like to contribute to this project, please fork the repository and submit a pull request. For major changes, please open an issue first to discuss what you would like to change.</p>

1. Fork the Project

2. Fork the Project
   ```bash
    git checkout -b feature/AmazingFeature
   ```
3. Commit your Changes
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. Push to the Branch
   ```bash
    git push origin feature/AmazingFeature
   ```
5. Open a Pull Request

## License

<p>This project is licensed under the MIT License</p>


[contributors-shield]: https://img.shields.io/github/contributors/Bladeyboy54/Elementium-frontend.svg?style=for-the-badge
[contributors-url]: https://github.com/Bladeyboy54/Elementium-frontend/graphs/contributors
[Forks]: https://img.shields.io/github/forks/Bladeyboy54/Elementium-frontend.svg?style=for-the-badge
[Forks-url]: https://github.com/Bladeyboy54/Elementium-frontend/forks
[Stars]: https://img.shields.io/github/stars/Bladeyboy54/Elementium-frontend.svg?style=for-the-badge
[Stars-url]: https://github.com/Bladeyboy54/Elementium-frontend/stargazers
