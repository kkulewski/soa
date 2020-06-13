using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Retail.Catalog.Web.Models;

namespace Retail.Catalog.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IConfiguration configuration;

        private IDbConnection DbConnection =>
            new NpgsqlConnection(configuration.GetValue<string>("Db:ConnectionString"));

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (var dbConnection = this.DbConnection)
            {
                dbConnection.Open();

                const string sql = "SELECT * FROM product";
                var products = await dbConnection.QueryAsync<Product>(sql);

                return Ok(products);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            using (var dbConnection = this.DbConnection)
            {
                dbConnection.Open();

                const string sql = "SELECT * FROM product WHERE productId = @Id";
                var product = (await dbConnection.QueryAsync<Product>(sql, new {Id = id})).SingleOrDefault();

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            using (var dbConnection = this.DbConnection)
            {
                dbConnection.Open();

                try
                {
                    const string sql = "INSERT INTO product (productId, name, description) VALUES (@Id, @Name, @Description)";
                    await dbConnection
                        .ExecuteAsync(sql, new
                        {
                            Id = product.ProductId,
                            Name = product.Name,
                            Description = product.Description
                        });

                    return CreatedAtAction(nameof(Get), new {id = product.ProductId}, product);
                }
                catch (PostgresException e) when (e.SqlState == "23505")
                {
                    return Conflict();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "Could not add product to DB.");
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Product product)
        {
            using (var dbConnection = this.DbConnection)
            {
                dbConnection.Open();

                try
                {
                    const string sql = "UPDATE product SET name = @Name, description = @Description WHERE productId = @Id";
                    var modifiedRows = await dbConnection
                        .ExecuteAsync(sql, new
                        {
                            Id = id,
                            Name = product.Name,
                            Description = product.Description
                        });

                    if (modifiedRows == 0)
                    {
                        return NotFound();
                    }

                    return NoContent();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, $"Could not update product with ID {id}.");
                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            using (var dbConnection = this.DbConnection)
            {
                dbConnection.Open();

                try
                {
                    const string sql = "DELETE FROM product WHERE productId = @Id";
                    var modifiedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

                    if (modifiedRows == 0)
                    {
                        return NotFound();
                    }

                    return NoContent();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, $"Could not delete product with ID {id}.");
                    return BadRequest();
                }
            }
        }
    }
}
