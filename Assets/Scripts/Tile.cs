using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Transport,
        City,
        Generator
    }

    public TileType Type = TileType.Normal;
    public bool Enabled = true;

    public Sprite Normal;
    public Sprite CitySprite;
    public Sprite GeneratorSprite;
    public Sprite EnabledRoad;
    public Sprite DisabledRoad;

    public float DarknessPercent = 0.95f;

    private Grid _grid;
    private SpriteRenderer _renderer;
    private bool _hovered;

	// Use this for initialization
	void Start ()
	{
	    _grid = FindObjectOfType<Grid>();
	    _renderer = GetComponent<SpriteRenderer>();
	}

    void OnMouseOver()
    {
        if (Type == TileType.Normal && Input.GetMouseButtonDown(0))
        {
            Type = TileType.Transport;
        }
        else if (Type == TileType.Transport && Input.GetMouseButtonDown(1))
        {
            Type = TileType.Normal;
        }

        _hovered = true;
    }

    void OnMouseExit()
    {
        _hovered = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Enabled = _grid.IsTileConnected(this);

	    switch (Type)
	    {
	        case TileType.Normal:
	            _renderer.sprite = Normal;
                break;
	        case TileType.Transport:
	            _renderer.sprite = Enabled ? EnabledRoad : DisabledRoad;
                break;
	        case TileType.City:
	            _renderer.sprite = CitySprite;
                break;
	        case TileType.Generator:
	            _renderer.sprite = GeneratorSprite;
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }

	    var darkness = _hovered ? 1 : Mathf.Clamp(DarknessPercent, 0f, 1f);
        _renderer.color = new Color(darkness, darkness, darkness);
	}
}
