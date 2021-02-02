using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 position
    {
        get { return transform.position;}
        set { transform.position = value; }
    }
}
