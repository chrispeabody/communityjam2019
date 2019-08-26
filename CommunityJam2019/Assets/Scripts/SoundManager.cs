using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSoundCollection(SoundCollection soundCollection) {
    	int randomIndex = Random.Range(0, soundCollection.sounds.Count);
    	_audioSource.clip = soundCollection.sounds[randomIndex];
    	_audioSource.volume = soundCollection.volume;

    	_audioSource.pitch = Random.Range(soundCollection.minPitch, soundCollection.maxPitch);
    	_audioSource.Play();
    }
}
