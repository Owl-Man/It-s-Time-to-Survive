using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[SelectionBase]
public class RepeatedObject : MonoBehaviour
{
    public Vector3 Offset = Vector3.forward; 

    [Min(1)] public int Count = 1;

    private void Start()
    {
        if (Application.IsPlaying(this))
        {
            CorrectChildCount();
            Destroy(this);
        }
    }

    private void Update()
    {
        CorrectChildCount();
    }

    private void CorrectChildCount()
    {
        Count = Mathf.Clamp(Count, 1, 1000);
        
        if (transform.childCount < Count)
        {
            for (int i = transform.childCount; i < Count; i++)
            {
                Transform instantiate = Instantiate(transform.GetChild(0), transform);
                instantiate.Translate(Offset * i);
            }
        }
        else if (Count < transform.childCount)
        {
            for (int i = transform.childCount-1; i >= Count; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}