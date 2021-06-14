using System.Collections;
using UnityEngine;

public class ArchonAI : EnemyAIBase, IEnemyAI
{
    public override void ChangeEXPValue() => values.ChangeEXPValue(10f);
}
