using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // variable to keep track of our direction
    private Vector2 _direction = Vector2.right;

    // making a list to keep track of all the segments of our AppleMuncher
    private List<Transform> _segments = new List<Transform>();

    // 4: ze prefab!
    public Transform segmentPrefab;

    // making the snake to have an initial size
    public int initialSize = 4;

    // start function
    private void Start()
    {
        ResetState();
    }

    // function for controlling the AppleMuncher
    private void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2.up;
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2.down;
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2.left;
        }
        else if(Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2.right;
        }
    }

    // this implements grid-based movement
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    // function called grow for copying the prefab
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        if (_segments.Count > 0)
        {
            segment.position = _segments[_segments.Count - 1].position;
        }

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int j = 1; j < this.initialSize; j++)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[j - 1].position;
            _segments.Add(segment);
        }
        this.transform.position = Vector3.zero;
    }

    // function for managing collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
