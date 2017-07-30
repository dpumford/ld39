using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Power : MonoBehaviour
{
    public List<Tile> Path;
    public float Speed;

    public int CarriedPower = 10;

    private const float DestinationTolerance = 0.2f;

	// Use this for initialization
	void Start () {
		
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
	                    Destroy(gameObject);
	                }
	            }

	            Path.Remove(nextTile);
	        }
	    }
	}
}
