using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public Vector3 debugDirection;
    public LayerMask obstacleMask;
    public Animator animator;
    private Vector2 direction;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 10;
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        direction = new Vector2(xInput, yInput).normalized;
        

    }

    private void FixedUpdate()
    {
        debugDirection = direction;
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

        if (direction.sqrMagnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        bool isColliding = checkIsColliding();
        if (isColliding)
        {
            body.linearVelocity = Vector2.zero;
        }
        else
        {

            body.linearVelocity = direction * moveSpeed;
        }
    }


    bool checkIsColliding()

    {
        Vector3 v3Direction = direction;

        bool isColliding = Physics2D.OverlapAreaAll(boxCollider.bounds.min + v3Direction * 0.5f, boxCollider.bounds.max + v3Direction * 0.5f, obstacleMask).Length > 0;
        return isColliding;
    }

    void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            // Calculate the shifted corners based on the direction
            Vector2 pointA = boxCollider.bounds.min + debugDirection ;
            Vector2 pointB = boxCollider.bounds.max + debugDirection;

            // Set Gizmo color and draw a rectangle
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(pointA.x, pointA.y, 0), new Vector3(pointB.x, pointA.y, 0));
            Gizmos.DrawLine(new Vector3(pointA.x, pointA.y, 0), new Vector3(pointA.x, pointB.y, 0));
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y, 0), new Vector3(pointB.x, pointB.y, 0));
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y, 0), new Vector3(pointB.x, pointB.y, 0));
        }
    }


}
