using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebService.DTO;
using WebService.Entities;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserinfoController : ControllerBase
    {
        private readonly DatabaseContext DBContext;

        public UserinfoController(DatabaseContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserinfoDTO>>> Get()
        {
            var List = await DBContext.Userinfos.Select(
                s => new UserinfoDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    Password = s.Password,
                    EnrollmentDate = s.EnrollmentDate
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserinfoDTO>> GetUserById(int Id)
        {
            UserinfoDTO? User = await DBContext.Userinfos.Select(s => new UserinfoDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Username = s.Username,
                Password = s.Password,
                EnrollmentDate = s.EnrollmentDate
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserinfoDTO User)
        {
            var entity = new Userinfo()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Username = User.Username,
                Password = User.Password,
                EnrollmentDate = User.EnrollmentDate
            };
            DBContext.Userinfos.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserinfoDTO User)
        {
            var entity = await DBContext.Userinfos.FirstOrDefaultAsync(s => s.Id == User.Id);
            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.EnrollmentDate = User.EnrollmentDate;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new Userinfo()
            {
                Id = Id
            };
            DBContext.Userinfos.Attach(entity);
            DBContext.Userinfos.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
