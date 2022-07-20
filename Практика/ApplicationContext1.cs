using Microsoft.EntityFrameworkCore;
public class ApplicationContext1 : DbContext
{
    public DbSet<User1> Users { get; set; } = null!;
    public ApplicationContext1(DbContextOptions<ApplicationContext1> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User1>().HasData(
                new User1 { Id = 1, Name = "Tom", Age = 37 , Hobby = "шахматы" },
                new User1 { Id = 2, Name = "Bob", Age = 41 , Hobby = "спорт" },
                new User1 { Id = 3, Name = "Sam", Age = 24  , Hobby = "приключения" }
        );
    }
}