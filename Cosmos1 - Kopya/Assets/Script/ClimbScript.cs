using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ClimbScript : MonoBehaviour
{
    public bool useIK;
    public bool leftHandIK;
    public bool rightHandIK;
    public Vector3 leftHandPos;
    public Vector3 rightHandPos;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        RaycastHit LHit;
        RaycastHit RHit;

        if (Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), out LHit, 1f))
        {
            leftHandIK = true;
            leftHandPos = LHit.point;
        }
        else
        {
            leftHandIK = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(0.5f, 0.0f, 0.0f), out RHit, 1f))
        {
            rightHandIK = true;
            rightHandPos = RHit.point;
        }
        else
        {
            rightHandIK = false;
        }
    }
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(0.5f, 0.0f, 0.0f), Color.green);
    }
}
