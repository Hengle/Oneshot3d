using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCameraPanner : MonoBehaviour
{
    public float speed;
    public float speedx;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if((player.transform.position.z > -10.77) && (player.transform.position.z < -6.42)) { }
        this.transform.eulerAngles = new Vector3(
            speedx * player.transform.position.z,
             speed * player.transform.position.z,
              this.transform.eulerAngles.z);
              }
       
    }

 