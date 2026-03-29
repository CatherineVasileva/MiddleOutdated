using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _jerkAction;
    private float2 _moveInput;
    private float _shootInput;
    private float _jerkInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _jerkAction = new InputAction("Lerk", binding: "<Keyboard>/LeftShift");
        _jerkAction.started += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.performed += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.canceled += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _jerkAction.Disable();
    }
    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.move = _moveInput;
            inputData.shoot = _shootInput;
            inputData.jerk = _jerkInput;
        });
    }
}
