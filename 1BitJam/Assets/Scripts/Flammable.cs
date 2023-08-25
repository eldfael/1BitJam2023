using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public bool OnFire = false;
    public Animator FireAnimation;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (OnFire)
        {
            CatchFire();
        }
        //Debug.Log(this.gameObject.transform.GetChild(0).name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatchFire() {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.layer = 1;
        FireAnimation.SetTrigger("Fire");
    }
}
