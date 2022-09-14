using UnityEngine;

public class OverlayTitleFactory : MonoBehaviour
{
    public string GetTitle(RoundEndReason roundEndReason){
        switch(roundEndReason){
            case RoundEndReason.BallHitNet: return "Net!";
            case RoundEndReason.UnsuccessfulServe: return "Bad serve";
        }

        return string.Empty;
    }
}