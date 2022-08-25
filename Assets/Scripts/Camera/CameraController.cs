using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [Space]
    [SerializeField] private Racket racket;
    [Space]
    [SerializeField] private Vector3 minDeadZone;
    [SerializeField] private Vector3 maxDeadZone;
    [Space]
    [SerializeField] [Range(0, 100f)] private float rotationSpeed = 2.5f;
    [SerializeField] [Range(0, 100f)] private float rotationThreshold = 3.5f;

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = camera.transform.position;
        minDeadZone.y = -10;
        maxDeadZone.y = 10;
    }

    private void Update() {
        float rotationProgress = IsRacketInDeadZone() ? 0.5f : racket.GetMovemenetProgress();
        RotateSmoothly(rotationProgress);
    }


    private void RotateSmoothly(float rotationProgress){
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Lerp(rotationThreshold, -rotationThreshold, rotationProgress), transform.eulerAngles.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsRacketInDeadZone()
        => racket.RigidbodyPosition.isVectorValuesInRange(minDeadZone, maxDeadZone);

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        GizmosExtensions.DrawRectangleGizmos(minDeadZone, maxDeadZone, racket.transform.position.y);
    }
}
