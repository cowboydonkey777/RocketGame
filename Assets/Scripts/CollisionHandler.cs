using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;


    AudioSource rocketAudio;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
        rocketAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RocketGameDebugKeys();
    }

    private void RocketGameDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartSuccessSequence();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Welcome home");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(crashSound, 0.7f);
        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }

    void StartSuccessSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(victorySound);

        victoryParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber);
    }

    void LoadNextLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;

        if (sceneNumber == SceneManager.sceneCountInBuildSettings)
        {
            sceneNumber = 0;
        }

        SceneManager.LoadScene(sceneNumber);
    }
}
