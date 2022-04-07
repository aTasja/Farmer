using UnityEngine;

namespace Player
{
    public class PlayerCollision: MonoBehaviour
    {
        public delegate void CarrotHarvestedAction(int score);
        public static event CarrotHarvestedAction OnEventCarrotHarvested;
        
        private int _carrotScore;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Carrot")) {
                _carrotScore++;
                OnEventCarrotHarvested?.Invoke(_carrotScore);
                Destroy(col.gameObject);
            }
        }
    }
}
