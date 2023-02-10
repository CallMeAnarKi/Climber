using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGO : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            collision.gameObject.SetActive(false);
        }
    }


}
