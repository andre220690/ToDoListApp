using Microsoft.EntityFrameworkCore;
using ToDoListApp.Dal.Models;

namespace ToDoListApp.Dal;

internal sealed class TasksDbContext : DbContext
{
    public DbSet<TaskDb> Tasks { get; set; } = null!;
    public DbSet<TaskStatusDb> TaskStatuses { get; set; } = null!;
    public DbSet<UserDb> Users { get; set; } = null!;

    public TasksDbContext(DbContextOptions<TasksDbContext> dbContextOptions) : base(dbContextOptions)
    {
        if (Database.GetPendingMigrations().Any())
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка отношений между таблицами
        modelBuilder.Entity<TaskDb>()
            .HasOne(t => t.TaskStatus)
            .WithMany(ts => ts.Tasks)
            .HasForeignKey(t => t.TaskStatusId);


        modelBuilder.Entity<TaskStatusDb>().HasData(
            new TaskStatusDb { Id = 1, TaskStatusName = "Новая" },
            new TaskStatusDb { Id = 2, TaskStatusName = "В процессе" },
            new TaskStatusDb { Id = 3, TaskStatusName = "Выполнено" }
        );

        modelBuilder.Entity<TaskDb>().HasData(
            new TaskDb { Id = 1, Text = "Задача 1", TaskStatusId = 1 },
            new TaskDb { Id = 2, Text = "Задача 2", TaskStatusId = 2 },
            new TaskDb { Id = 3, Text = "Задача 3", TaskStatusId = 3 }
        );

        modelBuilder.Entity<UserDb>().HasData(
            new UserDb { Id = 1, UserName = "admin", Password = "password" }
        );
    }
}
