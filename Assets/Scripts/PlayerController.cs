using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    public bool isGrounded;
    public int JumpHeight;


    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * JumpHeight);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14) // Important: Make sure to change the number to however many PickUp GameObjects you actually have in your game.
        {
            winTextObject.SetActive(true);
        }
    }
}
