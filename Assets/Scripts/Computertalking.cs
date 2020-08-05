using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Computertalking : MonoBehaviour
{
    private int whatinteractareyouon = 0;
    public GameObject background;
    public bool canplayerdostuff;
    public GameObject computericon;
    public string[] wordstosay;
    public GameObject openasset;
    public Text wheretosayit;
    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(false);
        computericon.SetActive(false);
        wheretosayit.gameObject.SetActive(false);
    }
    private void Update()
    {
       
    }

    void nexttextpls()
    {
        whatinteractareyouon += 1;
        wheretosayit.text = wordstosay[whatinteractareyouon];

    }
}


