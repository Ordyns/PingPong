using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Racket racket;
    [Space]
    [SerializeField] private InputPanel inputPanel;
    [SerializeField] private float inputDeltaCoefficient = 0.005f;

    private Vector3 _racketPositionBeforeMovement;

    private void Start() {
        OnRacketMovementStarted();
        inputPanel.DragStarted += OnRacketMovementStarted;
    }

    private void OnRacketMovementStarted(){
        _racketPositionBeforeMovement = racket.RigidbodyPosition;
    }

    private void FixedUpdate() {
        Vector3 input = new Vector3(inputPanel.Delta.x, 0, inputPanel.Delta.y) * inputDeltaCoefficient;
        Vector3 targetPosition = _racketPositionBeforeMovement + input;
        racket.MoveToPosition(targetPosition);
        racket.UpdateRotation();
    }
}
