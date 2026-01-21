using UnityEngine;
using UnityEngine.InputSystem;

public class Tetramino : MonoBehaviour
{

    public float move_rate = 1;
    float timer = 0;

    // values for the grid - world coordinates
    private float left_edge = -2.5f;
    private float right_edge = 2.5f;
    private float bottom_edge = -5f;

    // each from the origin of the tetramino, in world coordinates
    private float left_offset;
    private float right_offset;
    private float bottom_offset;

    private bool active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // you can set the boundaries here for each when they are created - these are for the square tetramino
        // use the block coordinates
        computeOffsets();
    }

    // Update is called once per frame
    void Update()
    {
        moveDown();

        if (active)
            moveOnInput();
    }

    void moveDown()
    {
        // move down 
        if (timer < move_rate)
        {
            timer += Time.deltaTime;
        }
        else if (transform.position.y + bottom_offset > bottom_edge)    // blocks cannot currently stack
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            timer = 0;
        }
        else
        {
            enabled = false;   // stops update from running
            // probably going to want to decompose the square into blocks
        }
    }

    void moveOnInput()
    {
        // move left on input
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame && transform.position.x + left_offset > left_edge)
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        }


        // move right on input
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame && transform.position.x + right_offset < right_edge)
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }

        // move down faster
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            move_rate = 0.1f;
            active = false;
        }

        // rotate on input
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)    // and the rotate offset is in bounds
        {
            transform.Rotate(0f, 0f, 90f);
            computeOffsets();
        }
    }

    void computeOffsets()
    {
        left_offset = float.MaxValue;
        right_offset = float.MinValue;
        bottom_offset = float.MaxValue;

        foreach (Transform child in transform)
        {
            Vector3 localPos = child.localPosition;

            left_offset = Mathf.Min(left_offset, localPos.x);
            right_offset = Mathf.Max(right_offset, localPos.x);
            bottom_offset = Mathf.Min(bottom_offset, localPos.y);
        }
    }
}
