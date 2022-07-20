using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Практика.Controllers
{
    public class Two_Controller : Controller
    {
        ApplicationContext2 db;
        public Two_Controller(ApplicationContext2 context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User2
                {
                });
                db.SaveChanges();
            }
        }
        [HttpGet("api/users2")]
        public async Task<ActionResult<IEnumerable<User2>>> Get()
        {
            return await db.Users.ToListAsync();
        }
        [HttpGet("api/users2/{id}")]
        public async Task<ActionResult<User2>> Get(int id)
        {
            User2? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(user);
            }
        }
        [HttpPost("api/users2")]
        public async Task<ActionResult<User1>> Post(User2 user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut("api/users2")]
        public async Task<ActionResult<User1>> Put(User2 userData)
        {
            //получаем пользователя по id
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, изменяем его данные и отправляем обратно клиенту
            user.Age = userData.Age;
            user.Name = userData.Name;
            user.Color = userData.Color;
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("api/users2/{id}")]
        public async Task<ActionResult<User1>> Delete(int id)
        {
            User2? user = db.Users.FirstOrDefault(x => x.Id == id);
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