using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputSystem _input;
    InputActionMap _playerActions;
    Vector3 _movement;
    Vector2 _rotation;
    [SerializeField] private GameObject _rocketExhaust;

    //Initial working design idea, enhancements to make it more challanging should be brought into the future
    //player enters planet collider
        //gravity gradually increased based on deltatime
        //player uses directional wasd to adjust X directions
        //hold space for booster, can only go positive-Y direction
            //booster takes away from gravity to land slower, this will naturally work itself out
                //because if the gravity var is too big, there will not be enough boost to subtract the gravity and death.
        //Q and E rotates the top point of the player, Q towards negative-X while E towards positive-X

    private void Awake()
    {
        _input = new InputSystem();
        _playerActions = _input.Main;
        _playerActions.Enable();
        
    }
    private void OnEnable()
    {
        _input.Main.Movement.performed += ctx => _movement = ctx.ReadValue<Vector3>();
        _input.Main.Rotation.performed += ctx => _rotation = ctx.ReadValue<Vector2>();
        
       
    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
                                                       
    }

    void Movement()
    {
        transform.Translate(_movement * Time.deltaTime * 1f);
        if(_movement == Vector3.up)
        {
            _rocketExhaust.SetActive(true);
        }
        else
        {
            _rocketExhaust.SetActive(false);
        }
        transform.Rotate(_rotation.y, 0, _rotation.x);
        
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
