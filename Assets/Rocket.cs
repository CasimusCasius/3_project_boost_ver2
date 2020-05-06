using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float throttleSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip dyingSFX;
    [SerializeField] AudioClip winningSFX;
    [SerializeField] ParticleSystem engineVFX;
    [SerializeField] ParticleSystem dyingVFX;
    [SerializeField] ParticleSystem winningVFX;



    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrottle();
            RespondToRotate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        { 
            return; 
        }
        else
        {
            audioSource.Stop();
            
        }


        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                Win();

                break;
            default:
                Die();
                break;
        }
    }

    private void Die()
    {
        state = State.Dying;
        audioSource.PlayOneShot(dyingSFX);
        dyingVFX.Play();
        Invoke("LoadFirstScene", 1f);
    }

    private void Win()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(winningSFX);
        winningVFX.Play();
        Invoke("LoadNextScene", 1f); // TODO parametrise time
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToRotate()
    {
        float rotationPerFrame = rotationSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationPerFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void RespondToThrottle()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Throttle();
        }
        else
        {
            audioSource.Stop();
            engineVFX.Stop();
        }
    }

    private void Throttle()
    {
        rigidBody.AddRelativeForce(Vector3.up * throttleSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        engineVFX.Play();
    }
}