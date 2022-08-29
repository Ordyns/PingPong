using System;

public class ScoreViewModel : ViewModel
{
    public event Action MatchFinished;

    public Score PlayerScore { get; private set; } = new Score();
    public Score EnemyScore { get; private set; } = new Score();

    public bool Overtime { get; private set; }
    
    public void AddPointToPlayer() {
        PlayerScore.Points.Value++;
        CalculateScore(PlayerScore);
    }

    public void AddPointToEnemy(){
        EnemyScore.Points.Value++;
        CalculateScore(EnemyScore);
    }

    private void CalculateScore(Score score){
        if(PlayerScore.Points == 10 && EnemyScore.Points == 10)
            Overtime = true;

        bool isOvertimeFinished = Overtime && Math.Abs(PlayerScore.Points.Value - EnemyScore.Points.Value) >= 2;
        if((score.Points == 11 && Overtime == false) || isOvertimeFinished){
            PlayerScore.Points.Value = EnemyScore.Points.Value = 0;

            score.Games.Value++;

            if(score.Games == 3)
                MatchFinished?.Invoke();
        }
    }

    public class Score
    {
        public event Action Changed;

        public ObservableProperty<int> Points = new ObservableProperty<int>();
        public ObservableProperty<int> Games = new ObservableProperty<int>();

        public Score(){
            Points.Changed += () => Changed?.Invoke();
            Games.Changed += () => Changed?.Invoke();
        }
    }
}