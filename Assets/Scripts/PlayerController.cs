using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 0f;
    float horizontalMove = 0f;
    float verticalMove = 0f;

    //bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalMove));

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, verticalMove, 0.0f);

        transform.position += moveDirection * speed;

        if (transform.position.y >= 50) // Upper Boundary
        {
            transform.position = new Vector3(transform.position.x, 50, 0);
        }
        else if (transform.position.y <= -48.8f) // Lower
        {
            transform.position = new Vector3(transform.position.x, -48.8f, 0);
        }

        if (transform.position.x >= 50) // Right
        {
            transform.position = new Vector3(50, transform.position.y, 0);
        }
        else if (transform.position.x <= -48.8f) // Left
        {
            transform.position = new Vector3(-48.8f, transform.position.y, 0);
        }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump = true;
        //}
    }
}
