using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamMove : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    private Rigidbody rigi;
    void Start()
    {
        Speed = 5f;
        JumpHeight = 5f;
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void MoveControle()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Speed * Time.deltaTime);
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.forward * Speed * Time.deltaTime);
    }
}
