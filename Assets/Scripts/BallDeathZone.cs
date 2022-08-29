using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BallDeathZone : MonoBehaviour
{
    [SerializeField] private ZoneType zoneType;

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Ball>(out Ball ball))
            BallDetected(ball);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.TryGetComponent<Ball>(out Ball ball))
            BallDetected(ball);
    }

    private void BallDetected(Ball ball){
        if(ball.BallState == Ball.State.Unplayable)
            return;

        ball.DeathZoneTriggered(zoneType);
    }

    public enum ZoneType{
        GameBorder,
        Net,
        Other
    }
}