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

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.3f;

            joint.spring = 5f;
            joint.damper = 10f;
            joint.massScale = 5f;

            lr.positionCount = 2;
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
        lr.SetPosition(1, grapplePoint);
    }

    public bool isGrappling()
    {
        return joint != null;
    }
}
