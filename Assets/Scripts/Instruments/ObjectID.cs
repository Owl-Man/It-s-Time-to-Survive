using UnityEngine;

namespace Instruments
{
    public class ObjectID : MonoBehaviour
    {
        public int id;


        private void Awake()
        {
            id = UnityEngine.Random.Range(0, 100000000);
        }
    }
}