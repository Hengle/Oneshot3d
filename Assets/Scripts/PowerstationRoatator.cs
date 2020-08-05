using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerstationRotator : MonoBehaviour
{
    public float speed;
    public float speedx;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(
            speedx * player.transform.position.z,
            speed * player.transform.position.z,
            this.transform.eulerAngles.z
        );
    }
}
