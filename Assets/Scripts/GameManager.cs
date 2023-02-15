using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    private static GameObject player;

    //barrier 
    [SerializeField] private GameObject barrierGO;
    public static GameObject[] barrierArray;
    [SerializeField] private GameObject barrierParent;

    //piece
    [SerializeField] private GameObject pieceGO;
    static public GameObject[] pieceArray;
    [SerializeField] private GameObject pieceParent;
    static private GameObject lastPieceSet;

    public enum Difficulty{ VeryEasy, Easy, Normal, Hard, VeryHard};
    private static Difficulty levelDifficulty;
    private static int playerScore = 0;
    private static float minRandomY;
    private static float maxRandomY;
    private static float widthBetweenBarriers;
    
    private void Awake()
    {
        if (_gameManager ==  null)
        {
            _gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }


    }

    private void Start()
    {
        //Move this function to scene management
        CreateLevel();
    }

    //Call this function when Scene management implemented
    private void CreateLevel()
    {
        barrierArray = new GameObject[50];
        for (int i = 0; i < barrierArray.Length; i++)
        {
            barrierArray[i] = Instantiate(barrierGO, new Vector2(1000, 1000), Quaternion.identity, barrierParent.transform);
            barrierArray[i].SetActive(false);
        }

        pieceArray = new GameObject[4];
        for (int i = 0; i < pieceArray.Length; i++)
        {
            pieceArray[i] = Instantiate(pieceGO, new Vector2(2000, 2000), Quaternion.identity, pieceParent.transform);
            pieceArray[i].SetActive(false);
        }

        for (int i = 0; i < pieceArray.Length-1; i++)
        {
            if (!pieceArray[i].activeSelf)
            {                
                if (i != 0)
                {
                    pieceArray[i].transform.position = new Vector2(0, lastPieceSet.transform.position.y + lastPieceSet.GetComponentInChildren<RectTransform>().rect.height);
                }
                else
                {
                    pieceArray[i].transform.position = new Vector2(0, 0);
                }

                lastPieceSet = pieceArray[i];

                pieceArray[i].SetActive(true);
                pieceArray[i].GetComponentInChildren<PoblatePiece>().SetBarriers(pieceArray[i].GetComponent<Transform>());                
            }
        }
    }

    static public void NextPiece()
    {

    }


    static public (float, float, float) GenerateDifficulty()
    {
        if (playerScore < 500)
        {
            levelDifficulty = Difficulty.VeryEasy;
        }
        else if( playerScore >= 500 && playerScore < 1000)
        {
            
        }

        switch (levelDifficulty)
        {
            case Difficulty.VeryEasy:

                //how many times is bigger the gap than the player between barriers 
                widthBetweenBarriers = 1.2f;

                minRandomY = 3;
                maxRandomY = 8;
                break;
            case Difficulty.Easy:
                break;
            case Difficulty.Normal:
                break;
            case Difficulty.Hard:
                break;
            case Difficulty.VeryHard:
                break;
            default:
                break;
        }

        return (widthBetweenBarriers, minRandomY, maxRandomY);
    }
    
    static public GameObject GetPlayer()
    {
        return player;
    }



}
