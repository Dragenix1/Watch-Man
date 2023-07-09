using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] searchSound;
    [SerializeField] private float minSound = 0.1f;
    [SerializeField] private float maxSound = 0.8f;
    [SerializeField] private float chanceToPlay = 0.33f;


    public void PlaySearchSound()
    {
        float chance = Random.Range(0.0f, 1.1f);

        if (chance < chanceToPlay)
        {
            float volume = Random.Range(minSound, maxSound);
            int ind = Random.Range(0, searchSound.Length);
            AudioSource.PlayClipAtPoint(searchSound[ind], gameObject.transform.position);
        }
    }
}
