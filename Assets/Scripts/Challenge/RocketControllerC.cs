using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving = true;
    private float _movementDirection;
    
    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }
    
    private void FixedUpdate()
    {
        if (!_isMoving) return;

        if (!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN))
        {
            return;
        }

        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isMoving = true;
            Vector2 inputValue = context.ReadValue<Vector2>().normalized;
            _movementDirection = inputValue.x;
        }
        else if (context.canceled)
        {
            _isMoving = false;
        }
    }

    // OnBoost 구현
    // private void OnBoost...
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.interaction is MultiTapInteraction)
            {
                if (!_energySystem.UseEnergy(ENERGY_BURST))
                {
                    return;
                }
                _isMoving = true;
                _rocketMovement.ApplyBoost(ENERGY_BURST);
            }
        }
    }
}