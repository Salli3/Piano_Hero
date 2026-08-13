[System.Serializable]
public struct Status
{
    public int block;
    public int damageStack;
    public int attackBoost;

    public Status(int block, int damageStack, int attackBoost)
    {
        this.block = block;
        this.damageStack = damageStack;
        this.attackBoost = attackBoost;
    }

    public override string ToString()
    {
        return $"Status(block:{block}, stack:{damageStack}, boost:{attackBoost})";
    }
}