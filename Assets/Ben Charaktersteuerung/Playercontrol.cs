using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Playercontrol : MonoBehaviour
{

    public float moveSpeed;

    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float sprintDuration = 2.0f;
    public float sprintCooldownDuration = 2.0f;

    public float rotationSpeed = 100.0f;
    [Tooltip("In Degree")] public float rotationAmount = 15.0f;
    public float moveLockOffset = 0.2f;

    public bool gamePaused = false;

    private bool isSprinting = false;
    private bool sprintCooldown = false;

    private bool isMoving = false;
    private bool isRotating = false;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Quaternion destRotation;
    private Coroutine sprintingCoroutine;

    private Animator anim;
    private int walkBool;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        destRotation = transform.rotation;
        moveSpeed = walkSpeed;

        anim = GetComponent<Animator>();
        walkBool = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController != null && !gamePaused)
        {
            //--------------
            //MOVING
            //--------------
            if (!isRotating)
            {
                moveDirection = transform.forward * Input.GetAxis("Vertical");
                moveDirection = moveDirection * moveSpeed;

                characterController.SimpleMove(moveDirection);
                if (moveDirection.sqrMagnitude > moveLockOffset)
                {

                    isMoving = true;
                    anim.SetBool(walkBool, true);
                }
                else
                {
                    isMoving = false;
                    anim.SetBool(walkBool, false);
                }
            }



            //------------------
            //ROTATING
            //------------------
            if (!isMoving)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    destRotation.eulerAngles = destRotation.eulerAngles - new Vector3(0, rotationAmount, 0);
                    isRotating = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    destRotation.eulerAngles = destRotation.eulerAngles + new Vector3(0, rotationAmount, 0);
                    isRotating = true;
                }
                transform.rotation = new Quaternion(destRotation.x, destRotation.y, destRotation.z, destRotation.w);

                if (Input.GetKeyUp(KeyCode.A))
                    isRotating = false;
                if (Input.GetKeyUp(KeyCode.D))
                    isRotating = false;
            }

            //---------------------
            //SPRINTING
            //---------------------
            if (Input.GetKey(KeyCode.LeftShift) && !isSprinting && !sprintCooldown)
            {
                sprintingCoroutine = StartCoroutine(Sprinting());
            }
            if (Input.GetKey(KeyCode.LeftShift) && !isSprinting && !sprintCooldown)
            {
                sprintingCoroutine = StartCoroutine(Sprinting());
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopCoroutine(sprintingCoroutine);
                StartCoroutine(SprintCooldown());
                moveSpeed = walkSpeed;
                isSprinting = false;
            }

        }
        else
        {
            characterController.SimpleMove(Vector3.zero);
            isMoving = false;
            isRotating = false;
            isSprinting = false;
            StopCoroutine(sprintingCoroutine);
        }
    }

    IEnumerator Sprinting()
    {
        isSprinting = true;
        moveSpeed = sprintSpeed;
        yield return new WaitForSeconds(sprintDuration);
        moveSpeed = walkSpeed;
        isSprinting = false;
        StartCoroutine(SprintCooldown());
    }

    IEnumerator SprintCooldown()
    {
        sprintCooldown = true;
        yield return new WaitForSeconds(sprintCooldownDuration);
        sprintCooldown = false;
    }
}
