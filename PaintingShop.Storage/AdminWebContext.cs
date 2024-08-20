using Microsoft.EntityFrameworkCore;
using PaintingShop.Storage.Maps;

namespace PaintingShop.Storage;
public class AdminWebContext : DbContext
{
    public DbSet<AdminMap> Admins { get; set; }
}
