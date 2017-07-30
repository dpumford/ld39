using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float DarknessPercent = 0.95f;

    public Sprite NormalSprite;
    public Sprite EnabledRoad;
    public Sprite DisabledRoad;
    public Sprite CitySprite;
    public Sprite GeneratorSprite;

    public Power PowerPrefab;

    protected Grid _grid;
    protected PowerSystem _powerSystem;
    protected SpriteRenderer _renderer;
    private bool _hovered;

	// Use this for initialization
	protected void Start ()
	{
	    _grid = FindObjectOfType<Grid>();
	    _powerSystem = FindObjectOfType<PowerSystem>();
	    _renderer = GetComponent<SpriteRenderer>();
	}

    protected void OnMouseOver()
    {
        _hovered = true;
    }

    protected void OnMouseExit()
    {
        _hovered = false;
    }

    // Update is called once per frame
    protected void Update ()
	{
        var darkness = _hovered ? 1 : Mathf.Clamp(DarknessPercent, 0f, 1f);
        _renderer.color = new Color(darkness, darkness, darkness);
	}

    public TTo ChangeType<TTo>(List<Tile> tiles) where TTo : Tile
    {
        var newScript = gameObject.AddComponent<TTo>();

        //TODO: On god why
        newScript.NormalSprite = NormalSprite;
        newScript.EnabledRoad = EnabledRoad;
        newScript.DisabledRoad = DisabledRoad;
        newScript.CitySprite = CitySprite;
        newScript.GeneratorSprite = GeneratorSprite;

        newScript.PowerPrefab = PowerPrefab;

        var temp = Grid.Index(tiles, this);
        tiles[temp] = newScript;

        Destroy(this);

        return newScript;
    }
}
