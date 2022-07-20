using Microsoft.EntityFrameworkCore;
public class ApplicationContext2 : DbContext
{
    public DbSet<User2> Users { get; set; } = null!;
    public ApplicationContext2(DbContextOptions<ApplicationContext2> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User2>().HasData(
                new User2 { Id = 1, Name = "Тачка 1", Color = "чёрный" , Age = 1005 },
                new User2 { Id = 2, Name = "Тачка 2", Color = "зелёный" , Age = 777 },
                new User2 { Id = 3, Name = "Тачка 3", Color = "красный" , Age = 897 }
        );
    }
}