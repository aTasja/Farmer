using UI;
using UnityEngine;

namespace Player
{
    public class BombPlanting: MonoBehaviour
    {
        public GameObject BombPrefab;
        public Transform BombParent;
        
        private void OnEnable()
        {
            UIButtons.OnEventBomb += SetBomb;
        }

        private void OnDisable()
        {
            UIButtons.OnEventBomb -= SetBomb;
        }
        
        private void SetBomb()
        {
            var bomb =  Instantiate(BombPrefab, BombParent);
            bomb.transform.position = transform.position;
        }
    }
}
