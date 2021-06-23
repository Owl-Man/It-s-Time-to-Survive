using System.Collections;
using UnityEngine;

public abstract class MagicanAIBase : EnemyAIBase, IEnemyAI, IMagicanEnemyAI
{
    [HideInInspector] public GameObject Magic;

    public override void AttackPlayer() 
    {
        base.AttackPlayer();
        Magic.SetActive(true);
    }

    public override void BeforeDie() => Magic.SetActive(false);
}
