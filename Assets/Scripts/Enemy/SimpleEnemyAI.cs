using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : EnemyAIBase, IEnemyAI
{
	public float GiveEXP;
	
	public override void ChangeEXPValue() => values.ChangeEXPValue(GiveEXP);
}