using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem CrashParticles;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] float respawnDelay = 1f;

    AudioSource audioSource;
    ParticleSystem particleSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Don't go backwards!");
                break;
            case "Finish":
                NextLevelSequence();
                //NextLevel();
                break;
            default:
                StartCrashSequence();
                //ReloadLevel();
                break;

        }
    }



    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        CrashParticles.Play();
        audioSource.PlayOneShot(Crash);
        Invoke("ReloadLevel", respawnDelay);
        
    }


    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        SuccessParticles.Play();
        audioSource.PlayOneShot(Success);
        Invoke("NextLevel", respawnDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
