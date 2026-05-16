using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UserInputSystem : MonoBehaviour
{
    private NewControls _gameInput;
    private IMoveable _moveController;
    private IAbility _shootAbility;

    private Vector2 _moveInput;
    private float _shootInput;
    private float _jerkInput;
    public Vector2 MoveInput => _moveInput;
    public float ShootInput => _shootInput;
    public float JerkInput => _jerkInput;

    private void Awake()
    {
        _gameInput = new NewControls();
        _gameInput.Enable();
        _moveController = GetComponent<IMoveable>();
        _shootAbility = GetComponent<IAbility>();
    }
    private void OnEnable()
    {
        _gameInput.Map1.shoot.performed += Attack;
    }

    private void OnDisable()
    {
        _gameInput.Map1.shoot.performed -= Attack;
    }

    private void Update()
    {
        _moveInput = _gameInput.Map1.move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        _moveController.Move(_moveInput);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        _shootAbility.Execute();
    }
}
