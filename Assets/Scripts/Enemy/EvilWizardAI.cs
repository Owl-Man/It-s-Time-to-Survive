using UnityEngine;
using System.Collections;

public class EvilWizardAI : MagicanAIBase, IMagicanEnemyAI
{
	private void Start() => Magic = links.FireBurningPlayer;
}