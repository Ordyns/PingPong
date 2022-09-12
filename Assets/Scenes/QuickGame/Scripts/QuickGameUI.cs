using UnityEngine;

public class QuickGameUI : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;

    public void Init(ScoreViewModel scoreViewModel){
        scoreView.Init(scoreViewModel);
    }
}
