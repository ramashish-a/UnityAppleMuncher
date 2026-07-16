using UnityEngine;

public class Food : MonoBehaviour
{
    // making a public variable for the grid area of the game
    public BoxCollider2D gridArea;

    // built-in function by Unity
    private void Start()
    {
        RandomizePosition();
    }

    // generating random number for our game
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    // function for managing collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
