using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class DisasterSystem : MonoBehaviour
{
    public int NumberOfMeteors = 3;
    public int MeteorShowerDiameter = 3;

    public int ConstrainedMeteorShowerDiameter
    {
        get
        {
            if (_grid != null)
            {
                return Mathf.Clamp(MeteorShowerDiameter, 0, _grid.Size);
            }
            else
            {
                return MeteorShowerDiameter;
            }
        }
    }

    public float MeteorShowerSeconds = 60;

    private Grid _grid;
    private float _meteorShowerTimer;

	// Use this for initialization
	void Start ()
	{
	    _grid = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_meteorShowerTimer > MeteorShowerSeconds)
	    {
	        DropMeteors();

	        _meteorShowerTimer = 0;
	    }
	    else
	    {
	        _meteorShowerTimer += Time.deltaTime;
	    }
	}

    private void DropMeteors()
    {
        var targets = new List<Target>(NumberOfMeteors);
        var constrainedDiameter = ConstrainedMeteorShowerDiameter;
        var gridSize = _grid.Size;

        for (var t = 0; t < NumberOfMeteors; t++)
        {
            Target newTarget;

            do
            {
                newTarget = new Target(Random.Range(0, constrainedDiameter), Random.Range(0, constrainedDiameter));
            } while (targets.Any(target => target.Equals(newTarget)));

            targets.Add(newTarget);
        }

        Target start = new Target(Random.Range(0, gridSize - constrainedDiameter), Random.Range(0, gridSize - constrainedDiameter));

        foreach (var target in targets)
        {
            var finalTarget = target + start;

            //TODO: Wrap this method.
            var tile = Grid.Get(finalTarget.X, finalTarget.Y, _grid.Tiles, gridSize) as Road;

            if (tile != null)
            {
                tile.ChangeType<Normal>(_grid.Tiles);
            }
        }
    }

    private class Target
    {
        public readonly int X;
        public readonly int Y;

        public Target(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Target;

            if (other == null)
            {
                return false;
            }
            else
            {
                return other.X == X && other.Y == Y;
            }
        }

        public static Target operator +(Target a, Target b)
        {
            return new Target(a.X + b.X, a.Y + b.Y);
        }
    }
}
