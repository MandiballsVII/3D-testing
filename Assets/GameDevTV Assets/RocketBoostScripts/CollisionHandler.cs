using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CinemachineImpulseSource))]
[RequireComponent(typeof(AudioSource))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 2.0f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    public event Action OnPlayerDie;
    public event Action OnFinishLevel;

    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
        Debug.Assert(impulseSource != null,
        "CollisionHandler requiere CinemachineImpulseSource",
        this);
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNewScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isControllable || !isCollidable) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly": print("You get a friendly place");
                break;
            case "Finish": print("Congratulations, you finished the level!!");
                StartSuccessSequence();
                break;
            case "Fuel": print("Fuel cached!!");
                break;
            default: print("You crashed!!");
                StartCrashSequence();               
                break;
        }
    }

    private void StartSuccessSequence()
    {
        OnFinishLevel?.Invoke();
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNewScene), crashDelay);
    }

    private void StartCrashSequence()
    {
        OnPlayerDie?.Invoke();

        if (impulseSource != null)
            impulseSource.GenerateImpulse();

        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadScene), crashDelay);
    }

    void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void LoadNewScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
