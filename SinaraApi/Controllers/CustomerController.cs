using SinaraLib.Models;

namespace SinaraApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase {
    private readonly HrDbContext db;
    private readonly DirectoryEntryService directoryEntryService;
    private readonly ILogger<CustomerController> logger;

    public CustomerController(HrDbContext db, DirectoryEntryService directoryEntryService, ILogger<CustomerController> logger) {
        this.db = db;
        this.directoryEntryService = directoryEntryService;
        this.logger = logger;
    }

    [HttpGet("[action]")]
    public async Task<List<Customer>> Gets() {
        logger.LogInformation("Get list of customers");
        var result = await db.Customers.Where(n => !n.IsDeleted).ToListAsync();
        return result;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get(int id) {
        logger.LogInformation($"Get customer {id}");
        var customer = await db.Customers.FirstOrDefaultAsync(n => n.Id == id);
        if (customer is null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost("[action]")]
    public async Task Add(Customer customer) {
        logger.LogInformation($"Add customer {customer.LastName}");
        db.Add(customer);
        await db.SaveChangesAsync();
    }

    [HttpPut("[action]")]
    public async Task Update(Customer customer) {
        logger.LogInformation($"Save customer {customer.Id}");
        db.Update(customer);
        await db.SaveChangesAsync();
    }


    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(int id) {
        logger.LogInformation($"Delete customer {id}");
        var customer = await db.Customers.FindAsync(id);
        if (customer is null)
            return NotFound();

        customer.IsDeleted = true;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("[action]")]
    public async Task<bool> Validation(KeyValuePair<string, string> login) {
        logger.LogInformation($"Validation login {login.Value}");
        return await directoryEntryService.ValidationLogin(login.Value);
    }
}