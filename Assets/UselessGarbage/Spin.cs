using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Spins a gameobject, this is used for the background on certain tabs.
public class Spin : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);
    }
}
