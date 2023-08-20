using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Animator crossfade;
    public Animator swipe;
    public Animator death;
    public GameObject deathMask;
    public float transitionTime = 1f;
    public float waitTime = 0.5f;
    bool control;
    Vector2 moveDirection;
    Vector2 lastDirection;
    Vector2 target;
    Vector2 pos;
    bool moving = false;
    bool readyToMove = true;
    bool facingForward = true;
    bool paused = false;
    Scene scene;
    bool readyToWin = false;


    public AudioSource pushSound;
    public AudioSource moveSound;
    public AudioSource winSound;
    public AudioSource dieSound;


    private void Start()
    {
        moveDirection = Vector2.zero;
        lastDirection = moveDirection;
        control = true;
        scene = SceneManager.GetActiveScene();
        animator.SetBool("Die", false);
        animator.SetBool("Win", false);
    }

    private void Update()
    {
        if (!paused)
        {
            if (moveDirection == Vector2.zero && control && readyToMove)
            {
                moveDirection.x = Input.GetAxisRaw("Horizontal");
                if (moveDirection.x == 1 && !facingForward)
                {
                    facingForward = true;
                    transform.localScale = new Vector3(1, 1, 1);

                }
                else if (moveDirection.x == -1 && facingForward)
                {
                    facingForward = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                if (moveDirection.x == 0)
                {
                    moveDirection.y = Input.GetAxisRaw("Vertical");

                    //Debug.Log(animator.GetFloat("Vert"));
                }

                animator.SetFloat("Vert", moveDirection.y + 2);

                if (animator.GetBool("Push") && moveDirection != lastDirection)
                {
                    animator.SetBool("Push", false);
                }
                if(moveDirection == Vector2.zero)
                {
                    animator.SetBool("Move", false);
                }

            }
            if (Input.GetKeyDown("r"))
            {
                RestartLevel();
            }
        }

    }

    private void FixedUpdate()
    {
        //animator.SetBool("Move", false);
        //animator.SetBool("Push", false);
        pos = transform.position;
        if (!moving && moveDirection != Vector2.zero)
        {
            target = pos + moveDirection;

            RaycastHit2D raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero);

            if (raycastHit.collider == null)
            {
                //Empty space ahead - Move as usual
                moveSound.Play();
                moving = true;
                readyToMove = false;
            }
            else if (raycastHit.collider.tag == "LightDetector")
            {
                //Empty space ahead - Move as usual
                moveSound.Play();
                moving = true;
                readyToMove = false;
            }
            else if (raycastHit.collider.tag == "Wall")
            {
                //Wall ahead - don't move
                
                moveDirection = Vector2.zero;
                animator.SetBool("Move", false);
            }
            else if (raycastHit.collider.tag == "Pushable")
            {
                //Pushable ahead - move and interact with pushable
                if (raycastHit.collider.gameObject.GetComponent<PushableController>().OnPush(moveDirection))
                {
                    //Pushable ahead returned true - Move ahead !
                    pushSound.Play();

                    moving = true;
                    readyToMove = false;
                    animator.SetBool("Push", true);
                }
                else
                {
                    //Pushable ahead returned false - don't move
                    moveDirection = Vector2.zero;
                    animator.SetBool("Move", false);
                    animator.SetBool("Push", false);
                }
            }
        }

        if (moving)
        {
            animator.SetBool("Move", true);
            transform.position = new Vector3(transform.position.x + moveDirection.x / 8, transform.position.y + moveDirection.y / 8, transform.position.z);
            if ((Vector2)transform.position == target)
            {
                moving = false;
                lastDirection = moveDirection;
                moveDirection = Vector2.zero;
                StartCoroutine(WaitToMove());
            }
        }

    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.04f);
        readyToMove = true;
    }

    IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(1.2f);
        RestartLevel();
    }

    public void SetControl(bool control)
    {
        this.control = control;
    }

    public void PlayerDeath()
    {
        //deathMask.transform.position = transform.position;
        //Debug.Log(deathMask.transform.position);
        SetControl(false);
        moving = false;
        moveDirection = Vector2.zero;
        dieSound.Play();
        animator.SetBool("Die", true);
        death.SetTrigger("Death");
        gameObject.layer = 1;
        StartCoroutine(WaitToRestart());
        // Add animation ~ and anything else we are gonna do on player death here !!

    }

    public void PlayerWin()
    {
        if (control && !moving)
        {
            StartCoroutine(WaitToWin());
        }
        if (readyToWin && control && !moving)
        {
            winSound.Play();
            animator.SetBool("Win", true);
            SetControl(false);
            StartCoroutine(LevelUp(SceneManager.GetActiveScene().buildIndex + 1));
        }
        //Temporary
        //if (control) { Debug.Log("You Win!"); }
        //SetControl(false);
    }
    IEnumerator LevelUp(int levelIndex)
    {
        yield return new WaitForSeconds(waitTime);
        crossfade.SetTrigger("NextLevel");
        swipe.SetTrigger("NextLevel");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public bool IsMoving()
    {
        return moving;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPause(bool paused)
    {
        this.paused = paused;
    }

    IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(0.03f);
        //yield return new WaitForFixedUpdate();
        readyToWin = true;
    }

}
