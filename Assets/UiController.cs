using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    public Text PowerReserves;
    public Text SpaceDate;
    public Text[] CityPower;

    public int SpaceDateWeekMillis = 10000;

    private PowerSystem powerSystem;
    private Timer elapsedWeekTimer;

    public int SpaceWeek = 1;
    public int SpaceMonth = 3;
    public int SpaceYear = 2197;

	// Use this for initialization
	void Start () {
        powerSystem = GameObject.FindObjectOfType<PowerSystem>();

        elapsedWeekTimer = new System.Timers.Timer();
        elapsedWeekTimer.Elapsed += new ElapsedEventHandler(SpaceDateClockCalculator);
        elapsedWeekTimer.Interval = SpaceDateWeekMillis;
        elapsedWeekTimer.Enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        PowerReserves.text = powerSystem.TotalPower.ToString("N0");
        SpaceDate.text = "WEEK " + SpaceWeek + "    MONTH " + SpaceMonth + "\nYEAR " + SpaceYear;
	}

    void SpaceDateClockCalculator(object source, ElapsedEventArgs e)
    {
        SpaceWeek++;
        if (SpaceWeek > 4)
        {
            SpaceWeek = 1;
            SpaceMonth++;
        }
        if (SpaceMonth > 12)
        {
            SpaceMonth = 1;
            SpaceYear++;
        }
    }
}
