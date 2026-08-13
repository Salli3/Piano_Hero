public interface IBuy
{
    string Name { get; }
    string Description { get; }
    int Level { get; }
    public void BuyItem();
}
