using UnityEngine;
using System.Collections;

public class EvilWizardAI : MagicanAIBase, IEnemyAI, IMagicanEnemyAI
{
	private void Start() => Magic = links.FireBurningPlayer;

	public override void ChangeEXPValue() => values.ChangeEXPValue(25f);
}