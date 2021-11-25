using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Retail.Catalog.Web.Controllers
{
    [Route("[controller]")]
    [Authorize("ApiScope")]
    public class ProductController : ProductAdminController
    {
        public ProductController(ILogger<ProductController> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}
