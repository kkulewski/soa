# Retail SOA
Ordinary online retail service with service oriented architecture.  
At some point it may evolve into coarse-grained microservices architecture.
## Services
### Frontend
ASP.NET Core MVC app with NServiceBus.  
Will be switched to React/Vue SPA + API Gateway.
### Sales
NServiceBus console app. Uses saga to process order in each state.
### Billing
NServiceBus console app. Collects payment for the orders. Uses key-value store to keep state.
### Shipping
NServiceBus console app. Ships order product to the customer.
### Recommendations
NServiceBus console app. Implements "You may also like" feature for products with graph database.
### Search
NServiceBus console app with API. Provides full-text search for product catalog.
### Catalog
ASP.NET Core Web API with NServiceBus. Simple CRUD for products information. Uses Dapper and relational DB.
### Auth
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
