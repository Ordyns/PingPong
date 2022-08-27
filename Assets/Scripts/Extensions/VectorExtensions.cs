using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Clamp(this Vector3 vector, float min, float max){
        return Clamp(vector, new Vector3(min, min, min), new Vector3(max, max, max));
    }

    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max){
        float x = Mathf.Clamp(vector.x, min.x, max.x);
        float y = Mathf.Clamp(vector.y, min.y, max.y);
        float z = Mathf.Clamp(vector.z, min.z, max.z);
        return new Vector3(x, y, z);
    }

    public static Vector3 Abs(this Vector3 vector3)
        => new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));

    public static bool IsValueInRange(this float value, float min, float max) => value >= min && value <= max;
    public static bool IsVectorValuesInRange(this Vector3 vector, Vector3 min, Vector3 max) 
        => IsValueInRange(vector.x, min.x, max.x) && IsValueInRange(vector.y, min.y, max.y) && IsValueInRange(vector.z, min.z, max.z);

    public static Vector3 RandomRange(Vector3 min, Vector3 max)
        => new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
}
