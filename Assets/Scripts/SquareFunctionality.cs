using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFunctionality : MonoBehaviour
{
    private void Start()
    {
        // Set Box Collider to be the size of the Square.
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
    }

    private void OnMouseDown()
    {
        // If the square is clicked, destroy it.
        GameManager.squareWasClicked = true;
        GameManager.gameScore++;
    }
}
