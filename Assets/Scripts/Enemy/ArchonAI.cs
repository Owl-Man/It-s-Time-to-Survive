namespace EnemySystem
{
    public sealed class ArchonAI : MagicanAIBase, IBossEnemy
    {
        public BossFightSystem bossFight;

        public override void BeforeDie() 
        {
            Magic.SetActive(false);
            if (bossFight != null) bossFight.Victory();
        }
    }
}
