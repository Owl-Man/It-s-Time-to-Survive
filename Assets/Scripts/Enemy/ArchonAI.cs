using System.Collections;
using UnityEngine;

public class ArchonAI : MagicanAIBase, IEnemyAI, IMagicanEnemyAI
{
    public BossFightSystem bossFight;

    private void Start() => Magic = links.DarkMagicPlayer;

    public override void BeforeDie() 
    {
        Magic.SetActive(false);
        if (bossFight != null) bossFight.Victory();
    }

    public override void ChangeEXPValue() => values.ChangeEXPValue(30f);
}
