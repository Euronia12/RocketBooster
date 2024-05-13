using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving;
    private float _movementDirection;
    
    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;
    private readonly float ENERGY_BURSTER = 3f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }
    
    private void FixedUpdate()
    {
        if (!_isMoving) return;
        
        //if(!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;
        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    public void OnMove(InputAction.CallbackContext context)
    {
        _isMoving = true;
        Vector2 inputValue = context.ReadValue<Vector2>().normalized;       
        _movementDirection = inputValue.x;
        Debug.Log("aa");

    }

    // OnBoost 구현
    // private void OnBoost...
    public void OnBoost()
    {
        _isMoving = true;
        _rocketMovement.ApplyBoost(ENERGY_BURSTER);
    }
}