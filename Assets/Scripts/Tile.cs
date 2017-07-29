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

    public Power PowerPrefab;
    public int Power;
    public float PowerDeltaSeconds;

    private Grid _grid;
    private PowerSystem _powerSystem;
    private SpriteRenderer _renderer;
    private bool _hovered;
    private float _powerTimer;

	// Use this for initialization
	void Start ()
	{
	    _grid = FindObjectOfType<Grid>();
	    _powerSystem = FindObjectOfType<PowerSystem>();
	    _renderer = GetComponent<SpriteRenderer>();
	}

    void OnMouseOver()
    {
        if (Type == TileType.Normal && Input.GetMouseButtonUp(0) && _powerSystem.TotalPower > _powerSystem.RoadCreateCost)
        {
            Type = TileType.Transport;
            _powerSystem.AddPower(_powerSystem.RoadCreateCost * -1);
        }
        else if (Type == TileType.Transport && Input.GetMouseButtonUp(1) && _powerSystem.TotalPower > _powerSystem.RoadDestroyCost)
        {
            Type = TileType.Normal;
            _powerSystem.AddPower(_powerSystem.RoadDestroyCost * -1);
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

	            if (Power <= 0)
	            {
	                Type = TileType.Normal;
	            }
                break;
	        case TileType.Generator:
	            _renderer.sprite = GeneratorSprite;

	            if (Input.GetKeyUp(KeyCode.P))
	            {
	                foreach (var path in _grid.ValidConnections)
	                {
	                    if (_powerSystem.TotalPower > _powerSystem.PowerCreateCost)
	                    {
	                        var newPower = Instantiate(PowerPrefab, transform.position, Quaternion.identity);
	                        newPower.Path = path;

                            _powerSystem.AddPower(-1 * _powerSystem.PowerCreateCost);
	                    }
	                }
	            }
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }

	    if (_powerTimer > PowerDeltaSeconds)
	    {
	        if (Type == TileType.City)
	        {
	            AddPower(-1 * _powerSystem.PowerConsumeAmount);
	        }
            else if (Type == TileType.Generator)
	        {
	            _powerSystem.AddPower(_powerSystem.PowerGenerateAmount);
	        }

	        _powerTimer = 0;
	    }
	    else
	    {
	        _powerTimer += Time.deltaTime;
	    }

	    if (Power < 0)
	    {
	        Power = 0;
	    }

        var darkness = _hovered ? 1 : Mathf.Clamp(DarknessPercent, 0f, 1f);
        _renderer.color = new Color(darkness, darkness, darkness);
	}

    public void AddPower(int amount)
    {
        Power += amount;
    }
}
