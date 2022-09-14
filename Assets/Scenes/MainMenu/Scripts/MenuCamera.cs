using UnityEngine;
using static MenuCameraAnimator;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private MenuCameraAnimator animator;

    [Header("Animations")]
    [SerializeField] private CompositeAnimation startAnimation;
    [Space]
    [SerializeField] private CompositeAnimation difficultySelectionAnimation;

    private void Awake() {
        animator.SetFOV(startAnimation.FovAnimation.StartValue);
        animator.SetRotation(startAnimation.RotationAnimation.StartValue);
    }

    private void Start() {
        MoveToDefault();
    }

    public void MoveToDefault(){
        PlayCompositeAnimation(startAnimation);
    }

    public void MoveToDifficultySelection(){
        PlayCompositeAnimation(difficultySelectionAnimation);
    }

    private void PlayCompositeAnimation(CompositeAnimation animation){
        animator.AnimateFOV(animation.FovAnimation);
        animator.AnimateRotation(animation.RotationAnimation);
    }

    [System.Serializable]
    private class CompositeAnimation
    {
        public AnimationSettings<float> FovAnimation;
        public AnimationSettings<Vector3> RotationAnimation;
    }
}
