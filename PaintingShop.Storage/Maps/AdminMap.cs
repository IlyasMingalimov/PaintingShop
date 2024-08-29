namespace PaintingShop.Storage.Maps;
public class AdminMap
{
    public uint Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string HashSalt { get; set; }
    public string Name { get; set; }
}
