using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class SEtPlayeRName : MonoBehaviour
{
    public InMemoryVariableStorage varstorage;
    public string username;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(varstorage);
        username = Environment.UserName;
        StartCoroutine(dothething());

    }
    IEnumerator dothething()
    {
        yield return new WaitForSeconds(0.1f);
        varstorage.SetValue("$Player", username);
    }
}
 
