using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Vector2 playerCurrentPos, touchedPos;
    private Rigidbody2D playerBody;

    private Touch touch;

    [SerializeField] private LayerMask backgroundLayer;
    private Ray ray;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchedPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    playerCurrentPos = transform.position;
                    Vector2 vectorBetweenTouchAndPlayer = playerCurrentPos - touchedPos;
                    playerBody.AddForce(vectorBetweenTouchAndPlayer.normalized*10 );
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }




            //detectar la pos del touch
            //hacer un vector desde touch hasta player (vector normalizado)
            //añadir la direccion del vector a un addForce al player 
        }*/

        Debug.DrawRay(transform.position, playerBody.velocity, Color.blue);
        if (Input.GetMouseButtonUp(0))
        {
            touchedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerCurrentPos = transform.position;

            Debug.DrawLine(touchedPos, playerCurrentPos, Color.red, 5f);



            Vector2 vectorBetweenTouchAndPlayer = playerCurrentPos - touchedPos;
            playerBody.AddForce(vectorBetweenTouchAndPlayer.normalized*100);
        }
        
    }



}
