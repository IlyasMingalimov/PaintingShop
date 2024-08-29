using Microsoft.EntityFrameworkCore;
using PaintingShop.Storage.Maps;

namespace PaintingShop.Storage;
public class PaintingShopContext : DbContext
{
    DbSet<PictureMap> Pictures {  get; set; }
    DbSet<ClientMap> Clients { get; set; }
    DbSet<ReserveMap> Reserves { get; set; }
}