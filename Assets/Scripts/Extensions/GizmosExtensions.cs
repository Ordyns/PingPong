using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtensions
{
    public static void DrawRectangleGizmos(Vector3 startPoint, Vector3 endPoint, float y){
        var bottomLeftPoint = new Vector3(startPoint.x, y, startPoint.z);
        var bottomRightPoint = new Vector3(endPoint.x, y, startPoint.z);
        var topRightPoint = new Vector3(endPoint.x, y, endPoint.z);
        var topLeftPoint = new Vector3(startPoint.x, y, endPoint.z);

        Gizmos.DrawLine(bottomLeftPoint, bottomRightPoint);
        Gizmos.DrawLine(bottomRightPoint, topRightPoint);
        Gizmos.DrawLine(topRightPoint, topLeftPoint);
        Gizmos.DrawLine(topLeftPoint, bottomLeftPoint);
    }
}
