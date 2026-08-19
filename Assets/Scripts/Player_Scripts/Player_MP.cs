using UnityEngine;

public class Player_MP : MonoBehaviour
{
    [SerializeField] private Ultimate_UI ultimateUI;
    [SerializeField] private Combat_Manager combatManager;
    [SerializeField] private UI_MP uiMP;

    private void OnEnable() => Combat_Manager.PlayerGainMP += ChangeMP;
    private void OnDisable() => Combat_Manager.PlayerGainMP -= ChangeMP;

    private void Start()
    {
        ChangeMP(0);
    }

    private void ChangeMP(int amount)
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentMP, amount);
        uiMP.UpdateMP(Game_Manager.instance.statsManager.CurrentMP, Game_Manager.instance.statsManager.MaxMP, amount);

        if (Game_Manager.instance.statsManager.CurrentMP >= Game_Manager.instance.statsManager.MaxMP)
        {
            ultimateUI.ShowUltimateEffect(
                Game_Manager.instance.statsManager.Player.playerSprite,
                Game_Manager.instance.statsManager.Player.playerName, 
                Game_Manager.instance.statsManager.Player.ultimate.Name);
            Game_Manager.instance.statsManager.Player.ultimate.Apply(combatManager);
            Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentMP, -Game_Manager.instance.statsManager.MaxMP);
            uiMP.UpdateMP(Game_Manager.instance.statsManager.CurrentMP, Game_Manager.instance.statsManager.MaxMP, -Game_Manager.instance.statsManager.MaxMP);
        }
    }
}
