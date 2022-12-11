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
        public IActionResult GetUserInfo()
        {
            return Ok(dbContext.UserInfo.ToList());
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

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, updateInfo update)
        {

            var model = dbContext.UserInfo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            model.Name = update.Name;
            model.PhoneNumber = update.PhoneNumber;
            model.PostalCode = update.PostalCode;
            model.City = update.City;   
            model.Region = update.Region;
            model.Email = update.Email;
            model.Address = update.Address;

            await dbContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserInformation([FromRoute] Guid id)
        {
            var userInfo = dbContext.UserInfo.Find(id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return Ok(userInfo);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<ActionResult> DeleteUserFromId([FromRoute] Guid id)
        {
            var getInfo = dbContext.UserInfo.Find(id);
            if (getInfo == null)
            {
                return NotFound();
            }
            dbContext.Remove(getInfo);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
