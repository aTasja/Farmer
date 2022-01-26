using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _S;
    
    public Text BombScore;
    public Text CarrotScore;
    public Text GameOverText;
    public Text YouWonText;
    public Text WonScore;

    public int CarrotsNumber = 10;
    public GameObject CarrotPrefab;
    public Transform CarrotParent;
    
    public delegate void BombButtonAction();
    public static event BombButtonAction OnEventBomb;

    [SerializeField] private Tilemap obstacles;
    [SerializeField] private Tilemap ground;
    
    private int bombScoreInt = 0;
    private int carrotScoreInt = 0;

    // Start is called before the first frame update
    void Awake()
    {
        // Assign the Singleton as part of the constructor.
        if (_S == null)
            _S = this;
        else if (_S == this)
            Destroy(gameObject);
    }

    private void Start()
    {
        for(int i=0; i<CarrotsNumber; i++)
        {
            GameObject carrot = Instantiate(CarrotPrefab, CarrotParent);
            carrot.transform.position = GetRandomCarrotPosition();
        }

        GameOverText.gameObject.SetActive(false);
        YouWonText.gameObject.SetActive(false);
    }

    Vector3 GetRandomCarrotPosition()
    {
        Vector3 carrotPos = Vector3.zero;
        float x = Random.Range(-(ground.size.x-1) / 2f, (ground.size.x-1) / 2f);
        float y = Random.Range(-(ground.size.y-1) / 2f, (ground.size.y-1) / 2f);

        carrotPos =  new Vector3(x, y, 0);

        // geting tile position for carrot
        Vector3Int tileForCarrot = ground.WorldToCell(carrotPos);
        carrotPos = ground.GetCellCenterWorld(tileForCarrot);
        
        if (!IsCellClear(carrotPos))
            return GetRandomCarrotPosition();
        else 
            return carrotPos;
    }

    private void OnEnable()
    {
        Bomb.OnEventDirtyFarmer += SetBombScore;
        Carrot.OnEventCattorHarvested += SetCarrotScore;
    }

    private void OnDisable()
    {
        Bomb.OnEventDirtyFarmer -= SetBombScore;
        Carrot.OnEventCattorHarvested -= SetCarrotScore;
    }

    public static Tilemap GroundTilemap
    {
        get { return _S.ground; }
    }

    public static Tilemap ObstaclesTilemap
    {
        get { return _S.obstacles; }
    }

    public static bool IsCellClear(Vector3 pos)
    {
        Vector3Int obstacleMapTile = _S.obstacles.WorldToCell(pos);// - new Vector3(0, .5f, 0));
        return !_S.obstacles.HasTile(obstacleMapTile);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void BombButton()
    {
        if (OnEventBomb != null)
            OnEventBomb();
    }

    private void SetBombScore()
    {
        _S.bombScoreInt++;
        _S.BombScore.text = bombScoreInt.ToString();
    }

    private void SetCarrotScore()
    {
        _S.carrotScoreInt++;
        _S.CarrotScore.text = carrotScoreInt.ToString();
        if (carrotScoreInt == CarrotsNumber)
        {
            _S.YouWonText.gameObject.SetActive(true);
            WonScore.text = (carrotScoreInt * bombScoreInt).ToString();
            Time.timeScale = 0;
        }
    }

    public static void GAMEOVER()
    {
        _S.GameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
