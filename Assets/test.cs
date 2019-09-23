using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Position = " + gameObject.transform.position);
        Debug.Log("Local position = " + gameObject.transform.localPosition);
        
    }
}
