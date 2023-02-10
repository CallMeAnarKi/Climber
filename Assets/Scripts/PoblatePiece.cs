using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoblatePiece : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private RectTransform pieceRectTransform;
    
    private GameObject previousBarrier;
    private RectTransform barrierRT;
    private float minRandom;
    private float maxRandom;
    private float randomY;
    private float randomX;





    // Start is called before the first frame update
    void Start()
    {
        barrierRT = (RectTransform)barrier.transform;

        if (previousBarrier == null)
        {
            previousBarrier = Instantiate(barrier, new Vector2(0, pieceRectTransform.rect.yMin + barrierRT.rect.yMax), Quaternion.identity);
        }

        SetDifficulty();
        StartCoroutine(SpawnBarrier(minRandom, maxRandom));


    }

    private IEnumerator SpawnBarrier(float min, float max)
    {
        while (previousBarrier.transform.position.y < pieceRectTransform.rect.yMax)
        {
            randomY = Random.Range(min, max);
            randomX = Random.Range(barrierRT.rect.xMax, max);


            previousBarrier = Instantiate(barrier, 
                                        new Vector2(
                                            pieceRectTransform.rect.xMin - randomX, //hacer k las barreras se posicionen a diferentes X 
                                            previousBarrier.transform.position.y + randomY + barrierRT.rect.yMax), 
                                        Quaternion.identity);



            yield return new WaitForSeconds(0);
        }

        yield return null;
    }

    private void SetDifficulty()
    {
        minRandom = GameManager.GenerateDifficulty().Item1;
        maxRandom = GameManager.GenerateDifficulty().Item2;
    }





}
