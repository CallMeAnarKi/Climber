using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    private static GameObject player;

    public enum Difficulty{ VeryEasy, Easy, Normal, Hard, VeryHard};
    private static Difficulty levelDifficulty;
    private static int playerScore = 0;
    private static float minRandomY;
    private static float maxRandomY;
    private static float minRandomX;
    private static float maxRandomX;


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

    static public (float, float, float, float) GenerateDifficulty()
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

                minRandomX = 1;
                maxRandomX = 3;

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

        return (minRandomX, maxRandomX, minRandomY, maxRandomY);
    }
    
    static public GameObject GetPlayer()
    {
        return player;
    }



}
