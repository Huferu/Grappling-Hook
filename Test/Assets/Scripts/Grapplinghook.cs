using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplinghook : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grappleStartPos;
    public LayerMask grappable;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
}
