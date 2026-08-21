using UnityEngine;

public class Player_HP : MonoBehaviour
{
    [SerializeField] private Camera_Shake cameraShake;
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private Game_Over gameOver;

    private void OnEnable() => Combat_Manager.DamagePlayer += OnDamagePlayer;
    private void OnDisable() => Combat_Manager.DamagePlayer -= OnDamagePlayer;

    private void Start()
    {
        ChangeHP(0);
    }

    private void OnDamagePlayer(int amount, bool _)
    {
        ChangeHP(amount);
    }

    private void ChangeHP(int amount)
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentHP, amount);

        if (amount < 0) cameraShake.Shake(); // camera hit shake
        uiHP.UpdateHP(Game_Manager.instance.statsManager.CurrentHP, Game_Manager.instance.statsManager.MaxHP, amount); // update UI

        if (Game_Manager.instance.statsManager.CurrentHP <= 0)
        {
            Game_Manager.instance.isCombatActive = false;
            gameOver.DisplayGameOverScreen();
        }

    }
}
