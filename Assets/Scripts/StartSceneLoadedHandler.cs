using UnityEngine;

public class StartSceneLoadedHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] dontDestroyOnLoadGameObjects;

    private void Start() {
        foreach(GameObject gameObject in dontDestroyOnLoadGameObjects)
            DontDestroyOnLoad(gameObject);

        ScenesLoader.LoadMainMenu();
    }
}
