public interface IHealth
{
    void ChangeHP(int amount);
}

public interface IHitNumber
{
    void ShowHitNumber(int damage, bool isBlocked = false);
}
