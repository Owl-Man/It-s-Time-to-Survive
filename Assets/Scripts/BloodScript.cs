using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    public GameObject[] Bloods;

    public void InstantiateBlood(Transform transform) 
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        Instantiate(Bloods[Random.Range(0, Bloods.Length)], pos, Quaternion.identity);
    }
}
