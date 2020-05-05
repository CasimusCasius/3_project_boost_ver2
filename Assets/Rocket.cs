using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float throttleSpeed = 100f;


    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

    Rigidbody rigidBody;
    AudioSource throttleSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        throttleSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Throttle();
            Rotate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":
                Invoke("LoadNextScene", 1f); // TODO parametrise time
                state = State.Transcending;
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstScene", 1f);
                break;
        }
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Rotate()
    {
        float rotationPerFrame = rotationSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationPerFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void Throttle()
    {
        float throttlePerFrame = throttleSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*throttlePerFrame);
            if (!throttleSound.isPlaying)
            {
                throttleSound.Play();
            }
        }
        else
        {
            throttleSound.Stop();
        }
    }
}

    
