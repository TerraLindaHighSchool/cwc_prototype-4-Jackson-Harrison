using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    public float rotationSpeed;
    public AudioSource musicSound;

    // Start is called before the first frame update
    void Start()
    {
        musicSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

}
