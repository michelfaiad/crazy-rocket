using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBehaviour : MonoBehaviour
{
    [Header("Object References")]
    [Tooltip("Credits Canvas")]
    [SerializeField] GameObject creditsPanel;

    float delay = 0.5f;

    public void StartGame()
    {
        LivesController.inst.ResetLives();
        FadeController.inst.FadeToBlack();
        Invoke("LoadFirstLevel", delay);
    }

    public void TitleScreen()
    {
        FadeController.inst.FadeToBlack();
        Invoke("LoadTitleScreen", delay);
    }

    public void Credits(bool show)
    {
        creditsPanel.SetActive(show);
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    void LoadTitleScreen()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
