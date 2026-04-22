# BenCore Web Framework 

> A custom Web Framework built in pure C# to demonstrate MVC patterns, Dependency Injection, and Reflection.

## About BenCore

BenCore is the "Application Layer" of a custom infrastructure. While traditional servers bundle everything together, BenCore is designed to be completely decoupled from the network protocol. 

It speaks **TTP (Torff Transfer Protocol)**, receiving structured data from a gateway (like the Torff Web Server) and routing it to specialized **Controllers** using a custom-built Reflection engine.

## Key Features

- **Custom IoC Container:** A built-in Dependency Injection engine that automatically resolves constructor dependencies using recursive Reflection.
- **Reflection-based Routing:** No manual route registration. BenCore scans the assembly for `Http` attributes to map the API at startup.
- **MVC Architecture:** Separation of concerns with a base `BenController` class, providing a clean API for developers (e.g., `Ok()`, `NotFound()`).
- **TTP Integration:** Native support for the Torff Transfer Protocol, allowing for a high-performance, JSON-based internal communication.

## Architecture


```text
src/
├── Core/             # The Host engine (TCP Listeners and TTP Handlers)
├── Mvc/              # The MVC heart (RouteScanner, Attributes, Base Controller)
├── IoC/              # The Dependency Injection Container
├── Controllers/      # Application logic 
└── Repositories/     # Data access layer contracts and implementations
```
## How it Works (The Lifecycle)
Startup: The RouteScanner uses Reflection to map every method with an Http attribute.

- **IoC Setup:** Services and Repositories are registered in the DependencyContainer.

- **Listen:** BenCore waits for TTP packets on port 5000.

- **Resolution:** When a request arrives, the IoC Container instantiates the correct Controller and injects all required dependencies.

- **Execution:** The mapped method is invoked, and the result is serialized back to the Gateway.
