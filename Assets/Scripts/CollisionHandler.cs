using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelloadDelay = 1.5f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Just getting started.");
                break;
            case "Finish":
                StartLandingSequence();
                break;
            case "Fuel":
                Debug.Log("num num num");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartLandingSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelloadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
    }

     void StartCrashSequence()
    {
        // To-Do add particle effect upon crash
        isTransitioning = true;
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelloadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void SkipLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

}
