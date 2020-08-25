using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFX : MonoBehaviour
{
    private AudioSource soundFX;

    [SerializeField]
    private AudioClip attack_Sound, die_Sound;

    void Awake()
    {
        soundFX = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        soundFX.clip = attack_Sound;
        soundFX.Play();
    }

    public void Die()
    {
        soundFX.clip = die_Sound;
        soundFX.Play();
    }
    
    
}
