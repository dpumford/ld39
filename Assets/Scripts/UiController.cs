using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    public Text PowerReserves;
    public Text SpaceDate;
    public Text[] CityPower;
    public GameObject[] Tabs;
    public Slider[] CityPowerBars;

    public int SpaceDateWeekMillis = 10000;

    public int SpaceWeek = 1;
    public int SpaceMonth = 3;
    public int SpaceYear = 2197;

    public Text TransferAmount;

    public Grid Grid;
    public int Mode = 1;

    public CursorGhost CursorGhost;

    private Generator generator;
    private PowerSystem powerSystem;
    private Timer elapsedWeekTimer;
    
    private GameObject selectedTab;
    private List<Tile> cities;

    private float powerBarWidth = 160;
    private int powerSetting = 1500;

    // Use this for initialization
    void Start () {
        powerSystem = GameObject.FindObjectOfType<PowerSystem>();
        generator = GameObject.FindObjectOfType<Generator>();

        elapsedWeekTimer = new System.Timers.Timer();
        elapsedWeekTimer.Elapsed += new ElapsedEventHandler(SpaceDateClockCalculator);
        elapsedWeekTimer.Interval = SpaceDateWeekMillis;
        elapsedWeekTimer.Enabled = true;
        if (Tabs.Length > 0)
        {
            selectedTab = Tabs[0];
        }
        cities = Grid.GetCities();
	}
	
	// Update is called once per frame
	void Update () {
        PowerReserves.text = powerSystem.TotalPower.ToString("N0");
        SpaceDate.text = "WEEK " + SpaceWeek + "    MONTH " + SpaceMonth + "\nYEAR " + SpaceYear;
        int i = 0;
        foreach (var city in cities)
        {
            City c = (City)city;
            int p = c.Power;
            CityPower[i].text = "POWER\n" + p.ToString("N0");
            CityPowerBars[i].value = p;
            i++;
        }
	}

    public void SelectTab(int selectedTabIndex)
    {
        if (Tabs.Length-1 >= selectedTabIndex)
        {
            selectedTab.SetActive(false);
            selectedTab = Tabs[selectedTabIndex];
            selectedTab.SetActive(true);
            Mode = selectedTabIndex + 1;
        }
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

    public void SetPowerTransferUnits(Slider slider)
    {
        int transferAmount = (int)slider.value;
        if (transferAmount == 0)
        {
            TransferAmount.text = "STANDBY";
            powerSetting = 0;
        }
        else if (transferAmount == 1)
        {
            TransferAmount.text = "1500 JiW";
            powerSetting = 1500;
        }
        else if (transferAmount == 2)
        {
            TransferAmount.text = "3000 JiW";
            powerSetting = 3000;
        }
        else if (transferAmount == 3)
        {
            TransferAmount.text = "7000 JiW";
            powerSetting = 7000;
        }
    }

    public void StartGhostCursor()
    {
        CursorGhost.On = true;
    }

    public void StopGhostCursor()
    {
        CursorGhost.On = false;
    }

    public void SetGeneratorPower()
    {
        if (generator)
        {
            generator.SetPowerToSend(powerSetting);
        }
    }

    public void GeneratorSendPower(int city)
    {
        if (generator)
        {
            generator.SendPower(city);
        }
    }
}
