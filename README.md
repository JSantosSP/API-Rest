# Simple REST API

This is a simple REST API built with .NET Core 5, implementing CRUD functionalities using the Model-View-Controller (MVC) pattern.

## Features

- **CRUD Operations**: Supports Create, Read, Update, and Delete operations.
- **Models**:
  - **Truck**: Represents a truck with the following attributes:
    - `id`
    - `ubication`
    - `state`
    - `fleetId`
  - **Fleet**: Represents a fleet with the following attributes:
    - `id`
    - `company`
  
- **Data Generation**: The project includes a file in the `Utils` folder (`DataStore.cs`) that generates sample data for testing, including 10 Trucks and 3 Fleets.

## Documentation and Testing

To view the API documentation and facilitate testing, the project utilizes Swagger. You can access the Swagger UI after running the application to explore the available endpoints and try out the API functionalities easily.
