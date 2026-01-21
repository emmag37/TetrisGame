using UnityEngine;
using UnityEngine.InputSystem;

public class Tetramino : MonoBehaviour
{
    public float move_rate = 1;
    float timer = 0;

    // values for the grid - world coordinates
        // will eventually be a grid object
    private float left_edge = -2.5f;
    private float right_edge = 2.5f;
    private float bottom_edge = -5f;

    // each from the origin of the tetramino, in world coordinates
        // eventually switch to grid?
    private float left_offset;
    private float right_offset;
    private float bottom_offset;

    private float scale;

    private bool active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scale = transform.localScale.x;
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
        else if (transform.position.y + bottom_offset > bottom_edge)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - scale, transform.position.z);
            timer = 0;
        }
        else
        {
            active = false;
            enabled = false;   // stops update from running
            // probably going to want to decompose the square into blocks
        }
    }

    void moveOnInput()
    {
        // move left on input
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame && transform.position.x + left_offset > left_edge)
        {
            transform.position = new Vector3(transform.position.x - scale, transform.position.y, transform.position.z);
        }


        // move right on input
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame && transform.position.x + right_offset < right_edge)
        {
            transform.position = new Vector3(transform.position.x + scale, transform.position.y, transform.position.z);
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

            left_offset = Mathf.Min(left_offset, localPos.x) * scale * 2;
            right_offset = Mathf.Max(right_offset, localPos.x) * scale * 2;
            bottom_offset = Mathf.Min(bottom_offset, localPos.y) * scale * 2;
        }
    }
}
