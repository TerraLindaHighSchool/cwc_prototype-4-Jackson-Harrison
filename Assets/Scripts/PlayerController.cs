using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public bool hasPowerUp = false;
    public GameObject powerupIndicator;
    public bool devMode;
    public AudioSource bounceSound;
    private Rigidbody playerRb;
    private float powerupStrength = 15;
    private GameObject focalPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //I'm going to remove this after you've graded
        if (Input.GetKeyDown("p"))
        {
            devMode = true;
            Debug.Log("p");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            Debug.Log("Picked Up P");
            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());

        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    //Applifies force when colliding with 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            bounceSound.Play();
        }

        if (devMode == true)
        {
            Destroy(collision.gameObject);
        }

        else if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + "with powerup set to " + hasPowerUp);

        }
    }


}
