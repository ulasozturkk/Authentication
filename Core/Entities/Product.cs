namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public User User { get; set; }
}
