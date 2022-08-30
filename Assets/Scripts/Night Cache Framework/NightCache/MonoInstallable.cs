using UnityEngine;

namespace NTC.Global.System
{
    public abstract class MonoInstallable : MonoBehaviour
    {
        private bool installedOnEnable;
        
        private void OnEnable()
        {
            OnPreEnable();
            if (!installedOnEnable)
            {
                OnFirstEnable();
                installedOnEnable = true;
            }
            OnLateEnable();
        }
        
        protected virtual void OnPreEnable() { }
        protected virtual void OnLateEnable() { }
        protected abstract void OnFirstEnable();
    }
}