# Retail SOA
Ordinary online retail service with service oriented architecture.  
At some point it may evolve into coarse-grained microservices architecture.
## Services
### Frontend
ASP.NET Core MVC app with MassTransit.  
Will be switched to SPA composed of micro frontends.
### Sales
MassTransit console app. Uses saga to process order in each state.
### Billing
MassTransit console app. Collects payment for the orders. Uses key-value store to keep state.
### Shipping
MassTransit console app. Ships order products to the customer.
### Recommendations
MassTransit console app. Implements "You may also like" feature for products with graph database.
### Search [TODO]
MassTransit console app with API. Provides full-text search for product catalog.
### Catalog
ASP.NET Core Web API. Simple CRUD for products information. Uses Dapper and relational DB.
### Auth [TODO]
ASP.NET Core Web API. Handles sign up/in and issues RSA encrypted JWT tokens.  
Will be switched to OAuth 2.0 / OpenID Connect provider.

## Infrastructure
### Queue
RabbitMQ with custom configuration.
### GraphDb
Neo4j DB.
### RelationalDb
PostgreSQL DB.
### KeyValueDb
Redis
### Elastic
Elasticsearch full-text search engine.
