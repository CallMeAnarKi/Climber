using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] Transform obstacleGenerator;
    private GameObject[] obstacleArray;
    private float respawnTimer;
    private float timer = 0;

    private void Start()
    {
        obstacleArray = new GameObject[10];

        for (int i = 0; i < obstacleArray.Length; i++)
        {
            obstacleArray[i] = Instantiate(obstacle, obstacleGenerator);
            obstacleArray[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            respawnTimer = Random.Range(3f, 5f);
        }
        timer += Time.deltaTime;
        if (timer > respawnTimer)
        {
            foreach (GameObject child in obstacleArray)
            {
                if (!child.activeSelf)
                {
                    child.SetActive(true);
                    StartCoroutine(ObstacleMovement(child));
                    break;
                }
            }

            timer = 0;
        }
    }

    IEnumerator ObstacleMovement(GameObject obstacle)
    {
        var speed = Random.Range(1f, 2f);
        while (obstacle.activeSelf)
        {
            obstacle.transform.localPosition = new Vector2(obstacle.transform.localPosition.x,  obstacle.transform.localPosition.y - speed * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
        obstacle.transform.position = new Vector2(obstacleGenerator.position.x, obstacleGenerator.position.y);
        yield return null;
    }







}
