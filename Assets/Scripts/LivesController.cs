using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    public static LivesController inst;

    [Header("Lives Configuration")]
    [Tooltip("Max number of lives the player could have")]
    [SerializeField] int livesMax;
    [Header("Object References")]
    [Tooltip("Lives Icons Array")]
    [SerializeField] Image[] livesIcons;
    [Tooltip("Lives Canvas")]
    [SerializeField] GameObject livesCanvas;

    int lives;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {        
        if (scene.buildIndex < 2)
        {
            livesCanvas.SetActive(false);
        }
        else
        {
            livesCanvas.SetActive(true);
            UpdateLifeBar();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

        lives = livesMax;
        UpdateLifeBar();
    }

    public int GetLives()
    {
        return lives;
    }

    public void SubtractLives(int decrement)
    {
        lives -= decrement;
        UpdateLifeBar();
        CheckGameOver();
    }

    public void AddLives(int increment)
    {
        lives += increment;
        if (lives > livesMax)
        {
            lives = livesMax;
        }
        UpdateLifeBar();
    }

    public void ResetLives()
    {
        lives = livesMax;
    }

    private void UpdateLifeBar()
    {
        int i = 0;
        foreach(Image img in livesIcons)
        {
            if (++i <= lives)
            {
                img.enabled = true;                
            } else
            {
                img.enabled = false;
            }
        }
    }

    private void CheckGameOver()
    {
        if (lives <= 0)
        {
            GameController.inst.GameOver();
        }
    }

}
