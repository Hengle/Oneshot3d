using System.Collections;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Yarn.Unity.Example;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Player;
    public bool playing = false;
    public GameObject[] monitors;
    public int screen = 0;
    public Movement charactermove;
    public Transform cameranewpoint;
    public Transform oldpoint;
    public bool canplay = true;
    public AudioSource sfx;
    public AudioClip bleep;
    public AudioClip startup;
    private void Start()
    {
        charactermove.canmove = true;
    }
    private void Update()
    {
        if(playing == true)
         monitors[screen].SetActive(true);
        if(playing == true && Input.GetKeyDown(KeyCode.Space) )
        {
            monitors[screen].SetActive(false);
            sfx.PlayOneShot(bleep); //heh
            screen += 1;

            if (screen >= monitors.Length)
            {
                Process.Start(Application.dataPath + @"/Fake Error Message.vbs");
                Player.SetActive(true);
                playing = false;
                Camera.main.transform.position = oldpoint.position;
                Camera.main.transform.rotation = oldpoint.rotation;
                Camera.main.orthographicSize = 9;
                charactermove.canmove = true;
                canplay = false;

                
            } 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && Input.GetKeyDown(KeyCode.Space) && playing == false && canplay == true)
        {
            sfx.PlayOneShot(startup);
            playing = true;
            charactermove.canmove = false;
            Player.SetActive(false);
            Camera.main.orthographicSize = 0.5f;
            Camera.main.transform.position = cameranewpoint.position;
            Camera.main.transform.rotation = cameranewpoint.rotation;
        }
    }
}
