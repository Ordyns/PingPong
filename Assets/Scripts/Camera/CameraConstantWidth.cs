using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private bool setSizeOnStart;
    [Space]
    [SerializeField] private Vector2 defaultResolution = new Vector2(1080, 1920);
    [SerializeField] [Range(0f, 1f)] private float widthOrHeight = 0;

    private Camera _camera;
    
    private float _initialSize;
    private float _initialFov;
    private float _horizontalFov;
    private float _targetAspect;

    private void OnValidate() {
        _camera = GetComponent<Camera>();
    }

    private void Awake(){
        _initialSize = _camera.orthographicSize;

        _targetAspect = defaultResolution.x / defaultResolution.y;

        _initialFov = _camera.fieldOfView;
        _horizontalFov = GetConstVerticalFov(_initialFov, 1 / _targetAspect);

        if(setSizeOnStart){
            if(_camera.orthographic)
                _camera.orthographicSize = GetConstOrthoSize(_initialSize);
            else
                _camera.fieldOfView = GetConstFov();
        }
    }

    public float GetConstOrthoSize(float initialSize){
        float constantWidthSize = initialSize * (_targetAspect / _camera.aspect);
        return Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
    }

    public float GetConstFov(){
        return Mathf.Lerp(GetConstVerticalFov(_horizontalFov, _camera.aspect), _initialFov, widthOrHeight);
    }

    private float GetConstVerticalFov(float fov, float aspectRatio){
        float horizontalFovInRads = fov * Mathf.Deg2Rad;
        float verticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRads / 2) / aspectRatio);
        return verticalFovInRads * Mathf.Rad2Deg;
    }
}