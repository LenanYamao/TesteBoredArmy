using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController {
    // Public for external hooks
    public FrameInput PlayerInput { get; private set; }
    public Vector3 RawMovement { get; private set; }

    private float _currentHorizontalSpeed, _currentVerticalSpeed;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update() {
        GetInput();
        CalculateMove();

        MoveCharacter();
    }

    private void GetInput() {
        PlayerInput = new FrameInput {
            X = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };
    }

    [Header("Movement")]
    public float _acceleration = 90;
    public float _moveClamp = 13;
    public float _deceleration = 60f;
    public float _rotationSpeed = 20f;

    private void CalculateMove() {
        if (PlayerInput.X != 0) {
            // Rotate player in the direction
            transform.Rotate(new Vector3(0, 0, PlayerInput.X * _rotationSpeed * -1 * Time.deltaTime));
        }
        else {
            // No input. Let's slow the character down
            _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0f, _deceleration * Time.deltaTime);
        }
        if (PlayerInput.y > 0)
        {
            // Set move speed
            _currentVerticalSpeed += PlayerInput.y * _acceleration * Time.deltaTime;

            // clamped by max frame movement
            _currentVerticalSpeed = Mathf.Clamp(_currentVerticalSpeed, 0f, _moveClamp);
        }
        else
        {
            // No input. Let's slow the character down
            _currentVerticalSpeed = Mathf.MoveTowards(_currentVerticalSpeed, 0, _deceleration * Time.deltaTime);
        }
    }

    private void MoveCharacter() {
        RawMovement = transform.up * _currentVerticalSpeed; // Used externally
        var move = RawMovement * Time.deltaTime;
        transform.position += move;
    }

    public void triggerState(string state)
    {
        if (anim)
        {
            switch (state)
            {
                case "Damaged":
                    anim.SetTrigger("Damaged");
                    break;
                case "Critical":
                    anim.SetTrigger("Critical");
                    break;
                default:
                    break;
            }
        }
    }
}