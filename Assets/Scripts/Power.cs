using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Power : MonoBehaviour
{
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public Sprite UpSprite;
    public Sprite DownSprite;

    public AudioClip PowerCreation;
    public AudioClip PowerAbsorption;

    public List<Tile> Path;
    public float Speed;

    public int CarriedPower = 10;

    private const float DestinationTolerance = 0.2f;

    private SpriteRenderer _renderer;

    private AudioSource SoundPlayer;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
        SoundPlayer = GetComponent<AudioSource>();

        SoundPlayer.PlayOneShot(PowerCreation);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Path.Any())
	    {
	        var nextTile = Path.Last();

	        if (nextTile == null || nextTile is Normal)
	        {
	            Destroy(gameObject);
	        } else if (nextTile is Road)
            {
                Road r = (Road) nextTile;
                Speed = r.Speed;
            }

	        var destination = nextTile.transform.position;
	        var direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y)
	            .normalized;
	        var directionRadians = Mathf.Atan2(direction.y, direction.x);

	        if (directionRadians >= 0 && directionRadians < Mathf.PI / 2f)
	        {
	            _renderer.sprite = RightSprite;
	        }
            else if (directionRadians >= Mathf.PI / 2f && directionRadians < Mathf.PI)
	        {
	            _renderer.sprite = UpSprite;
	        }
	        else if (directionRadians >= Mathf.PI && directionRadians < Mathf.PI * 3f / 2f)
	        {
	            _renderer.sprite = LeftSprite;
	        }
	        else
	        {
	            _renderer.sprite = DownSprite;
	        }

            transform.Translate(direction * Speed * Time.deltaTime);

	        if (Math.Abs(destination.x - transform.position.x) < DestinationTolerance &&
	            Math.Abs(destination.y - transform.position.y) < DestinationTolerance)
	        {
	            transform.position = new Vector3(destination.x, destination.y, transform.position.z);

	            if (Path.Count == 1)
	            {
	                var city = nextTile as City;

	                if (city != null)
	                {
	                    city.AddPower(CarriedPower);
                        SoundPlayer.PlayOneShot(PowerAbsorption);
                        _renderer.enabled = false;
	                    Destroy(gameObject, PowerAbsorption.length);
                    }
	            }

	            Path.Remove(nextTile);
	        }
	    }
	}
}
