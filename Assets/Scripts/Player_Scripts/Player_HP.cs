using UnityEngine;

public class Player_HP : MonoBehaviour, IHealth
{
    [SerializeField] private Player_UI playerUI;
    [SerializeField] private Game_Over gameOver;

    public void ChangeHP(int amount)
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentHP, amount);

        if (amount != 0)
        {
            playerUI.UpdateHPUI(amount);
            playerUI.ShowHitNumber(amount);

            if (Game_Manager.instance.statsManager.CurrentHP <= 0)
            {
                Game_Manager.instance.isCombatActive = false;
                gameOver.DisplayGameOverScreen();
            }
        }
    }
}
