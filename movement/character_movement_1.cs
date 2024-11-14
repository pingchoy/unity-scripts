using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    private Animator animator;
    private Coroutine idleCoroutine;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float veritcalInput = Input.GetAxisRaw("Vertical");

        Vector2 newMovement = new Vector2(horizontalInput, veritcalInput);


        if (newMovement != Vector2.zero)
        {
            Debug.Log("This is newMovement.x:" + newMovement.x);
            Debug.Log("This is newMovement.y:" + newMovement.y);
            if (newMovement.x != 0) newMovement.y = 0;


            animator.CrossFade("Walk", 0.1f);
            animator.SetBool("isMoving", true);
            StopAllCoroutines();
               
            animator.SetFloat("moveX", newMovement.x);
            animator.SetFloat("moveY", newMovement.y);
            // If moving left, then we need to flip the sprite
            if (newMovement.x == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (newMovement.x == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            var targetPos = transform.position;
            targetPos.x += newMovement.x;
            targetPos.y += newMovement.y;

            StartCoroutine(Move(targetPos));

            input = newMovement;


        }
        else
        {
            animator.CrossFade("Idle", 1);
            animator.SetBool("isMoving", false);
        }

    }


    IEnumerator Move(Vector3 targetPos)
    {
  
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
    
    }
}
