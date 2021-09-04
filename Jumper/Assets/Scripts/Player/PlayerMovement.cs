using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D controller;
    public float runSpeed = 30f;

    float x;
    bool canJump = false;
    Animator anim;
    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(startMovement());

        EventHandler.curr.onPlayerInteract += EnterDoor;
    }

    private void OnDestroy()
    {
        EventHandler.curr.onPlayerInteract -= EnterDoor;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) { 
            x = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                canJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(x * Time.fixedDeltaTime, canJump);
        canJump = false;

        anim.SetFloat("Running", Mathf.Abs(x));
        anim.SetBool("Jumping", !controller.isGrounded());

    }

    IEnumerator startMovement()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    private void EnterDoor()
    {
        canMove = false;
        StartCoroutine(DisableCharacterSprite());
    }

    IEnumerator DisableCharacterSprite()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("EnterDoor");
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
}
