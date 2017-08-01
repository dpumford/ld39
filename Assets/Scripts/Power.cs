using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Power : MonoBehaviour
{
    public Sprite[] HorizontalSprites;
    public Sprite[] VerticalSprites;

    public float AnimationSeconds = 0.1f;

    public AudioClip PowerCreation;
    public AudioClip PowerAbsorption;
    public AudioClip PowerDestroy;

    public List<Tile> Path;
    public float Speed;

    public int CarriedPower = 10;

    private const float DestinationTolerance = 0.25f;

    private SpriteRenderer _renderer;
    private AudioSource _soundPlayer;

    private int _currentSprite;
    private float _animationTimer;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
        _soundPlayer = GetComponent<AudioSource>();

        _soundPlayer.PlayOneShot(PowerCreation);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Path.Any())
	    {
	        var nextTile = Path.Last();

	        if (nextTile == null || nextTile is Normal)
	        {
	            _soundPlayer.PlayOneShot(PowerDestroy);
	            _renderer.enabled = false;
	            Destroy(gameObject, PowerDestroy.length);
	        }
	        else
	        {
	            var road = nextTile as Road;

	            if (road != null)
	            {
	                Speed = road.Speed;
	            }

	            var destination = nextTile.transform.position;
	            Vector2 direction = RotateTo(destination);

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

	                        _soundPlayer.PlayOneShot(PowerAbsorption);
	                        _renderer.enabled = false;
	                        Destroy(gameObject, PowerAbsorption.length);
	                    }
	                }

	                Path.Remove(nextTile);
	            }
	        }
	    }

	    if (_animationTimer > AnimationSeconds)
	    {
	        _currentSprite = (_currentSprite + 1) % Math.Min(HorizontalSprites.Length, VerticalSprites.Length);
	        _animationTimer = 0;
	    }
	    else
	    {
	        _animationTimer += Time.deltaTime;
	    }
	}

    private Vector2 RotateTo(Vector3 destination)
    {
        var direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y)
                        .normalized;
        var directionRadians = Mathf.Atan2(direction.y, direction.x);

        if ((directionRadians >= 0 && directionRadians < Mathf.PI / 2f) || (directionRadians >= Mathf.PI && directionRadians < Mathf.PI * 3f / 2f))
        {
            _renderer.sprite = HorizontalSprites[_currentSprite];
        }
        else
        {
            _renderer.sprite = VerticalSprites[_currentSprite];
        }

        return direction;
    }
}
