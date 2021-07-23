public class EvilWizardAI : MagicanAIBase, IMagicanEnemyAI
{
	private void Start() => Magic = LinkManager.instance.FireBurningPlayer;
}