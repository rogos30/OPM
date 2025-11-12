
public abstract class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Resurrects { get; set; }
    public int Amount { get; set; }
    public int Cost { get; set; }
    public int Id { get; set; }

    public void Add(int amount)
    {
        Amount += amount;
    }

    public abstract string Use(FriendlyCharacter source, FriendlyCharacter target);
}
