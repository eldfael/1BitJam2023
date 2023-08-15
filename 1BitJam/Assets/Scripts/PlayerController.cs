using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool control;
    Vector2 moveDirection;
    Vector2 target;
    Vector2 pos;
    bool moving = false;
    Scene scene;

    private void Start()
    {
        moveDirection = Vector2.zero;
        control = true;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (moveDirection == Vector2.zero && control)
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            if (moveDirection.x == 0)
            {
                moveDirection.y = Input.GetAxisRaw("Vertical");
            }
        }
        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(scene.name);
        }
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        if (!moving && moveDirection != Vector2.zero)
        {
            target = pos + moveDirection;

            RaycastHit2D raycastHit = Physics2D.BoxCast(target, Vector2.one*0.5f, 0f, Vector2.zero);
            Debug.Log(raycastHit.collider);

            if (raycastHit.collider == null)
            {
                //Empty space ahead - Move as usual
                moving = true;
            }
            else if (raycastHit.collider.tag == "Wall")
            {
                //Wall ahead - don't move
                moveDirection = Vector2.zero;
            }
            else if (raycastHit.collider.tag == "Pushable")
            {
                //Pushable ahead - move and interact with pushable
                if (raycastHit.collider.gameObject.GetComponent<PushableController>().OnPush(moveDirection))
                {
                    //Pushable ahead returned true - Move ahead !
                    moving = true;
                }
                else
                {
                    //Pushable ahead returned false - don't move
                    moveDirection = Vector2.zero;
                }
            }
        }
        
        if (moving)
        {
            transform.position = new Vector3 (transform.position.x + moveDirection.x/8, transform.position.y + moveDirection.y/8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                moveDirection = Vector2.zero;
            }
        }

    }

    public void SetControl(bool control)
    {
        this.control = control;
    }

    public void PlayerDeath()
    {
        // Add animation ~ and anything else we are gonna do on player death here !!
        SetControl(false);
    }

    public void PlayerWin()
    {
        //Temporary
        if (control) { Debug.Log("You Win!"); }
        SetControl(false);
    }
}
