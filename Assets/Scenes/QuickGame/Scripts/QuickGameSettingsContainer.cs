using UnityEngine;

public class QuickGameSettingsContainer : MonoBehaviour
{
    public QuickGameSettings QuickGameSettings { get; private set; }

    public void SetQuickGameSettings(QuickGameSettings quickGameSettings){
        QuickGameSettings = quickGameSettings;
    }
}
