using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public int RoadCreateCost = 5;
    public int RoadDestroyCost = 2;

    public int PowerGenerateAmount = 1;
    public int PowerConsumeAmount = 5;

    public int PowerCreateCost = 5;

    public int TotalPower = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void AddPower(int amount)
    {
        TotalPower += amount;
    }
}
