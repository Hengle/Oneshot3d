using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
    public GameObject[] buttons;
    WorldPos startgamer;
    // Update is called once per frame

    private void Start()
    {
        startgamer = GameObject.Find("citytestmap").GetComponent(typeof(WorldPos)) as WorldPos;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (buttons.Contains(hit.collider.gameObject))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.name == "Start")
                    {
                        startgamer.goingdown = true;
                        startgamer.speed = 5;
                    }
                    if (hit.collider.gameObject.name == "Quit")
                        Application.Quit();
                }
                // handle mouseclick
            }
        }

    }
}
