namespace PaintingShop.Storage.Maps;

public class ReserveMap
{
    public uint Id { get; set; }
    public PictureMap Picture { get; set; }
    public ClientMap Client { get; set; }
    public DateTime Date { get; set; }
    public ReserveState State { get; set; }
}

public enum ReserveState
{
    Active,
    InActive
}
