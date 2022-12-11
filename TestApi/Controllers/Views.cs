using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    // rest api endpoint
    public class Views : Controller
    {
        private FetchDataContactDbContext dbContext;

        public Views(FetchDataContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(dbContext.UserInfo.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> AddContacts(AddUserInfo addInfo)
        {
            var contacts = new Contact()
            {
                Id =  Guid.NewGuid(),
                Address= addInfo.Address,
                Name= addInfo.Name,
                PhoneNumber= addInfo.PhoneNumber,
                PostalCode= addInfo.PostalCode,
                City= addInfo.City,
                Region= addInfo.Region,
                Email= addInfo.Email,
            };
            await dbContext.UserInfo.AddAsync(contacts);
            await dbContext.SaveChangesAsync();
            return Ok(contacts);


        }
    }
}
