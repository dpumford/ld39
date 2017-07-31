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

    public float MeteorShowerSeconds = 3;
    public float MeteorShowerPredictionPercent = 0.5f;
    public float IndividualMeteorSeconds = 0.5f;

    private Grid _grid;
    private float _meteorShowerTimer;

    private List<Target> _meteorsToDrop;
    private float _individualMeteorTimer;

	// Use this for initialization
	void Start ()
	{
	    _grid = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_meteorShowerTimer > MeteorShowerSeconds * MeteorShowerPredictionPercent)
	    {
	        if (_meteorsToDrop == null)
	        {
	            _meteorsToDrop = PickTargets();
	        }
        }

        if (_meteorShowerTimer > MeteorShowerSeconds)
	    {
	        if (_meteorsToDrop.Count > 0)
	        {
	            if (_individualMeteorTimer > IndividualMeteorSeconds)
	            {
	                DropMeteor();
	                _individualMeteorTimer = 0;
	            }
	            else
	            {
	                _individualMeteorTimer += Time.deltaTime;
	            }
	        }
	        else
	        {
	            _meteorsToDrop = null;
	            _meteorShowerTimer = 0;
	        }
	    }
	    else
	    {
	        _meteorShowerTimer += Time.deltaTime;
	    }
	}

    private List<Target> PickTargets()
    {
        var targets = new List<Target>(NumberOfMeteors);
        var constrainedDiameter = ConstrainedMeteorShowerDiameter;
        var gridSize = _grid.Size;

        Target start = new Target(Random.Range(0, gridSize - constrainedDiameter), Random.Range(0, gridSize - constrainedDiameter));

        for (var t = 0; t < NumberOfMeteors; t++)
        {
            Target newTarget;

            do
            {
                newTarget = new Target(Random.Range(0, constrainedDiameter), Random.Range(0, constrainedDiameter));
            } while (targets.Any(target => target.Equals(newTarget)));

            targets.Add(newTarget + start);
        }

        return targets;
    }

    private void DropMeteor()
    {
        var finalTarget = _meteorsToDrop.First();

        //TODO: Wrap this method.
        var tile = Grid.Get(finalTarget.X, finalTarget.Y, _grid.Tiles, _grid.Size) as Road;

        if (tile != null)
        {
            tile.MeteorHit();
        }

        _meteorsToDrop.Remove(finalTarget);
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
