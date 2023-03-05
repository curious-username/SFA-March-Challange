using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputSystem _input;
    InputActionMap _playerActions;
    Vector3 _movement;
    float _rotation;

    private void Awake()
    {
        _input = new InputSystem();
        _playerActions = _input.Main;
        _playerActions.Enable();
        
    }
    private void OnEnable()
    {
        _input.Main.Movement.performed += ctx => _movement = ctx.ReadValue<Vector3>();
        _input.Main.Rotation.performed += ctx => _rotation = ctx.ReadValue<float>();
        
       
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
        if(_rotation )
        transform.Translate(_movement * Time.deltaTime * 1f);
        transform.Rotate(transform.position, _rotation);
        
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
