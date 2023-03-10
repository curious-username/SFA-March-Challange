using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sandbox : MonoBehaviour
{

    InputSystem _input;
    InputActionMap _playerActions;
    Vector3 _movement, _change;
    Vector2 _rotation;
    Rigidbody _gravity;

    [SerializeField] private GameObject _rocketExhaust, _planet;
    private void Awake()
    {
        _input = new InputSystem();
        _playerActions = _input.Main;
        _playerActions.Enable();

    }
    private void OnEnable()
    {
        _input.Main.Movement.performed += ctx => _movement = ctx.ReadValue<Vector3>();
        


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
        Vector3 direction = _planet.transform.position - transform.position;
        


        transform.Translate(_movement * Time.deltaTime);
        transform.LookAt(_planet.transform);
        _gravity.AddForce(direction * 0.01f, ForceMode.Impulse);



        //var artificalGravity = transform.position - _planet.transform.position;
        //var movementCache = _movement;
        //_change = _movement * Time.deltaTime * .01f;
        Debug.Log(_change);


        // look at works, need to figure out local and world position to fix this.

        
    }
}
