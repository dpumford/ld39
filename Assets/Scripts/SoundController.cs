using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioClip clickSound;
    public AudioClip denyClickSound;

    private AudioSource _soundPlayer;

    public void PlayClickSound()
    {
        _soundPlayer.PlayOneShot(clickSound);
    }

    public void DenyClickSound()
    {
        _soundPlayer.PlayOneShot(denyClickSound);
    }

	// Use this for initialization
	void Start ()
	{
	    _soundPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
