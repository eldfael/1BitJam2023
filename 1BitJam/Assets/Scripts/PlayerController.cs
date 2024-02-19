using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Animator crossfade;
    public Animator swipe;
    public Animator death;
    public Animator UiDeath;
    public GameObject deathMask;
    public float transitionTime = 1f;
    public float waitTime = 0.5f;
    public bool control;
    public Vector2 moveDirection;
    Vector2 lastDirection;
    Vector2 target;
    Vector2 pos;
    bool moving = false;
    public bool readyToMove = true;
    public bool facingForward = true;
    bool paused = false;
    Scene scene;
    bool readyToWin = false;

    bool readyToRestart = false;
    bool axemove = false;

    // UI buttons
    // Phasing these out of being used in playerController
    // UndoButton undoButton;
    // ArrowButton upArrow;
    // ArrowButton downArrow;
    // ArrowButton leftArrow;
    // ArrowButton rightArrow;


    Touch touch;
    Vector2 firstTouch;
    Vector2 lastTouch;
    Vector2 swipeDirection;
    float swipeDistance;
    bool touchHeld;
    bool setTouch;
    bool doAnim;

    bool readyToUndo;
    Stack undoStack;
    List<(Vector2, GameObject)> tupleList;

    public AudioSource pushSound;
    public AudioSource moveSound;
    public AudioSource winSound;
    public AudioSource dieSound;

    GameController gController;

    LayerMask lmask;
    ContactFilter2D filter;


    private void Start()
    {
        moveDirection = Vector2.zero;
        lastDirection = moveDirection;
        control = true;
        scene = SceneManager.GetActiveScene();
        animator.SetBool("Die", false);
        animator.SetBool("Win", false);
        swipeDistance = Screen.height * 18 / 100;
        undoStack = new Stack();

        StartCoroutine(WaitToRestart());

        gController = FindObjectOfType<GameController>();   

        lmask = LayerMask.GetMask("Default") + LayerMask.GetMask("TransparentFX") + LayerMask.GetMask("AxeBlock");
        filter.layerMask = lmask;

        touchHeld = false;
        setTouch = false;
        doAnim = false;
    }

    private void Update()
    {
        if (!paused)
        {
            //Check for Undo
            if (Input.GetKey("z"))
            {
                if (readyToMove)
                {
                    OnUndo();
                }
            }

            
            //Mobile inputs
            // NEW touch Inputs
            if (Input.touchCount == 1)
            {
                if (!setTouch)
                {
                    firstTouch = touch.position;
                    lastTouch = firstTouch;
                    setTouch = true;
                }
                //Player is touching screen
                touch = Input.GetTouch(0);
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        firstTouch = touch.position;
                        lastTouch = firstTouch;
                        break;
                    case TouchPhase.Moved:
                        lastTouch = touch.position;
                        break;
                    case TouchPhase.Stationary:
                        // catching this in case needs to be used 
                        break;
                    case TouchPhase.Ended:
                        lastTouch = touch.position;
                        touchHeld = false;
                        break;
                    
                }
            }
            else
            {
                //Player is not touching screen
                touchHeld = false;
                setTouch = false;
                firstTouch = Vector2.zero;
                lastTouch = Vector2.zero;
            }

            // Check if enough distance has been covered by swipe
            if((Mathf.Abs(lastTouch.x - firstTouch.x) > swipeDistance || Mathf.Abs(lastTouch.y - firstTouch.y) > swipeDistance) && readyToMove && control)                
            {
                // Check if swipe is bigger in X distance or Y distance                 
                if (Mathf.Abs(lastTouch.x - firstTouch.x) >= Mathf.Abs(lastTouch.y - firstTouch.y))
                {
                    //X is greater
                    //Check direction
                    if (lastTouch.x > firstTouch.x)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
                else
                {
                    //Y is greater
                    //Check direction
                    if (lastTouch.y > firstTouch.y)
                    {
                        MoveUp();
                    }
                    else
                    {
                        MoveDown();
                    }
                }
                //Set new "Original" touch position to the last touch position
                firstTouch = lastTouch;

                if (moveDirection == swipeDirection || Mathf.Abs(lastTouch.x - firstTouch.x) > swipeDistance*2 || Mathf.Abs(lastTouch.y - firstTouch.y) > swipeDistance*2)
                {
                    //Check to see if "Double Swipe"
                    touchHeld = true;
                }
                else
                {
                    //If not - only move once
                    touchHeld = false;
                }
                swipeDirection = moveDirection;

            }
            else if (touchHeld && readyToMove && control && Input.touchCount == 1)
            {
                switch(lastDirection)
                {
                    case Vector2 v when v.Equals(Vector2.right):
                        MoveRight();
                        break;
                    case Vector2 v when v.Equals(Vector2.left):
                        MoveLeft();
                        break;
                    case Vector2 v when v.Equals(Vector2.up):
                        MoveUp();
                        break;
                    case Vector2 v when v.Equals(Vector2.down):
                        MoveDown();
                        break;
                }
            }

            //Arrow Key Inputs
            /* Touch Inputs
            if (moveDirection == Vector2.zero && control && readyToMove)
            {
                if (Input.touchCount == 1)
                {
                    touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("TouchPhase: Began");
                        firstTouch = touch.position;
                        lastTouch = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Debug.Log("TouchPhase: Moved");
                        lastTouch = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        Debug.Log("TouchPhase: Ended");
                        lastTouch = touch.position;
                        touchHeld = false;
                        doAnim = true;
                    }
                }
                else if (Input.touchCount == 0)
                {
                    touchHeld = false;
                }

                if (Mathf.Abs(lastTouch.x - firstTouch.x) > swipeDistance || Mathf.Abs(lastTouch.y - firstTouch.y) > swipeDistance)
                {
                    if (Mathf.Abs(lastTouch.x - firstTouch.x) >= Mathf.Abs(lastTouch.y - firstTouch.y))
                    {
                        if (lastTouch.x > firstTouch.x)
                        {
                            //Right movement
                            moveDirection.x = 1;
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            //Left movement
                            moveDirection.x = -1;
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                    }
                    else
                    {
                        if (lastTouch.y > firstTouch.y)
                        {
                            //Up movement
                            moveDirection.y = 1;
                        }
                        else
                        {
                            //Down movement
                            moveDirection.y = -1;
                        }
                    }
                    firstTouch = lastTouch;
                    touchHeld = true;
                }
                else if (touchHeld)
                {
                    Debug.Log("Touch Held");
                    moveDirection = lastDirection;
                    if (moveDirection.x == 1)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (moveDirection.x == -1)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }

                if (touchHeld || doAnim)
                {
                    animator.SetFloat("Vert", moveDirection.y + 2);

                    if (animator.GetBool("Push") && moveDirection != lastDirection)
                    {
                        animator.SetBool("Push", false);
                    }
                    if (moveDirection == Vector2.zero)
                    {
                        animator.SetBool("Move", false);
                    }
                }
                doAnim = false;

            }
            */

            

            //PC inputs 
            if (moveDirection == Vector2.zero && control && readyToMove)
            {
                doAnim = true;
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

                

            }
            // Handle animations
            if (doAnim)
            {
                animator.SetFloat("Vert", moveDirection.y + 2);

                if (animator.GetBool("Push") && moveDirection != lastDirection)
                {
                    animator.SetBool("Push", false);
                }
                if (moveDirection == Vector2.zero)
                {
                    animator.SetBool("Move", false);
                }
                doAnim = false;
            }


            // Check for Restart
            if (Input.GetKeyDown("r") && readyToRestart)
            {
                RestartLevel();
            }

        }

    }

    public void MoveLeft()
    {
        doAnim = true;
        moveDirection.x = -1;
        if (facingForward)
        {
            facingForward = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void MoveRight()
    {
        doAnim = true;
        moveDirection.x = 1;
        if (!facingForward)
        {
            facingForward = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MoveUp()
    {
        doAnim = true;
        moveDirection.y = 1;
    }

    public void MoveDown()
    {
        doAnim = true;
        moveDirection.y = -1;
    }

    private void FixedUpdate()
    {
        //animator.SetBool("Move", false);
        //animator.SetBool("Push", false);
        pos = transform.position;
        if (!moving && moveDirection != Vector2.zero)
        {
            target = pos + moveDirection;

            RaycastHit2D raycastHit = Physics2D.BoxCast(target, Vector2.one * 0.5f, 0f, Vector2.zero, Mathf.Infinity, lmask);
            Debug.Log(raycastHit.collider);
            if (raycastHit.collider == null)
            {
                //Empty space ahead - Move as usual
                tupleList = new List<(Vector2, GameObject)>();
                tupleList.Add((pos, gameObject));
                undoStack.Push(tupleList);

                moveSound.Play();
                moving = true;
                readyToMove = false;
                animator.SetBool("Push", false);
            }
            else if (raycastHit.collider.tag == "Wall" || raycastHit.collider.tag == "LightDetector")
            {
                //Wall ahead - don't move
                
                moveDirection = Vector2.zero;
                animator.SetBool("Move", false);
            }
            else if (raycastHit.collider.tag == "Pushable")
            {
                //Pushable ahead - move and interact with pushable
                if (raycastHit.collider.gameObject.GetComponent<Pushable>().TryPush(moveDirection))
                {
                    //Pushable ahead returned true - Move ahead !
                    tupleList = new List<(Vector2, GameObject)>();
                    tupleList.Add((pos, gameObject));

                    tupleList = raycastHit.collider.gameObject.GetComponent<Pushable>().OnPush(moveDirection, tupleList);
                    
                    undoStack.Push(tupleList);

                    pushSound.Play();
                    moving = true;
                    readyToMove = false;
                    animator.SetBool("Push", true);
                }
                else if (axemove)
                {
                    axemove = false;
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
        }
        if ((Vector2)transform.position == target)
        {
            
            if(moving)
            {
                lastDirection = moveDirection;
                StartCoroutine(WaitToMove());
            }
            moveDirection = Vector2.zero;
            moving = false;
        }

    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.04f);
        if(control) { readyToMove = true; gameObject.layer = 0; }
        
    }

    IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(0.8f);
        readyToRestart = true;
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
        readyToMove = false;
        StartCoroutine(WaitAfterDeath());
        gameObject.layer = 1;
        //StartCoroutine(WaitToDie());
        //moving = false;
        //moveDirection = Vector2.zero;
        
        dieSound.Play();
        animator.SetBool("Die", true);
        death.SetTrigger("Death");
        UiDeath.SetTrigger("Death");
   
        //StartCoroutine(WaitToRestart());
        // Add animation ~ and anything else we are gonna do on player death here !!

    }

    public void PlayerWin(string WinLevel)
    {
        if (control && !moving && !readyToWin)
        {
            StartCoroutine(WaitToWin());
            SetControl(false);
        }
        if (readyToWin && control && !moving)
        {
            winSound.Play();
            animator.SetBool("Win", true);
            SetControl(false);
            if (WinLevel == "" || WinLevel == null) {
                StartCoroutine(LevelUp(SceneManager.GetActiveScene().buildIndex + 1));
            } else {
                StartCoroutine(LevelSel(WinLevel));
            }

           if(!gController.GetLevelCompleted(gController.GetCurrentLevel()))
           {
                gController.SetLevelCompleted(gController.GetCurrentLevel());
           }
            
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
    IEnumerator LevelSel(string levelIndex)
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

    public void MoveOntoAxe()
    {
        tupleList = new List<(Vector2, GameObject)>();
        tupleList.Add((pos, gameObject));
        undoStack.Push(tupleList);

        moveSound.Play();
        moving = true;
        readyToMove = false;
        axemove = true;
        animator.SetBool("Push", false);
    }

    IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(0.03f);
        //yield return new WaitForFixedUpdate();
        readyToWin = true;
        control = true;
    }

    IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(1f);
        readyToMove = true;
    }

    IEnumerator WaitToPlayMove()
    {
        yield return new WaitForFixedUpdate();
        moveSound.Play();
    }

    IEnumerator WaitToPlayPush()
    {
        yield return new WaitForFixedUpdate();
        pushSound.Play();
    }

    public void OnUndo()
    {
        if (undoStack.Count > 0)
        {
            tupleList = (List<(Vector2, GameObject)>)undoStack.Pop();
            
            moving = true;
            readyToMove = false;
            pos = transform.position;
            moveDirection = tupleList[0].Item1 - pos;
            target = tupleList[0].Item1;

            if (!control)
            {
                control = true;    
                death.SetTrigger("Revive");
                UiDeath.SetTrigger("Revive");
                animator.SetBool("Die", false);
            }
            
            if (animator.GetBool("Push") && (moveDirection != lastDirection || tupleList.Count == 1))
            {
                animator.SetBool("Push", false);
            }
            if (moveDirection == Vector2.zero)
            {
                animator.SetBool("Move", false);
            }
            

            if (tupleList.Count == 1)
            {
                StartCoroutine(WaitToPlayMove());
            }
            else
            {
                StartCoroutine(WaitToPlayPush());
                animator.SetBool("Push", true);
            }

            if (moveDirection.x == -1 && !facingForward)
            {
                facingForward = true;
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (moveDirection.x == 1 && facingForward)
            {
                facingForward = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
            animator.SetFloat("Vert", 2 - moveDirection.y);

            for (int i = 1; i < tupleList.Count; i++)
            {
                tupleList[i].Item2.GetComponent<Pushable>().OnUndo(tupleList[i].Item1);
            }
        }
    }

}
