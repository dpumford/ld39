using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Assets.Helpers;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Assumes that the grid is square
    public List<Tile> Tiles;

    public ReadOnlyCollection<List<Tile>> ValidConnections {
        get { return _validConnections.AsReadOnly(); }
    }

private List<List<Tile>> _validConnections;

    // Use this for initialization
	void Start ()
	{
        //TODO: Generate tiles based on size

        _validConnections = new List<List<Tile>>();
	}

    public bool IsTileConnected(Tile tile)
    {
        return _validConnections.Any(connection => connection.Contains(tile));
    }

    //TODO make this not assume the size.
    private static List<Tile> GetNeighbors(List<Tile> tiles, Tile tile)
    {
        var neighbors = new List<Tile>();
        var size = SquareSize(tiles);

        for (var dX = -1; dX <= 1; dX++)
        {
            for (var dY = -1; dY <= 1; dY++)
            {
                if (Math.Abs(dX) != Math.Abs(dY))
                {
                    var tileIndex = Index(tiles, tile);
                    var tileX = XFromIndex(tileIndex, size);
                    var tileY = YFromIndex(tileIndex, size);

                    var neighbor = Get(tileX + dX, tileY + dY, tiles, size);


                    //TODO: Allow other types, too
                    if (neighbor != null && neighbor.Type != Tile.TileType.Normal)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }
        }

        return neighbors;
    }

    private static int Heuristic(List<Tile> space, Tile tileA, Tile tileB)
    {
        var size = SquareSize(space);

        var indexA = Index(space, tileA);
        var indexB = Index(space, tileB);

        var xA = XFromIndex(indexA, size);
        var yA = XFromIndex(indexA, size);

        var xB = XFromIndex(indexB, size);
        var yB = XFromIndex(indexB, size);

        return Math.Abs(xA - xB) + Math.Abs(yA - yB);
    }

    private static int SquareSize(List<Tile> tiles)
    {
        return Convert.ToInt32(Math.Sqrt(tiles.Count));
    }

    public static Tile Get(int x, int y, IList<Tile> tiles, int size)
    {
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            return null;
        }

        return tiles[x + y * size];
    }

    public static int Index(List<Tile> space, Tile tile)
    {
        return space.FindIndex(t => t == tile);
    }

    public static int XFromIndex(int index, int size)
    {
        return index % size;
    }

    public static int YFromIndex(int index, int size)
    {
        return index / size;
    }

    // Update is called once per frame
	void Update ()
	{
	    _validConnections.Clear();

        var cities = Tiles.Where(t => t.Type == Tile.TileType.City).ToList();
	    var generators = Tiles.Where(t => t.Type == Tile.TileType.Generator).ToList();

	    foreach (var generator in generators)
	    {
	        foreach (var city in cities)
	        {
	            List<Tile> possiblePath;
	            List<Tile> excludeTiles = new List<Tile>();

	            do
	            {
	                possiblePath = Pathfinder.AStar(Tiles, generator, city, Heuristic, GetNeighbors, excludeTiles);

	                if (possiblePath != null)
	                {
	                    _validConnections.Add(possiblePath);
                        excludeTiles.AddRange(possiblePath.GetRange(1, possiblePath.Count - 1));
	                }
	            } while (possiblePath != null);
	        }
	    }
    }
}
