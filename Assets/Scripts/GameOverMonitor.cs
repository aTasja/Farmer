using CarrotFabric;
using Farmer;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMonitor : MonoBehaviour
{
    public Text GameOverText;
    public Text YouWonText;
    public Text WonScore;

    private int _carrotScoreToWin;

    private void Start()
    {
        _carrotScoreToWin = CarrotsCreator.CarrotsNumber;
        GameOverText.gameObject.SetActive(false);
        YouWonText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        PlayerCollision.OnEventCarrotHarvested += CheckIfWon;
        FarmerCollision.OnEventPlayerCaughtByFarmer += GameOver;
    }
        
    private void OnDisable()
    {
        PlayerCollision.OnEventCarrotHarvested -= CheckIfWon;
        FarmerCollision.OnEventPlayerCaughtByFarmer -= GameOver;
    }
    
    private void CheckIfWon(int score)
    {
        if (score == _carrotScoreToWin) {
            YouWonText.gameObject.SetActive(true);
            if(FarmerCollision.BombScore > 0)
                WonScore.text = (_carrotScoreToWin * FarmerCollision.BombScore).ToString();
            else
                WonScore.text = _carrotScoreToWin.ToString();
            Time.timeScale = 0;
        }
    }

    private void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}