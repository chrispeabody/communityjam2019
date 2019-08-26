using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundCollection")]
public class SoundCollection : ScriptableObject {
    [SerializeField] public List<AudioClip> sounds;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public float volume = 1;
}
