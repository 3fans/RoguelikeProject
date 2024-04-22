using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Instance.OnEnterMainMenu();
    }

    public void OnPlayGame()
    {
        GameInstance.Instance.OnExitMainMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    public void OnVolumeChange(float volume)
    {
        GameInstance.Instance.SetVolume(volume);
    }
}
