# Retail SOA
Ordinary online retail service with service oriented architecture.  
At some point it may evolve into coarse-grained microservices architecture.
## Services
### Retail.Queue
RabbitMQ with custom configuration. Transport for MassTransit service bus.
### Retail.Frontend
ASP.NET Core MVC app with MassTransit.  
Will be switched to SPA composed of micro frontends.
### Retail.Sales
ASP.NET Core Web API with MassTransit. Uses saga to process order in each state.
### Retail.Billing
MassTransit console app. Collects payment for the orders. Uses key-value store to keep state.
### Retail.Billing.Database [TODO]
Redis key-value store.
### Retail.Shipping
MassTransit console app. Ships order products to the customer. Manages state in NoSQL database.
### Retail.Shipping.Database [TODO]
MongoDB NoSQL database.
### Retail.Catalog
ASP.NET Core Web API. Simple CRUD for products information. Uses Dapper with relational database.
### Retail.Catalog.Database
PostgreSQL relational database.
### Retail.Catalog.Frontend
Svelte UI for product catalog CRUD operations.
### Retail.Recommendations
MassTransit console app. Implements "You may also like" feature for products with graph database.
### Retail.Recommendations.Database
Neo4j graph database.
### Retail.Search [TODO]
ELK stack. Provides full-text search for various services.
### Retail.Auth [TODO]
ASP.NET Core Web API. Handles sign up/in and issues RSA encrypted JWT tokens.  
Will be switched to OAuth 2.0 / OpenID Connect provider.
### Retail.Http
Example HTTP requests to various services.
