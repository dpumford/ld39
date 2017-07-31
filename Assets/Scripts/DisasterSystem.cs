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

    public ImpendingMeteor ImpendingMeteorPrefab;

    private Grid _grid;
    private float _meteorShowerTimer;

    private List<ImpendingMeteor> _impendingMeteors;

	// Use this for initialization
	void Start ()
	{
	    _grid = GetComponent<Grid>();
        _impendingMeteors = new List<ImpendingMeteor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_meteorShowerTimer > MeteorShowerSeconds * MeteorShowerPredictionPercent && !_impendingMeteors.Any())
	    {
	        var meteorsToDrop = PickTargets();

	        for (var m = 0; m < meteorsToDrop.Count; m++)
	        {
	            var target = Grid.Get(meteorsToDrop[m].X, meteorsToDrop[m].Y, _grid.Tiles, _grid.Size);

	            var impendingMeteor = Instantiate(ImpendingMeteorPrefab, new Vector3(target.transform.position.x, target.transform.position.y, -1), Quaternion.identity);
	            impendingMeteor.SecondsToImpact = (MeteorShowerSeconds - MeteorShowerSeconds * MeteorShowerPredictionPercent) + IndividualMeteorSeconds * m;
	            impendingMeteor.Target = target;

	            _impendingMeteors.Add(impendingMeteor);
	        }
        }
        else if (_meteorShowerTimer > MeteorShowerSeconds)
	    {
            _impendingMeteors.Clear();
	        _meteorShowerTimer = 0;
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
                newTarget = new Target(Random.Range(0, constrainedDiameter), Random.Range(0, constrainedDiameter)) + start;
            } while (targets.Any(target => target.Equals(newTarget)));

            targets.Add(newTarget);
        }

        return targets;
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
