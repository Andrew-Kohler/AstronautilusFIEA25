using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Controls.IActionsActions
{
    private InputAction _moveAction;
    private Vector2 _moveValues;
    private Vector3 _velocity;

    private Rigidbody _rb;
    private Animator _anim;

    public float MoveSpeed = 7f;
    public float xDir;
    public float yDir;

    Controls controls;

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
        _moveAction.Enable();
        
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        _moveValues = _moveAction.ReadValue<Vector2>();
        xDir = _moveValues.x;
        yDir = _moveValues.y;

        _velocity = new Vector3(_moveValues.x * MoveSpeed, 0, _moveValues.y * MoveSpeed);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }
}
