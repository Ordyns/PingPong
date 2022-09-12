using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TextMeshProUGUI playerPointsText;
    [SerializeField] private TextMeshProUGUI playerGamesText;
    
    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI enemyPointsText;
    [SerializeField] private TextMeshProUGUI enemyGamesText;

    private ScoreViewModel _viewModel;

    public void Init(ScoreViewModel scoreViewModel){
        _viewModel = scoreViewModel;
        _viewModel.PlayerScore.Changed += () => UpdateScoreTexts(_viewModel.PlayerScore, playerPointsText, playerGamesText);
        _viewModel.EnemyScore.Changed += () => UpdateScoreTexts(_viewModel.EnemyScore, enemyPointsText, enemyGamesText);
    }

    private void UpdateScoreTexts(ScoreViewModel.Score score, TextMeshProUGUI pointsText, TextMeshProUGUI gamesText){
        pointsText.text = score.Points.ToString();
        gamesText.text = score.Games.ToString();
    }
}
