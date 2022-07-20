using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Практика.Controllers
{
    public class One_Controller : Controller
    {
        ApplicationContext1 db;
        public One_Controller(ApplicationContext1 context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User1
                {
                });
                db.SaveChanges();
            }
        }
        [HttpGet("api/users")]
        public async Task<ActionResult<IEnumerable<User1>>> Get()
        {
            return await db.Users.ToListAsync();
        }
        [HttpGet("api/users/{id}")]
        public async Task<ActionResult<User1>> Get(int id)
        {
            User1? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(user);
            }
        }
        [HttpPost("api/users")]
        public async Task<ActionResult<User1>> Post(User1 user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut("api/users")]
        public async Task<ActionResult<User1>> Put(User1 userData)
        {
            //получаем пользователя по id
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, изменяем его данные и отправляем обратно клиенту
            user.Age = userData.Age;
            user.Name = userData.Name;
            user.Hobby = userData.Hobby;
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("api/users/{id}")]
        public async Task<ActionResult<User1>> Delete(int id)
        {
            User1? user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
