public class ArchonAI : MagicanAIBase, IMagicanEnemyAI, IBossEnemy
{
    public BossFightSystem bossFight;

    private void Start() => Magic = LinkManager.instance.DarkMagicPlayer;

    public override void BeforeDie() 
    {
        Magic.SetActive(false);
        if (bossFight != null) bossFight.Victory();
    }
}
