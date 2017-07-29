using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Assumes that the grid is square
    public List<Tile> Tiles;

    public int Size
    {
        get { return Convert.ToInt32(Math.Sqrt(Tiles.Count)); }
    }

    // Use this for initialization
	void Start ()
	{
        //TODO: Generate these based on size
	    //GenerateTiles();
	}

    public Tile GetRelative(Tile context, int dX, int dY)
    {
        var tileIndex = Tiles.FindIndex(tile => tile == context);
        return Get(XFromIndex(tileIndex) + dX, YFromIndex(tileIndex) + dY);
    }

    public Tile Get(int x, int y)
    {
        if (x < 0 || x > Size || y < 0 || y > Size)
        {
            return null;
        }

        return Tiles[x + y * Size];
    }

    public int XFromIndex(int index)
    {
        return index % Size;
    }

    public int YFromIndex(int index)
    {
        return index / Size;
    }

    private void GenerateTiles()
    {
        var gridBounds = gameObject.GetComponent<Collider2D>().bounds;

        for (var y = 0; y < Tiles.Count; y++)
        {
            for (var x = 0; x < Tiles.Count; x++)
            {
                //TODO: Get the math right
            }
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
