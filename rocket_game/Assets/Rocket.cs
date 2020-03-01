using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float leavelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidbody;
    AudioSource audioSource;
    enum State { Alive, Dying, Transcending}
    State state = State.Alive;

    bool collisionsDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToThrust();
            RespondToRotateInput();
        }

        RespondToDebug();
        
    }

    private void RespondToDebug()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled)
        {
            return;
        }
           
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence(); 
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        successParticles.Play();
        Invoke("LoadFirstLevel", leavelLoadDelay);
    }

    private void StartSuccessSequence() 
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        deathParticles.Play();
        Invoke("LoadNextLevel", leavelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true;
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {   
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);

        }
        rigidbody.freezeRotation = false;
    }

    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();

        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }
}
