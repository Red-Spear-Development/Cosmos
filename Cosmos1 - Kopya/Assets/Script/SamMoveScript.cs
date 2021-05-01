using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamMoveScript : MonoBehaviour
{
    CharacterController controller;
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    public float StrafeTurnSpeed;
    float normalFov;
    public float SprintFov;
    float inputX;
    float inputY;
    float maxSpeed;

    public Transform Sam;
    Animator anim;
    Vector3 StickDirection;
    Vector3 velocity;
    Camera mainCamera;
    Rigidbody rb;
    public float jumpHeight;
    public bool isGround = true;
    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.C;
    public KeyCode JumpButton = KeyCode.Space;
    public enum MovementType
    {
        Directional,
        Strafe
    };
    public MovementType hareketTipi;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        normalFov = mainCamera.fieldOfView;
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (hareketTipi == MovementType.Strafe)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            anim.SetFloat("iX", inputX, damp, Time.deltaTime * 10);
            anim.SetFloat("iY", inputY, damp, Time.deltaTime * 10);

            var hareketEdiyor = inputX != 0 || inputY != 0;

            if (hareketEdiyor)
            {
                float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                anim.SetBool("strafeMoving", true);
            }
            else
            {
                anim.SetBool("strafeMoving", false);
            }
        }

        if (hareketTipi == MovementType.Directional)
        {
            InputMove();
            InputRotation();

            StickDirection = new Vector3(inputX, 0, inputY);

            if (Input.GetKey(SprintButton) && isGround == true)
            {
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, SprintFov, Time.deltaTime * 2);

                maxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(WalkButton) && isGround == true)
            {
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);

                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(JumpButton) && isGround == true)
            {
                rb.AddForce(Vector3.up * jumpHeight);
            }
            else
            {
                maxSpeed = 1f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            if (Input.GetKey(KeyCode.Space) && isGround == true)
            {
                rb.AddForce(Vector3.up * jumpHeight);
                anim.SetBool("Jump", true);
                isGround = false;
            }
            else
            {
                anim.SetBool("Jump", false);
            }
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                inputX = 2 * Input.GetAxis("Vertical");
                anim.SetBool("Slide", true);
            }
            else
            {
                anim.SetBool("Slide", false);
            }
            if (Input.GetKey(KeyCode.E))
            {
                inputX = 2 * Input.GetAxis("Vertical");
                anim.SetBool("Dodge", true);
            }
            else
            {
                anim.SetBool("Dodge", false);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    void InputMove()
    {
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    void InputRotation()
    {
        Vector3 rotOfset = mainCamera.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Sam.forward = Vector3.Slerp(Sam.forward, rotOfset, Time.deltaTime * rotationSpeed);
    }

}
