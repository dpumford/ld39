using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioSource clickSound;
    public AudioSource denyClickSound;

    public void PlayClickSound()
    {
        clickSound.Play();
    }

    public void DenyClickSound()
    {
        denyClickSound.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
