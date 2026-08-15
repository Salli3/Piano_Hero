using UnityEngine;

public interface IBuy
{
    string Name { get; }
    string Description { get; }
    int Level { get; }
    Color Color { get; }
    public void BuyItem();
}
