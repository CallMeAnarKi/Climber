using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    private static GameObject player;
    [SerializeField] private PlayerInput _playerInput;

    static private int score = 0;


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



    }



    private void Start()
    {
        //Move this function to scene management
        //CreateLevel();
    }

    //Call this function when Scene management implemented
    /*
    private void CreateLevel()
    {


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
    }*/

    //----------- UI action map -----------//
    public void OnTouchUI(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            SceneManager.LoadScene(1);
            _playerInput.actions.FindActionMap("UI").Disable();
            _playerInput.actions.FindActionMap("Play").Enable();
        }
    }


    //----------- Play action map -----------//
    public void OnTouchPlay(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            player.GetComponent<Player>().PlayerMove(Mouse.current.position.ReadValue());
        }
    }


    static public (float, float, float) GenerateDifficulty()
    {
        if (playerScore < 500)
        {
            levelDifficulty = Difficulty.VeryEasy;
        }
        else if( playerScore >= 500 && playerScore < 1000)
        {
            levelDifficulty = Difficulty.Easy;
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
    
    static public void IncreaseScore()
    {
        score += 100;
        //Debug.Log(score);

        if (score >= 500)
        {
            ObstacleGenerator.SetFirstRock();
        }
    }



    static public GameObject GetPlayer()
    {
        return player;
    }    
    static public void SetPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }



}
