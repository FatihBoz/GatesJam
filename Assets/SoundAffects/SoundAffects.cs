using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundAffects : MonoBehaviour
{
    public static SoundAffects Instance { get; private set; }


    public AudioClip winFem;
    public AudioClip winMale;
    public AudioClip loseFem;
    public AudioClip loseMale;


    [SerializeField] private List<AudioClip> maleTalks; // Listeyi Inspector üzerinden ekleyeceksiniz
    [SerializeField] private AudioSource source;


    private void Awake()
    {
        Instance = this;
    }


    public void PlayFemaleWinSF()
    {
        source.PlayOneShot(winFem);
    }


    public void PlayFemaleLoseSF()
    {
        source.PlayOneShot(loseFem);
    }

    public void PlayMaleWinSF()
    {
        source.PlayOneShot(winMale);
    }

    public void PlayMaleLoseSF()
    {
        source.PlayOneShot(loseMale);
    }

    public void PlayMaleTalkSF()
    {
        source.PlayOneShot(maleTalks[Random.Range(0, maleTalks.Count)]);
    }

}
