using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool Road;
    public bool Enabled = true;

    public Sprite Normal;
    public Sprite EnabledRoad;
    public Sprite DisabledRoad;

    public float DarknessPercent = 0.95f;

    private Grid _grid;
    private SpriteRenderer _renderer;
    private bool _hovered;

	// Use this for initialization
	void Start ()
	{
	    _grid = GetComponentInParent<Grid>();
	    _renderer = GetComponent<SpriteRenderer>();
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Road = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Road = false;
        }

        _hovered = true;
    }

    void OnMouseExit()
    {
        _hovered = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Road)
	    {
	        _renderer.sprite = Enabled ? EnabledRoad : DisabledRoad;
	    }
	    else
	    {
	        _renderer.sprite = Normal;
	    }

	    var darkness = _hovered ? 1 : Mathf.Clamp(DarknessPercent, 0f, 1f);
        _renderer.color = new Color(darkness, darkness, darkness);

	}
}
