using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode]
//Made by A Blueberry#3141
public class LevelScript : MonoBehaviour
{
    public GameObject instancedterrain;
    public GameObject[] obj;
    public GameObject[] randompool;
    public Vector3 spawnPoint;
    public int whichobject;
    public bool israndom;
    public void BuildObject()
    {
        GameObject newobj;
        if (israndom == true)
        {
                newobj = Instantiate(randompool[Random.Range(0,randompool.Length -1)], spawnPoint, Quaternion.identity);
        }
        else
        {
                 newobj = Instantiate(obj[whichobject], spawnPoint, Quaternion.identity);
        }
        newobj.transform.parent = instancedterrain.transform;
    }

    void OnDrawGizmos()
    {
        if (obj[whichobject].gameObject.GetComponent<MeshFilter>())
            Gizmos.DrawWireMesh(obj[whichobject].gameObject.GetComponent<MeshFilter>().sharedMesh, spawnPoint);
        Gizmos.DrawWireCube(spawnPoint, Vector3.one);    

           
    }
}
