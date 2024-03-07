using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using RestApiExample.Data;
using RestApiExample.Models;

namespace RestApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDataController : Controller // Name convention??
    {
        private readonly RestApiExDbContext _context;
        private readonly ILogger<ProductDataController> _logger;
        private readonly HttpClient _httpClient;


        public ProductDataController(RestApiExDbContext context, ILogger<ProductDataController> logger, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("GetProductDetails")]
        public async Task<IActionResult> GetProductDetails(string sku) // name convention?? 
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.SKU == sku);
                if(product == null)
                {
                    return NotFound("Product with provided SKU does not exist");
                }

                var inventory = _context.Inventories.FirstOrDefault(x => x.SKU == sku);
                var price = _context.Prices.FirstOrDefault(x => x.SKU == sku);

                var productDetails = new // used anonymous type to collect needed data in object 
                {
                    Name = product.Name,
                    EAN = product.EAN,
                    Producer_Name = product.Producer_Name,
                    Category = product.Category,
                    URL = product.Default_Image,
                    Qty = inventory.Qty,
                    Unit = inventory.Unit,
                    Nett_Price = price.Nett_Price,
                    Shipping_Cost = inventory.Shipping_Cost
                };

                var productInfo = $"{sku} {productDetails.Name} {productDetails.EAN} {productDetails.Producer_Name} " +
                    $"{productDetails.Category} {productDetails.URL} /{productDetails.Qty} szt./ " +
                    $" - {productDetails.Nett_Price} for 1 unit, product is sold in a box of {productDetails.Unit} units." +
                    $"Shipping cost price - {productDetails.Shipping_Cost}";

                return Ok(productInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Collecting product info went wrong with error: {ex.Message}"); // maybe modify error messages 
                return StatusCode(500, $"Collecting product info went wrong with error: {ex.Message}");
            }
        
        }

        [HttpPost("ImportProducts")]
        public async Task<IActionResult> ImportProducts()
        {
            try
            {
                var productsFileUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
                var productFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Products.csv");
                await DownloadFile(productsFileUrl, productFilePath); //saving file on local storage

                using (var reader = new StreamReader(productFilePath))
                using (var file = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    var products = file.GetRecords<Product>().Where(x => !x.Is_Wire && x.Shipping <= DateTime.Now.AddDays(1)).ToList();

                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
                return Ok("Products imported successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Importing products went wrong with error: {ex.Message}");
                return StatusCode(500, $"Importing products went wrong with error: {ex.Message}");
            }
        }

        [HttpPost("ImportInventory")]
        public async Task<IActionResult> ImportInventory()
        {
            try
            {
                var inventoryFileUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
                var inventoryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.csv");
                await DownloadFile(inventoryFileUrl, inventoryFilePath); //saving file on local storage

                using (var reader = new StreamReader(inventoryFilePath))
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    var inventoryItems = csv.GetRecords<Inventory>().Where(x => x.Shipping <= DateTime.Now.AddDays(1)).Select(x => new Inventory
                    {
                        Id = x.Id,
                        SKU = x.SKU,
                        Qty = x.Qty,
                        Unit = x.Unit,
                        Shipping = x.Shipping, // Don't know if it is necessary, because we only collect and save products which shipping is set up to 24h

                    }).ToList(); // Data to confirm 

                    await _context.Inventories.AddRangeAsync(inventoryItems);
                    await _context.SaveChangesAsync();
                }
                return Ok("Inventory imported successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Imported inventory items went wrong with error: {ex.Message}");
                return StatusCode(500, $"Imported inventory items went wrong with error: {ex.Message}");
            }
        }

        [HttpPost("ImportPrices")]
        public async Task<IActionResult> ImportPrices()
        {
            try
            {
                var priceFileUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";
                var priceFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Prices.csv");
                await DownloadFile(priceFileUrl, priceFilePath); //saving file on local storage

                using (var reader = new StreamReader(priceFilePath))
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    var prices = csv.GetRecords<Price>().Select(x => new Price
                    {
                        Id = x.Id,
                        SKU = x.SKU,
                        Nett_Price = x.Nett_Price,
                        Discount_Nett_Price= x.Discount_Nett_Price,
                        Vat_Rate = x.Vat_Rate,
                        Logistic_Unit_Nett_Price = x.Logistic_Unit_Nett_Price,
                    }).ToList(); // Data to confirm

                    await _context.Prices.AddRangeAsync(prices);
                    await _context.SaveChangesAsync();
                }
                return Ok("Prices imported successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Imported prices went wrong with error: {ex.Message}");
                return StatusCode(500, $"Imported prices went wrong with error: {ex.Message}");
            }
        }


        private async Task DownloadFile(string url, string destinationPath)
        {
            using (var response = await _httpClient.GetAsync(url))
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
