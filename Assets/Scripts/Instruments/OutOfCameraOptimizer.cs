using System.Collections.Generic;
using UnityEngine;

namespace Instruments
{
    public class OutOfCameraOptimizer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objects;
        [SerializeField] private Animator[] animators;

        private void Start()
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].enabled = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == other)
                {
                    animators[i].enabled = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == other)
                {
                    animators[i].enabled = false;
                }
            }
        }
    }
}