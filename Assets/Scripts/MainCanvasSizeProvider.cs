using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MainCanvasSizeProvider : MonoBehaviour
{
    public static Vector2 Size { get; private set; }

    private void Awake() {
        Size = transform.GetComponent<RectTransform>().rect.size;
    }
}
