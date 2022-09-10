using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class MenuCameraAnimator : MonoBehaviour
{
    [SerializeField] private Transform cameraParent;
    private Camera _camera;

    private void OnValidate() {
        _camera = GetComponent<Camera>();
    }

    public void AnimateFOV(AnimationSettings<float> animation){
        _camera.DOFieldOfView(animation.TargetValue, animation.Duration).SetEase(animation.Ease);
    }

    public void SetFOV(float fieldOfView){
        _camera.fieldOfView = fieldOfView;
    }

    public void AnimateRotation(AnimationSettings<Vector3> animation){
        cameraParent.DORotate(animation.TargetValue, animation.Duration).SetEase(animation.Ease);
    }

    public void SetRotation(Vector3 rotation){
        cameraParent.eulerAngles = rotation;
    }

    [System.Serializable]
    public class AnimationSettings<T>
    {
        public T StartValue;
        public T TargetValue;
        [Space]
        public float Duration;
        public Ease Ease;
    }
}