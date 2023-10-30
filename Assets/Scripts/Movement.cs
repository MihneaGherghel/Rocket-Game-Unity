using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource ad;

    float mainThurst = 900f;
    float rotationThurst = 50f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticule;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    void Start()
    {
        rb=GetComponent<Rigidbody>();
        ad=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThurst();
        ProcessRotation();
    }

    void ProcessThurst()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThursting();
        }
        else
        {
            ad.Stop();
            mainEngineParticule.Stop();
        }
    }

    private void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThurst);
        if (!ad.isPlaying)
        {
            ad.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticule.isPlaying)
        {
            mainEngineParticule.Play();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    private void RotateRight()
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * Time.deltaTime * rotationThurst);
        rb.freezeRotation = false;
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThurst);
        rb.freezeRotation = false;
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
}
