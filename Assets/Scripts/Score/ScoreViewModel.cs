using System;
using MVVM;

public class ScoreViewModel : ViewModel
{
    public event Action MatchFinished;
    public event Action<RoundEndReason> RoundEnded;

    public Score PlayerScore { get; private set; } = new Score();
    public Score EnemyScore { get; private set; } = new Score();

    public bool IsOvertime { get; private set; }

    public void AddPointTo(Racket.Owner racketOwner){
        Score score = racketOwner == Racket.Owner.Player ? PlayerScore : EnemyScore;
        score.Points.Value++;
        CalculateScore(score);
    }

    private void CalculateScore(Score score){
        if(PlayerScore.Points == 10 && EnemyScore.Points == 10)
            IsOvertime = true;

        bool isOvertimeFinished = IsOvertime && Math.Abs(PlayerScore.Points.Value - EnemyScore.Points.Value) >= 2;
        if((score.Points == 11 && IsOvertime == false) || isOvertimeFinished){
            PlayerScore.Points.Value = EnemyScore.Points.Value = 0;

            score.Games.Value++;

            if(score.Games == 3)
                MatchFinished?.Invoke();
        }
    }

    public void OnRoundEnded(RoundEndReason roundEndReason) => RoundEnded?.Invoke(roundEndReason);

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