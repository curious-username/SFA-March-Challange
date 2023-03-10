using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputSystem _input;
    InputActionMap _playerActions;
    private Vector3 _movement, _change, gravityApplied, _direction;
    private Vector2 _rotation;
    private Rigidbody _gravity;
    private float _force = 0;
    private float speed;
    private bool _isLanded = false;

    [SerializeField] private GameObject _rocketExhaust, _planet, _planetParent;

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
        _playerActions.actionTriggered += ctx =>
        {
            if (ctx.performed)
            {
                if(ctx.action.name == "Movement")
                {
                    _movement = ctx.ReadValue<Vector3>();
                    transform.LookAt(_planet.transform);
                }
                //else if(ctx.action.name == "Rotation")
                //{
                //    _rotation = ctx.ReadValue<Vector2>();
                    
                //}

            }
            else if (ctx.canceled)
            {
                _movement = Vector3.zero;
                //_rotation = Vector2.zero;
            }
        };
            
            
            
        



    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }
    void Start()
    { 
        _gravity = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
                                                       
    }

    void Movement()
    {
        if(_isLanded == false)
        {
            transform.Translate(_movement * Time.deltaTime * 1f);
            _direction = _planet.transform.position - transform.position;
            speed = _gravity.velocity.magnitude;
            transform.LookAt(_planet.transform);
        }

        if (_movement == Vector3.back)
        {
            gravityApplied = _direction * (_force - 0.01f);
            _gravity.AddForce(gravityApplied, ForceMode.Impulse);
            _rocketExhaust.SetActive(true);
            

        }
        else
        {
            gravityApplied = _direction * (_force + 0.01f);
            _gravity.AddForce(gravityApplied, ForceMode.Impulse);
            _rocketExhaust.SetActive(false);
        }
        
        

        
        
        

        // look at works, need to figure out local and world position to fix this.


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Planet_A")
        {
            _isLanded = true;
            if (speed < 1f)
            {
                Debug.Log("Good job you landed successfully");
                
            }
            else
            {
                Debug.Log("You crashed, your ship is destroyed");
                
            }
        }
        //Debug.Log(collision.gameObject.name);
    }

}
