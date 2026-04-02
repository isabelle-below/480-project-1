using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int jumpCount;

    public float speed = 0;
    public float jumpHeight = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        //get rigid body from player
        rb = GetComponent<Rigidbody>();
        count = 0;
        // set count text
        SetCountText();
        //hide win message
        winTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue)
    {
        // when movement input, get input and store as x and y input
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump ()
    {
        // if not moving up or down (if on surface)
        if (rb.linearVelocity.y == 0)
        {
            // set jump count to zero and apply force vertically
            jumpCount = 0;
            rb.AddForce(Vector3.up * jumpHeight);
        }
        // if moving vert and jump count less than one
        else if (jumpCount < 1)
        {
            // increase jump count and apply force vertically
            jumpCount++;
            rb.AddForce(Vector3.up * jumpHeight);
        }
    }

    void SetCountText()
    {
        // update count based on number of objects picked up
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        // create vector of x left right, 0 up down, y forward back
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        // apply vector force multiplied by speed var
        rb.AddForce(movement * speed);
    }

    // pick up objects and increase score
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

}
