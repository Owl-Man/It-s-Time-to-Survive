using Instruments;

namespace EnemySystem
{
	public sealed class EvilWizardAI : MagicanAIBase
	{
		private void Start() => Magic = LinkManager.Instance.fireBurningPlayer;
	}
}