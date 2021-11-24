public sealed class EvilWizardAI : MagicanAIBase
{
	private void Start() => Magic = LinkManager.instance.FireBurningPlayer;
}