using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    public static LevelContext Instance { get; private set; }

    public ScoreManager ScoreManager { get; private set; }
    [field:SerializeField] public ScoreViewModel ScoreViewModel {get; private set; }

    [field:SerializeField] public ServeManager ServeManager;

    private void Awake() {
        ScoreManager = new ScoreManager(ScoreViewModel, ServeManager);

        Instance = this;
    }
}
