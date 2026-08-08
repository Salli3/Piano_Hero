//For the limit of this game interface actually dont have that much use

//In the situation of having many combatant, attack with collider to get 
//the entity that got hit to pass into combat handler then the interfaces
//will show a much better use since it able to work with more abstraction

//==Dev note for future resusement of code==

public interface IHealth
{
    void ChangeHP(int amount);
}

public interface IHitNumber
{
    void ShowHitNumber(int damage, bool isBlocked = false);
}

public interface IStatus
{
    void UpdateCombatStatusUI(int block, int stackingDamage, int boostTime);
}
