namespace KeramikTracker.Shared.DatabaseModels;

public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }
}
