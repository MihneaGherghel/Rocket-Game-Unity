using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collison : MonoBehaviour
{

    [SerializeField] AudioClip obstacle;
    [SerializeField] AudioClip finish;
    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ParticleSystem obstacleParticle;

    AudioSource ad;

    float levelLoadDelay = 1f;
    bool isTransitioning;

    private void Start()
    {
        ad= GetComponent<AudioSource>();
        isTransitioning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend");
                break;

            case "Finish":
                Debug.Log("Finish");
                StartSuccessSequence();
                break;

            default:
                Debug.Log("Blew up!");
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        finishParticle.Play();
        isTransitioning = true;
        ad.PlayOneShot(finish);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        obstacleParticle.Play();
        isTransitioning = true;
        ad.PlayOneShot(obstacle);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        ad.PlayOneShot(obstacle);
    }
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        if(nextSceneIndex-1==SceneManager.sceneCount)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
