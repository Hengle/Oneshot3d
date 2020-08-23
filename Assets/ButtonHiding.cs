using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonHiding : MonoBehaviour
{
    GameObject cityheight;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        cityheight = GameObject.Find("citytestmap");
    }

    // Update is called once per frame
    void Update()
    {
        if (cityheight.transform.position.y == 4)
        {
            int count = 0;
           foreach(GameObject element in buttons)
            {
                buttons[count].SetActive(true);
                count++;
            }
        }
        else
        {
            int count = 0;
            foreach (GameObject element in buttons)
            {
                buttons[count].SetActive(false);
                count++;
            }
        }
    }
}
