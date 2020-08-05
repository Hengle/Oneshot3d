using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        Destroy();
    }
    void Destroy ()
    {
        Destroy(this.gameObject);
    }
}

    

