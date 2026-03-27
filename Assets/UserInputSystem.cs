using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private float2 _moveInput;
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
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
    }
    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.move = _moveInput;
        });
    }
}
