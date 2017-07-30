using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorGhost : MonoBehaviour {

    public Texture2D BuildCursor;
    public bool On;

	// Use this for initialization
	void Start () {
        On = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (On)
        {
            Cursor.SetCursor(BuildCursor, Vector2.zero, CursorMode.Auto);
        }
	}
}
