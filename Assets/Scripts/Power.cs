using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Power : MonoBehaviour
{
    public List<Tile> Path;
    public float Speed;

    private const float DestinationTolerance = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Path.Any())
	    {
	        var nextTile = Path.Last();

	        if (nextTile.Type == Tile.TileType.Normal)
	        {
	            Destroy(gameObject);
	        }

	        var destination = nextTile.transform.position;
	        var direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y)
	            .normalized;

	        transform.Translate(direction * Speed * Time.deltaTime);

	        if (Math.Abs(destination.x - transform.position.x) < DestinationTolerance &&
	            Math.Abs(destination.y - transform.position.y) < DestinationTolerance)
	        {
	            transform.position = new Vector3(destination.x, destination.y, transform.position.z);

	            Path.Remove(nextTile);
	        }
	    }
	    else
	    {
            //TODO: Transmit power
	        Destroy(gameObject);
	    }
	}
}
