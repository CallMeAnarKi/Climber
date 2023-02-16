using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoblatePiece : MonoBehaviour
{
    static public PoblatePiece _intance;


     
    //barrier 
    [SerializeField] private GameObject barrierGO;
    static private GameObject[] barrierArray;
    [SerializeField] private GameObject barrierParent;
    static private RectTransform pieceRT;

    //piece
    [SerializeField] private GameObject pieceGO;
    private GameObject[] pieceArray;
    [SerializeField] private GameObject pieceParent;
    private GameObject lastPieceSet;


    static private RectTransform playerRef;
    
    static private GameObject previousBarrier;
    static private RectTransform barrierRT;
    static private float widthBetweenBarriers;
    static private float minRandomY;
    static private float maxRandomY;
    static private float randomY;
    static private float randomX;
    static private bool lastBarrierPlaced;


    // Start is called before the first frame update
    void Awake()
    {
        if (_intance == null)
        {
            _intance = this;
        }

        GameManager.SetPlayer();

        barrierArray = new GameObject[40];
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

        barrierRT = (RectTransform)barrierGO.transform;
        playerRef = GameManager.GetPlayer().GetComponent<RectTransform>();
        pieceRT = pieceArray[0].GetComponentInChildren<RectTransform>();

    }
    private void Start()
    {
        SetDifficulty();
        StartLvl();
    }

    private void StartLvl()
    {
        for (int i = 0; i < pieceArray.Length - 1; i++)
        {
            if (!pieceArray[i].activeSelf)
            {
                if (lastPieceSet == null)
                {
                    lastPieceSet = pieceArray[i];
                    pieceArray[i].transform.position = new Vector2(0, 0);
                }
                else
                {
                    pieceArray[i].transform.position = new Vector2(0, lastPieceSet.transform.position.y + lastPieceSet.GetComponentInChildren<RectTransform>().rect.height);
                }

                lastPieceSet = pieceArray[i];
                pieceArray[i].SetActive(true);
            }
        }

        RandomizeNextBarrier();

        //Set the first one barrier 
        if (previousBarrier == null)
        {
            for (int i = 0; i < barrierArray.Length; i++)
            {
                if (!barrierArray[i].activeSelf)
                {
                    previousBarrier = barrierArray[i];
                    previousBarrier.transform.position = new Vector2(randomX, playerRef.transform.position.y + randomY);
                    previousBarrier.SetActive(true);

                    barrierArray[i + 1].transform.position = new Vector2(previousBarrier.transform.position.x + barrierRT.rect.width + (playerRef.rect.width * widthBetweenBarriers),
                                                                         previousBarrier.transform.position.y);
                    barrierArray[i + 1].SetActive(true);                                        
                    break;
                }
            }
        }

        //Set few barriers ahead
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < barrierArray.Length; i++)
            {
                if (!barrierArray[i].activeSelf)
                {
                    RandomizeNextBarrier();
                    barrierArray[i].transform.position = new Vector2(randomX,
                                                                     previousBarrier.transform.position.y + randomY);
                    previousBarrier = barrierArray[i];
                    previousBarrier.SetActive(true);

                    barrierArray[i + 1].transform.position = new Vector2(previousBarrier.transform.position.x + barrierRT.rect.width + (playerRef.rect.width * widthBetweenBarriers),
                                                                         previousBarrier.transform.position.y);
                    barrierArray[i + 1].SetActive(true);

                    break;
                }
            }
        }
        
    }

    
    static public void SetNewBarrier(GameObject barrierObjective)
    {       

        barrierObjective.SetActive(false);

        //generate a new pair of barriers only if the barrierObjective is even
        if (System.Array.IndexOf(barrierArray, barrierObjective)%2 == 0)
        {
            for (int i = 0; i < barrierArray.Length; i++)
            {
                if (!barrierArray[i].activeSelf)
                {
                    RandomizeNextBarrier();
                    barrierArray[i].transform.position = new Vector2(randomX,
                                                                     previousBarrier.transform.position.y + randomY);
                    previousBarrier = barrierArray[i];
                    previousBarrier.SetActive(true);

                    barrierArray[i + 1].transform.position = new Vector2(previousBarrier.transform.position.x + barrierRT.rect.width + (playerRef.rect.width * widthBetweenBarriers),
                                                                         previousBarrier.transform.position.y);
                    barrierArray[i + 1].SetActive(true);

                    break;
                }
            }
        }
    }
    static private void RandomizeNextBarrier()
    {
        randomX = Random.Range(pieceRT.rect.xMin - barrierRT.rect.xMax, pieceRT.rect.xMax - barrierRT.rect.xMax - (playerRef.rect.width * widthBetweenBarriers));
        randomY = Random.Range(minRandomY, maxRandomY);
        return;
    }
    private void SetDifficulty()
    {
        widthBetweenBarriers = GameManager.GenerateDifficulty().Item1;
        minRandomY = GameManager.GenerateDifficulty().Item2;
        maxRandomY = GameManager.GenerateDifficulty().Item3;
    }

    static public void SetNewPiece()
    {
        //hacer k pille la pieza a mas altura y le concatene la siguiente
        //esto se llama desde el player
    }







}
