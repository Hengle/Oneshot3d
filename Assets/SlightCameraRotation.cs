using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlightCameraRotation : MonoBehaviour
{
    public float rotationscalex;
    public float rotationscaley;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 originalcamerarot = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float Camerax = Mathf.Clamp((540 - Mathf.Abs(Input.mousePosition.y))*rotationscaley, -30, 30);
        float Cameray = Mathf.Clamp((960 - Mathf.Abs(Input.mousePosition.x)) * rotationscalex, -30, 30);
        this.transform.localEulerAngles = new Vector3(Camerax, Cameray, 0f);

    }
}
