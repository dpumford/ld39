using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Tile[] Tiles;

	// Use this for initialization
	void Start ()
	{
        //TODO: Generate these based on size
	    //GenerateTiles();
	}

    private void GenerateTiles()
    {
        var gridBounds = gameObject.GetComponent<Collider2D>().bounds;

        for (var y = 0; y < Tiles.Length; y++)
        {
            for (var x = 0; x < Tiles.Length; x++)
            {
                //TODO: Get the math right
            }
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
