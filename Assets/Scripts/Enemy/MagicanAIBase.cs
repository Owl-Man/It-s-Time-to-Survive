using Instruments;
using UnityEngine;

namespace EnemySystem
{
    public abstract class MagicanAIBase : EnemyAIBase
    {
        [HideInInspector] public GameObject Magic;
    
        private void Start() => Magic = LinkManager.Instance.darkMagicPlayer;

        public override void AttackPlayer() 
        {
            base.AttackPlayer();
            Magic.SetActive(true);
        }

        public override void BeforeDie() => Magic.SetActive(false);
    }
}
