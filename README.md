# Retail SOA
Ordinary online retail service with service oriented architecture.  
At some point it may evolve into coarse-grained microservices architecture.
## Services
### Retail.Queue
RabbitMQ with custom configuration. Transport for MassTransit service bus.
### Retail.Frontend
SPA in Vue.js 3. Combines multiple microfrontends into a single UI for the application.  
Uses OIDC-Client to obtain access and identity tokens.  
![Microfrontends](01_microfrontends.png) 
### Retail.Sales
ASP.NET Core Web API with MassTransit. Uses saga to process order in each state. Pushes logs to ElasticSearch with Serilog.
### Retail.Sales.Microfrontend
WebComponents in vanilla JavaScript, served with nginx. Provides fragments for other views.  
- `<sales-buy>` - displays "buy button" for product with given ID, emits `product_added_to_cart` event
- `<sales-cart>` - displays cart count, consumes `product_added_to_cart` to update state
### Retail.Billing
MassTransit console app. Collects payment for the orders. Uses key-value store to keep state.
### Retail.Billing.Database [TODO]
Redis key-value store.
### Retail.Shipping
MassTransit console app. Ships order products to the customer. Manages state in NoSQL database.
### Retail.Shipping.Database
MongoDB NoSQL database.
### Retail.Catalog
ASP.NET Core Web API. Simple CRUD for products information. Uses Dapper with relational database.
### Retail.Catalog.Database
PostgreSQL relational database.
### Retail.Catalog.Frontend
Svelte UI for product catalog CRUD operations. Uses no-auth endpoint.
### Retail.Recommendations [TODO]
MassTransit console app. Implements "You may also like" feature for products with graph database.
### Retail.Recommendations.Database
Neo4j graph database.
### Retail.Elastic
ELK stack - ElasticSearch. Used to aggregate logs from services.
### Retail.Kibana
ELK stack - Kibana. Used to browse logs from services.
### Retail.Auth
IdentityServer4. Handles OICD authentication and OAuth 2.0 authorization with RSA256 signed JWT.  
RSA is preffered over HMAC, as such tokens can be verified by other services which cached Auth's public key.
### Retail.Jaeger
Jaeger instance. Used to collect and visualize traces of inter-service requests in OpenTelemetry format.
### Retail.Http
Example HTTP requests to various services.
