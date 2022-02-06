using UnityEngine;

namespace Instruments
{
    public class OutOfCameraOptimizer : MonoBehaviour
    {
        [SerializeField] private ObjectID[] objectsIds;
        [SerializeField] private Animator[] animators;

        private void OnTriggerEnter2D(Collider2D other)
        {
            for (int i = 0; i < objectsIds.Length; i++)
            {
                if (other.gameObject.GetComponent<ObjectID>().id == objectsIds[i].id)
                {
                    animators[i].enabled = true;
                    print("yeah");
                    return;
                }
                else
                {
                    animators[i].enabled = false;
                }
            }
        }
    }
}