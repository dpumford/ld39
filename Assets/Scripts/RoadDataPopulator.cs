using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadDataPopulator : MonoBehaviour {

    public bool Unlocked = false;
    public string Name;
    public string Description;
    public int BuildCost;
    public int RepairCost;
    public int RoadSpeed;
    public int RoadDurability;

    public Text RoadTitle;
    public Text RoadDescription;
    public Text StatBlock;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private string GetValueDescription(int value)
    {
        if (value == 1)
        {
            return "LOW";
        }
        else if (value == 2)
        {
            return "MEDIUM";
        }
        else if (value == 3)
        {
            return "HIGH";
        }
        else
        {
            return "VERY HIGH";
        }
    }

    public void ShowInfo()
    {
        if (Unlocked)
        {
            RoadTitle.text = Name;
            RoadDescription.text = Description;
            StatBlock.text = BuildCost + " JiW\n" 
                + RepairCost + " JiW\n" 
                + GetValueDescription(RoadSpeed) + "\n" 
                + GetValueDescription(RoadDurability);
        }
    }
}
