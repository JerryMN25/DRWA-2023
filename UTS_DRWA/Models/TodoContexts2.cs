using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext2 : DbContext
{
    public TodoContext2(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<Mapel> TodoItems { get; set; } = null!;

}