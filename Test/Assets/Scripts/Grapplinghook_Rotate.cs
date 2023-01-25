using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplinghook_Rotate : MonoBehaviour
{
    public Grapplinghook grapHook;
    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (!grapHook.isGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }
        else
            desiredRotation = Quaternion.LookRotation(grapHook.GetGrapplingPoint() - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
