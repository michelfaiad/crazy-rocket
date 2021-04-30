using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController inst;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void GameOver()
    {
        FadeController.inst.FadeToBlack();
        Invoke("LoadGameOverScreen", .5f);

    }

    void LoadGameOverScreen()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
