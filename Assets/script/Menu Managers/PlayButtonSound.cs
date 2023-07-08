using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public void PlaySound()
    {
        source.Play();
    }
}
