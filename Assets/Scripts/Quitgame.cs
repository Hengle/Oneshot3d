using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitgame : MonoBehaviour
{
    public void doExitGame()
    {
        GameObject world = GameObject.Find("citytestmap");
        WorldPos startgamer = GameObject.Find("citytestmap").GetComponent(typeof(WorldPos)) as WorldPos;
        startgamer.goingdown = false;
    }
}
