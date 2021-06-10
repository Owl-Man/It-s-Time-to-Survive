using UnityEngine;
using System.Collections;

public class EvilWizardAI : EnemyAIBase
{
	private GameObject Fire;

	private void Start() => Fire = links.FireBurningPlayer;

	public override void AttackPlayer() 
	{
		base.AttackPlayer();
		Fire.SetActive(true);
	}
}