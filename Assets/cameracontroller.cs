using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    //the player and where they are
    public GameObject player;
    public int playerchunk;

    //split the map into "chunks"
    public Transform[] locationcheckers;
    public GameObject[] camerapoints;

    //variable to get a camerapoint
    public int cycler;

    //what the position used to be
    public int oldpos;

    //check if this is the first frame
    private bool firsttime = true;

    //check when to start the clock for a lerp
    public bool islerping = false;
    private float howfarisit;

    //where to and where from
    private GameObject whereto;
    private GameObject wherefrom;

    //where to and where from to rotate
    private Quaternion wheretor;
    private Quaternion wherefromr;

    //how fast should you move
    public float distancebetween;

    //how far we have moved already
    public float distCovered;

    void Update()
    {
        cycler = 1;
        //run this section until you get the player's chunk
        Tryagain:
        if ((player.transform.position.z > locationcheckers[cycler].position.z) && (player.transform.position.z < locationcheckers[cycler - 1].position.z))
        {
            playerchunk = cycler - 1;
        }
        else
        {
            cycler += 1;
            goto Tryagain;
        }

        //check for first time so things don't bug
        if(firsttime == true)
        {
            oldpos = playerchunk;
            playerchunk += 1;
            firsttime = false;
        }

        //check if the camera needs to move
        if (playerchunk == oldpos && islerping == false)
        {
            this.transform.position = camerapoints[playerchunk].gameObject.transform.position;
            return;
        }

        //okay so we know it needs to move so now it's time to LERP
        //are we already lerping? if not start the clock
        if (islerping == false)
        {
            //where are we going, where are we coming from
            whereto = camerapoints[playerchunk].gameObject;
            wherefrom = camerapoints[oldpos].gameObject;
            //match rotations
            wherefromr = wherefrom.transform.rotation;
            wheretor = whereto.transform.rotation;
            //check distance and prevent this from rerunning
            howfarisit = Vector3.Distance(wherefrom.transform.position, whereto.transform.position);
            islerping = true;
            return;
        }
        //now fetch where the player is inbetween the two points
        if (wherefrom.transform.position.z < whereto.transform.position.z)
        {
            distancebetween = -1 * (player.transform.position.z - whereto.transform.position.z);
        }
        else
        {
            distancebetween = -1 * (player.transform.position.z - wherefrom.transform.position.z);
        }


        //check what fraction of the way we are

        float fractionOfJourney = distancebetween / howfarisit;

        //execute the stuff that we set up


        //get the inbetween position
        this.gameObject.transform.position = Vector3.Lerp(wherefrom.transform.position, whereto.transform.position, fractionOfJourney);
        //get the inbetween rotation
        this.gameObject.transform.rotation = Quaternion.Lerp(wherefromr, wheretor, fractionOfJourney);
        if (fractionOfJourney == 1)
        {
            islerping = false;
        }
    }
}
