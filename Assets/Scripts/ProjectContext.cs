using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public static ProjectContext Instance { get; private set; }

    public QuickGameSettingsContainer QuickGameSettingsContainer { get; private set; }

    private void Awake() {
        QuickGameSettingsContainer = new QuickGameSettingsContainer();

        Instance = this;
    }
}
