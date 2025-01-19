using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Controls.IActionsActions
{
    private InputAction _moveAction;
    private InputAction _dashAction;
    private Vector2 _moveValues;
    public bool _canDash = true;       // If you are allowed to dash
    public bool _dashActive;           // If the dash state is active
    private Vector3 _velocity;

    private Rigidbody _rb;
    private Animator _anim;

    public float MoveSpeed = 7f;
    public float DashSpeed = 12f;
    public float dashActiveTime = .1f;
    public float dashCooldownTime = 3f;
    public float dashCooldownTimer = 0f;
    public float xDir;
    public float yDir;

    Controls controls;

    public GameObject footsteps;
    AudioSource audioSource;

    [SerializeField] GameObject backRightLegAnim;
    [SerializeField] GameObject backLeftLegAnim;
    [SerializeField] GameObject frontRightLegAnim;
    [SerializeField] GameObject frontLeftLegAnim;




    private bool soundPlaying;

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            // Tell the "gameplay" action map that we want to get told about
            // when actions get triggered.
            controls.actions.SetCallbacks(this);
        }
        controls.actions.Enable();
    }

    void Start()
    {
        _moveAction = controls.FindAction("Move");
        _dashAction = controls.FindAction("Dash");
        _moveAction.Enable();
        _dashAction.Enable();
        
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        audioSource = footsteps.GetComponent<AudioSource>();

        

    }

    // Update is called once per frame
    void Update()
    {
        _moveValues = _moveAction.ReadValue<Vector2>();
        xDir = _moveValues.x;
        yDir = _moveValues.y;

        if (_moveValues.x != 0 || _moveValues.y != 0) 
        {
            backRightLegAnim.GetComponent<Animator>().Play("BLeg_RWalk", 0);
            backLeftLegAnim.GetComponent<Animator>().Play("BLeg_LWalk", 0);
            frontRightLegAnim.GetComponent<Animator>().Play("FLeg_R_Walk", 0);
            frontLeftLegAnim.GetComponent<Animator>().Play("FLeg_L_Walk", 0);


            if (!soundPlaying)
            {

                audioSource.Play();
                soundPlaying = true;
            }

            
        }
        else if (_moveValues.x == 0 && _moveValues.y == 0) 
        {
            backRightLegAnim.GetComponent<Animator>().Play("BLeg_RStatic", 0);
            backLeftLegAnim.GetComponent<Animator>().Play("BLeg_LStatic", 0);
            frontRightLegAnim.GetComponent<Animator>().Play("FLeg_R_Static", 0);
            frontLeftLegAnim.GetComponent<Animator>().Play("FLeg_LStatic", 0);

            if (soundPlaying)
            {
                audioSource.Stop();
                soundPlaying = false;
            }
            
        }

        if (_dashAction.WasPressedThisFrame() && _canDash == true)
        {
            _dashActive = true;
            _canDash = false;
            StartCoroutine(DoDashCooldown());
        }

        if (_dashActive)
        {
            _velocity = new Vector3(_moveValues.x, 0, _moveValues.y).normalized * DashSpeed;
        }
        else
        {
            _velocity = new Vector3(_moveValues.x, 0, _moveValues.y).normalized * MoveSpeed;
        }
    
    }

    private void FixedUpdate()
    {
        MovePlayer3D();
    }

    private void MovePlayer3D()
    {

        _rb.velocity = _velocity;
            
        /*if (!IsIdle)
        {
            
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }*/

    }

    private IEnumerator DoDashCooldown()
    {
        yield return new WaitForSeconds(dashActiveTime);
        _dashActive = false;

        dashCooldownTimer = dashCooldownTime;
        while(dashCooldownTimer > 0)
        {
            dashCooldownTimer-= Time.deltaTime;
            yield return null;
        }
        _canDash = true;
    }

    #region Silly New Input Stuff

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnDash(InputAction.CallbackContext context)
    {

    }

    #endregion
}
