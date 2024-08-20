namespace PaintingShop.Storage;

public class PictureMap 
{ 
    public uint Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public PictureState State { get; set; }
}

public enum PictureState
{
    None,
    Delete
}
