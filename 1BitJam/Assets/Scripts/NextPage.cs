using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    public GameObject showPage;
    public GameObject hidePage;
    // Start is called before the first frame update
    public void ShowNextPage()
    {
        showPage.SetActive(true);
        Debug.Log("Next Page");
    }
    public void HideThisPage()
    {
        hidePage.SetActive(false);
    }
}
