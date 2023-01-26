using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grapplinghook : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappable;
    public Transform gunTip;
    public Transform mCamera;
    public Transform player;
    public float maxDistance;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartGrapple();
        else if (Input.GetMouseButtonUp(0))
            StopGrapple();
        ChangeJointLength();
    }

    private void LateUpdate()
    {
        DrawRope();

    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(mCamera.position,mCamera.forward,out hit, maxDistance, grappable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = true;
            }

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.3f;

            joint.spring = 5f;
            joint.damper = 10f;
            joint.massScale = 5f;

            lr.positionCount = 2;
        }
    }

    void ChangeJointLength()
    {
        if (joint == null)
            return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(joint.maxDistance > 0 && joint.minDistance > 0)
            joint.maxDistance -= joint.maxDistance / 2;
            joint.minDistance -= joint.minDistance / 2;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (joint.maxDistance >= 50 && joint.minDistance >= 50)
            {
                joint.maxDistance += joint.maxDistance / 2;
                joint.minDistance += joint.maxDistance / 2;
            }
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if(joint == null)
            return;
        lr.SetPosition(0, gunTip.position);
        if (joint.connectedBody != null)
            lr.SetPosition(1, joint.connectedBody.position);
        else
            lr.SetPosition(1, grapplePoint);

    }

    public bool isGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplingPoint()
    {
        if (joint.connectedBody != null)
            return joint.connectedBody.position;
        else
        return grapplePoint;
    }

}
