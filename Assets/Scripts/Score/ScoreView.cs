using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [Header("ViewModel")]
    [SerializeField] private ScoreViewModel viewModel;

    [Header("Player")]
    [SerializeField] private TextMeshProUGUI playerPointsText;
    [SerializeField] private TextMeshProUGUI playerGamesText;
    
    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI enemyPointsText;
    [SerializeField] private TextMeshProUGUI enemyGamesText;

    void Start(){
        viewModel.PlayerScore.Changed += () => UpdateScoreTexts(viewModel.PlayerScore, playerPointsText, playerGamesText);
        viewModel.EnemyScore.Changed += () => UpdateScoreTexts(viewModel.EnemyScore, enemyPointsText, enemyGamesText);
    }

    private void UpdateScoreTexts(ScoreViewModel.Score score, TextMeshProUGUI pointsText, TextMeshProUGUI gamesText){
        pointsText.text = score.Points.ToString();
        gamesText.text = score.Games.ToString();
    }
}
