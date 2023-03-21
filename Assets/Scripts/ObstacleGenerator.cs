using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public static ObstacleGenerator _instance;


    [SerializeField] private Transform spawnerParent;
    static private Transform spawner1;
    static private Transform spawner2;
    static private Transform spawner3;
    [SerializeField] private Transform camTransform;
    static private GameObject playerRef;

    [SerializeField] private GameObject padGO;
    static private GameObject[] padArray;
    [SerializeField] private GameObject padParent;
    static private RectTransform padRT;

    [SerializeField] private GameObject rockGO;
    static private GameObject[] rockArray;
    [SerializeField] private GameObject rockParent;
    static private RectTransform rockRT;



    static private GameObject previousPad;
    static private GameObject previousRock;
    static private int lastPathPos;
    static private int repeatedPathCount;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        spawner1 = spawnerParent.GetChild(0).GetComponent<Transform>();
        spawner2 = spawnerParent.GetChild(1).GetComponent<Transform>();
        spawner3 = spawnerParent.GetChild(2).GetComponent<Transform>();


        padArray = new GameObject[5];
        for (int i = 0; i < padArray.Length; i++)
        {
            padArray[i] = Instantiate(padGO, new Vector2(2000, 2000), Quaternion.identity, padParent.transform);
            padArray[i].SetActive(false);
        }

        rockArray = new GameObject[5];
        for (int i = 0; i < rockArray.Length; i++)
        {
            rockArray[i] = Instantiate(rockGO, new Vector2(2000, 2000), Quaternion.identity, rockParent.transform);
            rockArray[i].SetActive(false);
        }

        playerRef = GameManager.GetPlayer();
        repeatedPathCount = 0;
    }

    private void Start()
    {
        padRT = padGO.GetComponent<RectTransform>();
        rockRT = rockGO.GetComponent<RectTransform>();

        if (previousPad == null)
        {
            previousPad = padArray[0];
            previousPad.transform.position = new Vector2(PathPos(), playerRef.transform.position.y + 7);
            previousPad.SetActive(true);
            
            padArray[1].transform.position = new Vector2(PathPos(), previousPad.transform.position.y + padRT.rect.yMax * 2+ playerRef.GetComponent<RectTransform>().rect.height + 1);
            padArray[1].SetActive(true);
            previousPad = padArray[1];

            padArray[2].transform.position = new Vector2(PathPos(), previousPad.transform.position.y + padRT.rect.yMax * 2+ playerRef.GetComponent<RectTransform>().rect.height + 1);
            padArray[2].SetActive(true);
            previousPad = padArray[2];
        }
    }

    static private float PathPos()
    {
        int pathSelected = SelectPath();
        lastPathPos = pathSelected;

        if (pathSelected == 0)
        {
            return spawner1.position.x;            
        }
        else if (pathSelected == 1)
        {
            return spawner2.position.x;
        }
        else if (pathSelected == 2)
        {
            return spawner3.position.x;
        }
        return 0;
    }

    static private int SelectPath()
    {
        int path = Random.Range(0, 3);        

        if (path != lastPathPos)
        {
            repeatedPathCount = 0;
        }
        else
        {
            lastPathPos = path;
            repeatedPathCount++;
        }

        while (path == lastPathPos && repeatedPathCount >= 2)
        {
            path = Random.Range(0, 3);
        }
        return path;
    }

    static public void SetNewPad(GameObject padObjective)
    {
        padObjective.SetActive(false);

        for (int i = 0; i < padArray.Length; i++)
        {
            if (!padArray[i].activeSelf)
            {
                padArray[i].transform.position = new Vector2(PathPos(), previousPad.transform.position.y + padRT.rect.yMax * 2 + playerRef.GetComponent<RectTransform>().rect.height + 1);
                padArray[i].SetActive(true);
                previousPad = padArray[i];
                break;
            }
        }
    }

    static public void SetFirstRock()
    {
        if (previousRock == null)
        {
            previousRock = rockArray[0];
            previousRock.transform.position = new Vector2(PathPos(), playerRef.transform.position.y + 7);
            previousRock.SetActive(true);
        }
    }
    static public void SetNewRock(GameObject rockObjective)
    {
        rockObjective.SetActive(false);

        for (int i = 0; i < rockArray.Length; i++)
        {
            if (!rockArray[i].activeSelf)
            {
                rockArray[i].transform.position = new Vector2(PathPos(), playerRef.transform.position.y + 7);
                rockArray[i].SetActive(true);
                previousRock = rockArray[i];
                break;
            }
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        spawnerParent.position = new Vector2(spawnerParent.position.x, spawnerParent.position.y + camTransform.transform.position.y);
    }*/










}
