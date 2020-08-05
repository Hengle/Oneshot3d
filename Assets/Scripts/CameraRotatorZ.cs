using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotatorZ : MonoBehaviour
{
    public float speed;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(
            this.transform.eulerAngles.x,
             speed * player.transform.position.z,
              this.transform.eulerAngles.z
        );
    }
}
