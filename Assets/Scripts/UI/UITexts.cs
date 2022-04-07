using Farmer;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UITexts:MonoBehaviour
    {
        public Text BombScore;
        public Text CarrotScore;
        
        private void OnEnable()
        {
            FarmerCollision.OnEventDirtyFarmer += SetBombScore;
            PlayerCollision.OnEventCarrotHarvested += SetCarrotScore;
        }

        private void OnDisable()
        {
            FarmerCollision.OnEventDirtyFarmer -= SetBombScore;
            PlayerCollision.OnEventCarrotHarvested -= SetCarrotScore;
        }
        
        private void SetCarrotScore(int score) => CarrotScore.text = score.ToString();
        
        private void SetBombScore(int score) => BombScore.text = score.ToString();
    }
}
