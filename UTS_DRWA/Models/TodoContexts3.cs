using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext3 : DbContext
{
    public TodoContext3(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<Jadwal> TodoItems { get; set; } = null!;

}