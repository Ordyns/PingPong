using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDifficultyFactory : MonoBehaviour
{
    [SerializeField] private BotDifficulty easyDifficulty;
    [SerializeField] private BotDifficulty normalDifficulty;
    [SerializeField] private BotDifficulty hardDifficulty;

    public BotDifficulty Get(QuickGameDifficulty quickGameDifficulty){
        switch(quickGameDifficulty){
            case QuickGameDifficulty.Easy: return easyDifficulty;
            case QuickGameDifficulty.Normal: return normalDifficulty;
            case QuickGameDifficulty.Hard: return hardDifficulty;
        }

        throw new System.NotImplementedException();
    }
}
