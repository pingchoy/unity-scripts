using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    //public bool isColliding;
    public Vector3 debugDirection;
    public LayerMask obstacleMask;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 15;
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");



        Vector2 direction = new Vector2(xInput, yInput).normalized;
        debugDirection = direction;

        bool isColliding = checkIsColliding(direction);
        if (isColliding)
        {
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.linearVelocity = direction * moveSpeed;
        }
        //body.linearVelocity = direction * moveSpeed;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.collider.name);
    }

    bool checkIsColliding(Vector3 direction)

    {
        bool isColliding = Physics2D.OverlapAreaAll(boxCollider.bounds.min + direction*0.5f, boxCollider.bounds.max + direction*0.5df, obstacleMask).Length > 0;
        Debug.Log(isColliding);
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
