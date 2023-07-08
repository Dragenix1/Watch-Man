using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip audioClip;
    public float stepLength = 0.4f;
    public float volume = 0.7f;

    private CharacterController characterController;
    private float delay = 0.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.velocity.sqrMagnitude > 0.2f && characterController.isGrounded)
        {
            if (delay >= stepLength)
            {
                float audioVolume = Random.Range(volume - 0.1f, volume + 0.11f);
                AudioSource.PlayClipAtPoint(audioClip, transform.position, audioVolume);
                delay = 0;
            }
        }
        delay += Time.deltaTime;
    }
}
