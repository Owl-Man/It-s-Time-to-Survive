using UnityEngine;

namespace NTC.Global.Cache
{
    public sealed class NightCacheEntry : MonoBehaviour
    {
        private void Update()
        {
            NightCacheCore.Run();
        }

        private void FixedUpdate()
        {
            NightCacheCore.FixedRun();
        }

        private void LateUpdate()
        {
            NightCacheCore.LateRun();
        }
        
        private void OnDestroy()
        {
            NightCacheCore.Reset();
        }
    }
}