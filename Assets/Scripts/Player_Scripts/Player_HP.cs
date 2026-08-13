using UnityEngine;

public class Player_HP : MonoBehaviour
{
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private Player_UI playerUI;
    [SerializeField] private Game_Over gameOver;

    private void OnEnable() => Combat_Manager.DamagePlayer += (amount, _) => ChangeHP(amount);
    private void OnDisable() => Combat_Manager.DamagePlayer -= (amount, _) => ChangeHP(amount);

    public void ChangeHP(int amount)
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentHP, amount);
        uiHP.UpdateHP(Game_Manager.instance.statsManager.CurrentHP, Game_Manager.instance.statsManager.MaxHP, amount);

        if (Game_Manager.instance.statsManager.CurrentHP <= 0)
        {
            Game_Manager.instance.isCombatActive = false;
            gameOver.DisplayGameOverScreen();
        }

    }
}
