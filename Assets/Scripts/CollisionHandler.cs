using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    float delay = 0.5f;

    [Header("Object References")]
    [Tooltip("Audio Clips")]
    [SerializeField] AudioClip success, crash;
    [Tooltip("Particles")]
    [SerializeField] ParticleSystem successParticle, crashParticle;
    [Tooltip("Audio Source")]
    [SerializeField] AudioSource audioSource;

    bool isTransitioning = false;

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                StartFinishSequence();
                break;            
            default:
                StartCrashSequence();
                break;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Fuel":
                Destroy(other.gameObject);
                LivesController.inst.AddLives(1);
                break;
            default:                
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        FadeController.inst.FadeToBlack();
        LivesController.inst.SubtractLives(1);
        if (LivesController.inst.GetLives() > 0)
        {
            Invoke("ReloadScene", delay);
        }
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        FadeController.inst.FadeToBlack();
        Invoke("LoadNextLevel", delay);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 2;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
