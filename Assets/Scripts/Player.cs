using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Vector2 playerCurrentPos, touchedPos;
    private Rigidbody2D playerBody;
    [SerializeField] private BoxCollider2D playerBC;
    private bool isJumping;
    
    private Touch touch;

    [SerializeField] private LayerMask backgroundLayer;
    private Ray ray;

    private void Awake()
    {
        GameManager.SetPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    /*void Update()
    {
        
        if (Input.touchCount > 0)
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
        }


        
    }*/

    public void PlayerMove(Vector2 mousePos)
    {
        if (!isJumping)
        {
            Debug.DrawRay(transform.position, playerBody.velocity, Color.blue);

            touchedPos = Camera.main.ScreenToWorldPoint(mousePos);
            playerCurrentPos = transform.position;

            Debug.DrawLine(touchedPos, playerCurrentPos, Color.red, 5f);


            Vector2 vectorBetweenTouchAndPlayer = playerCurrentPos - touchedPos;
            playerBody.AddForce(vectorBetweenTouchAndPlayer.normalized * 100);
        }
    }

    public void PlayerJump()
    {
        isJumping = true;
        playerBC.enabled = !playerBC.enabled;
    }

    //ESTO SE LLAMARA DESDE LA NIMACION DE SALTO
    public void ReactivateCollider()
    {
        isJumping = false;
        playerBC.enabled = !playerBC.enabled;
    }



private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.IncreaseScore();       
        
        if (collision.transform.position.y < transform.position.y && collision.CompareTag("Obstacle"))
        {
            ObstacleGenerator.SetNewRock(collision.gameObject);
            return;
        }
        if (collision.transform.position.y < transform.position.y && collision.CompareTag("Piece"))
        {
            PoblatePiece.SetNewPiece(collision.transform.parent.gameObject);
            return;
        }
        if (collision.transform.position.y < transform.position.y && collision.CompareTag("Pad"))
        {
            ObstacleGenerator.SetNewPad(collision.gameObject);
            return;
        }

    }

}
