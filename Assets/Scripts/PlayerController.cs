using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    Vector2 movementInput;

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer spriterenderer;

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public bool canMove = true;

    public SwordAttack swordAttack;

    public ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    // TODO: ERIK
  //   void Update()
  //   {
		// if (IsServer)
  //       {

  //       }

  //       if (IsClient && IsOwner)
  //       {
  //           UpdateClient();
  //       }
  //   }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // If movement is not 0, try move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success)
                    {
                        success = TryMove(new Vector2(movementInput.y, 0));
                    }
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // Set dirrection for movement animation
            if (movementInput.x < 0)
            {
                spriterenderer.flipX = true;
            } 
            else if (movementInput.x > 0)
            {
                spriterenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Next if statement remove running animation when player is next to a wall
        // but for some reason add bug in up and down movement, while left / right movement works just fine

        //if (direction != Vector2.zero)
        //{
            // Collision check
            int count = rb.Cast(
                direction, // X and Y values from -1 to 1
                movementFilter, // Determine where a collision can occur
                castCollisions, // Store found collisions
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // Amount to cast

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
       //}
       // else
       // {
       //    return false;
       //}
    }

    void OnMove(InputValue movementValue) 
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriterenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    	// TODO: ERIK - REFACTOR 
    // private void UpdateServer()
    // {
    //     transform.position = new Vector3(transform.position.x+networkPositionDirection.Value.x,
    //         transform.position.y+networkPositionDirection.Value.y, 0.0f);
    // }

    // private void UpdateClient()
    // {
    //     float xDirection = Input.GetAxis("Horizontal");
    //     float yDirection = Input.GetAxis("Vertical");

    //     var moveDirection = new Vector3(xDirection, yDirection, 0.0f);

    //     UpdateClientPositionServerRpc(moveDirection * speed);
    // }

    // [ServerRpc]
    // public void UpdateClientPositionServerRpc(Vector3 newPosition)
    // {
    //     networkPositionDirection.Value = newPosition;
    // }
}
