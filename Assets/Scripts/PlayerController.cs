using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    //VARIABLES
    [SerializeField]
    float moveSpeed = 2f;
    [SerializeField]
    float runSpeed = 4f;
    [SerializeField]
    float crouchSpeed = 0.75f;

    [SerializeField]
    float airMoveSpeedRatio = 0.75f;
    [SerializeField]
    float jumpForce = 6.5f;
    [SerializeField]
    float gravityScale = 0.75f;

    private Vector3 moveDirection;

    const float k_Half = 0.5f;

    //PLAYER STATES
    public bool CanMove = true;
    private bool IsCrouching = false;
    private bool IsRunning = false;

    public bool GroundCheck = true;
    public LayerMask ignoreLayers;

    //REFERENCES
    private CharacterController cc;
     //CHARACTER CAPSULE
    private float ccHeight;
    private Vector3 ccCenter;
   public GameObject triggerPanel;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        ccHeight = cc.height;
        ccCenter = cc.center;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       }

    // Update is called once per frame
    void Update()
    {

        GroundCheck = checkGrounded();

        if (GroundCheck)
        {
            MoveDirection();
            //Footsteps();
        }
        else if (!GroundCheck)
        {
            Airborne();
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        cc.Move(moveDirection * Time.deltaTime);

    }


    private void MoveDirection()
    {
        moveDirection.y = 0f;
        float yStore = moveDirection.y;
        if (!IsRunning) { moveSpeed = 2f; }
        if (IsRunning) { moveSpeed = runSpeed; }
        if (IsCrouching) { moveSpeed = crouchSpeed; }

        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            if (!IsRunning)
                moveSpeed = crouchSpeed;
            else
                moveSpeed = 2f;
        }
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        //START JUMPING
        if (Input.GetKey(KeyCode.Space) && !IsCrouching)
        {
            moveDirection.y = jumpForce;
        }
        //START AND STOP RUNNING
        if (Input.GetKey(KeyCode.LeftShift) && !IsCrouching)
        {
            IsRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
        }
        //START CROUCH
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ScaleCapsuleForCrouching();
        }
        //STOP CROUCH
        if (Input.GetKeyUp(KeyCode.LeftControl) && IsCrouching)
        {
            ScaleBackCapsule();
        }
    }

 

    private void Airborne()
    {
        float yStore = moveDirection.y;
        IsRunning = false;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * (moveSpeed * airMoveSpeedRatio);
        moveDirection.y = yStore;

    }

    void ScaleCapsuleForCrouching()
    {
        if (IsCrouching) return;
        cc.height = cc.height / 2f;
        cc.center = cc.center / 2f;
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsCrouching", true);
        transform.GetChild(0).GetComponent<Animator>().Play("CamDown");
        
        //anim.SetBool("IsCrouching", true);
        IsCrouching = true;
    }
    void ScaleBackCapsule()
    {
        if (!IsCrouching) return;
        Ray crouchRay = new Ray(cc.transform.position + Vector3.up * cc.radius * k_Half, Vector3.up);
        float crouchRayLength = ccHeight - cc.radius * k_Half;
        if (Physics.SphereCast(crouchRay, cc.radius * k_Half, crouchRayLength, ignoreLayers, QueryTriggerInteraction.Ignore)) //Physics.AllLayers
        {

            //anim.SetBool("IsCrouching", true);
            IsCrouching = true;
            return;
        }
        cc.height = ccHeight;
        cc.center = ccCenter;
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsCrouching", false);
        transform.GetChild(0).GetComponent<Animator>().Play("CamUp");
        IsCrouching = false;
    }

    bool checkGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }


}
