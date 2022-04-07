using UnityEngine;

namespace CarrotFabric
{
    public class Carrot : MonoBehaviour
    {
        private void Start()
        {
            var randomZrotation = Random.Range(0, 360f);
            transform.rotation = Quaternion.Euler(0, 0, randomZrotation);
        }
    }
}
