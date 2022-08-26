using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Racket : MonoBehaviour
{
    [field:Header("> Owner")]
    [field:SerializeField] public Owner RacketOwner { get; private set; }

    [Header("> Movement")]
    [field:SerializeField] private Vector3 minPosition;
    [field:SerializeField] private Vector3 maxPosition;
    [field:Space]
    [field:SerializeField] [field:Range(0, 100f)] public float MovementSpeed { get; private set; } = 1f;

    [Header("> Rotation")]
    [SerializeField] private Vector3 rotationThreshold = new Vector3(0, 30, 75);
    [SerializeField] [CurveRange(0, -1, 1, 1)] private AnimationCurve rotationCurve;
    [Space]
    [SerializeField] [Range(0, 100f)] private float rotationSpeed = 15f;

    [field:Header("> Serves")]
    [field:SerializeField] public Vector3 ServePosition { get; private set; }
    [field:Space]
    [field:SerializeField] public float BallServePositionRange { get; private set; } = 3f;

    public Vector3 Velocity => _rigidbody.velocity;
    public Vector3 RigidbodyPosition => _rigidbody.position;
    public Vector3 StartPosition { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.position = StartPosition = transform.position;
    }

    public void MoveToPosition(Vector3 targetPosition){
        targetPosition.y = StartPosition.y;
        targetPosition = targetPosition.Clamp(minPosition, maxPosition);

        _rigidbody.MovePosition(Vector3.Lerp(RigidbodyPosition, targetPosition, MovementSpeed * Time.deltaTime));
    }

    public void UpdateRotation(){
        float progress = GetMovemenetProgress();

        Vector3 newRotation = new Vector3(){
            y = rotationThreshold.y * rotationCurve.Evaluate(progress),
            z = rotationThreshold.z * rotationCurve.Evaluate(progress)
        };
        
        _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime));
    }

    public void SetMovementSpeed(float speed) => MovementSpeed = Mathf.Clamp(speed, 0, 100);

    public float GetMovemenetProgress()
        => ((_rigidbody.position.x - minPosition.x) / (maxPosition.x - minPosition.x));

    public enum Owner{
        Player,
        Bot
    }
}