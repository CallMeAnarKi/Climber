using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{

    public enum Difficulty{ VeryEasy, Easy, Normal, Hard, VeryHard};
    private static Difficulty levelDifficulty;
    private static int playerScore = 0;
    private static float minRandom;
    private static float maxRandom;

    static private void Awake()
    {
        
    }

    static public (float, float) GenerateDifficulty()
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
                minRandom = 1;
                maxRandom = 3;
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

        return (minRandom, maxRandom);
    }
}
