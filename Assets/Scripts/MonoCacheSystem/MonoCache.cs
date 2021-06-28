using UnityEngine;
using System.Collections.Generic;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allUpdate = new List<MonoCache>(10001);

    private void OnEnable() => allUpdate.Add(this);

    private void OnDisable() => allUpdate.Remove(this);

    private void OnDestroy() => allUpdate.Remove(this);

    public void Tick() => OnTick();

    public virtual void OnTick() { } // Update changer
}