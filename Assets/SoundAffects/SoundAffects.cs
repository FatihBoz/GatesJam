using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundAffects : MonoBehaviour
{
    public AudioClip winFem;
    public AudioClip winMale;
    public AudioClip loseFem;
    public AudioClip loseMale;


    [SerializeField] private List<AudioClip> maleTalks; // Listeyi Inspector üzerinden ekleyeceksiniz


}
