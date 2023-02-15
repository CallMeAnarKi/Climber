using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoblatePiece : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private RectTransform pieceRT;

    private RectTransform playerRef;
    
    private GameObject previousBarrier;
    private RectTransform barrierRT;
    private float widthBetweenBarriers;
    private float minRandomY;
    private float maxRandomY;
    private float randomY;
    private float randomX;
    private bool lastBarrierPlaced;


    // Start is called before the first frame update
    void Awake()
    {
        barrierRT = (RectTransform)barrier.transform;
        playerRef = GameManager.GetPlayer().GetComponent<RectTransform>();

        
        //StartCoroutine(SpawnBarrier(widthBetweenBarriers, minRandomY,maxRandomY));
    }

    private void SpawnBarrier(Transform pieceObjective,float widthBetweenBarriers, float minY, float maxY)
    {
        while (previousBarrier.transform.position.y < pieceObjective.position.y + pieceRT.rect.yMax && !lastBarrierPlaced)
        {
            randomX = Random.Range(pieceRT.rect.xMin - barrierRT.rect.xMax, pieceRT.rect.xMax - barrierRT.rect.xMax - (playerRef.rect.width * widthBetweenBarriers));
            randomY = Random.Range(minY, maxY);

            for (int i = 0; i < GameManager.barrierArray.Length; i++)
            {
                if (!GameManager.barrierArray[i].activeSelf)
                {                    
                    GameManager.barrierArray[i].transform.position = new Vector2(randomX, 
                                                                                 previousBarrier.transform.position.y + randomY);
                    previousBarrier = GameManager.barrierArray[i];
                    previousBarrier.SetActive(true);

                    GameManager.barrierArray[i + 1].transform.position = new Vector2(previousBarrier.transform.position.x + barrierRT.rect.width + (playerRef.rect.width * widthBetweenBarriers),
                                                                                     previousBarrier.transform.position.y);
                    GameManager.barrierArray[i + 1].SetActive(true);

                    //if the last pair of barriers are upper than max.y of the piece, they are deactivated
                    if (previousBarrier.transform.position.y > pieceObjective.position.y + pieceRT.rect.yMax)
                    {
                        previousBarrier.SetActive(false);
                        GameManager.barrierArray[i + 1].SetActive(false);
                        lastBarrierPlaced = !lastBarrierPlaced;
                    }
                    break;
                }
            }       
        }

        return ;
    }

    private void SetDifficulty()
    {
        widthBetweenBarriers = GameManager.GenerateDifficulty().Item1;
        minRandomY = GameManager.GenerateDifficulty().Item2;
        maxRandomY = GameManager.GenerateDifficulty().Item3;  
    }

    public void SetBarriers(Transform pieceObjective)
    {
        if (previousBarrier == null)
        {
            for (int i = 0; i < GameManager.barrierArray.Length; i++)
            {
                if (!GameManager.barrierArray[i].activeSelf)
                {
                    previousBarrier = GameManager.barrierArray[i];
                    //previousBarrier.transform.position = new Vector2(pieceRT.rect.center.x, pieceRT.rect.min.y + barrierRT.rect.max.y);
                    previousBarrier.transform.position = new Vector2(pieceObjective.position.x, pieceObjective.position.y - pieceRT.rect.yMax + barrierRT.rect.max.y);
                    previousBarrier.SetActive(true);
                    break;
                }
            }
        }
        SetDifficulty();
        SpawnBarrier(pieceObjective, widthBetweenBarriers, minRandomY, maxRandomY);
    }


}
