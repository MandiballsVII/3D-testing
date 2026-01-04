using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] int thrustStrength = 1000;
    [SerializeField] float rotationStrenght = 1.2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void OnDisable()
    {
        thrust.Disable();
        rotation.Disable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!thrustParticles.isPlaying)
                thrustParticles.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                thrustParticles.Stop();
                audioSource.Stop();
            }
        }
    }
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            leftParticles.Stop();
            ApplyRotation(rotationStrenght);
            if(!rightParticles.isPlaying)
                rightParticles.Play();
        }
        else if(rotationInput > 0)
        {
            rightParticles.Stop();
            ApplyRotation(-rotationStrenght);
            if(!leftParticles.isPlaying)
                leftParticles.Play();
        }
        else
        {
            leftParticles.Stop();
            rightParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
