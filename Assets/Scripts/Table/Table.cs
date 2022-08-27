using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;

    private static Table _instance;

    public static Vector3 StartPoint => _instance.startPoint;
    public static Vector3 EndPoint => _instance.endPoint;

    private void Awake() {
        if(_instance == null){
            _instance = this;

            startPoint.y = -10;
            endPoint.y = 10;
        }
        else{
            throw new System.NotSupportedException("The scene can have only one \"Table\"");
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        GizmosExtensions.DrawRectangleGizmos(startPoint, endPoint, transform.position.y);
    }
}
