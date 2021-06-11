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

	public override void BeforeDie() => Fire.SetActive(false);
}