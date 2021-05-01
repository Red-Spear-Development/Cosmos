using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLookScript : MonoBehaviour
{
    float weight = 1;
    Animator anim;
    Camera mainCam;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }
    void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(.5f, .5f, 1.2f, .5f, .5f);

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        anim.SetLookAtPosition(lookAtRay.GetPoint(50));

    }
    public void art()
    {
        weight = Mathf.Lerp(weight, 1f, Time.fixedDeltaTime);
    }
    public void azal()
    {
        weight = Mathf.Lerp(weight, 0, Time.fixedDeltaTime);
    }
}
