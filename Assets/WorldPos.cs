using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldPos : MonoBehaviour
{
    public float start;
    public float end;
    public float speed;
    public bool goingdown = true;
    private void Start()
    {
        this.transform.position = new Vector3(0f, start, 0f);
    }

    void Update()
    {
        if (goingdown == true)
        {
            if (this.transform.position.y < end)
            {
                this.transform.Translate(new Vector3(0f, speed * Time.deltaTime, 0f));
                if (speed != 0)
                    speed += Time.deltaTime * 4f;
            }
            if (this.transform.position.y > end)
                this.transform.position = new Vector3(0f, end, 0f);
        }
        else
        {
            if (this.transform.position.y > start)
            {
                this.transform.Translate(new Vector3(0f, -speed * Time.deltaTime, 0f));
                if (speed != 0)
                    speed += Time.deltaTime * 4f;
            }
            if (this.transform.position.y < start)
                this.transform.position = new Vector3(0f, start, 0f);
        }
    }
}
