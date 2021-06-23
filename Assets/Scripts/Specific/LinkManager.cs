using UnityEngine;
using UnityEngine.UI;

public class LinkManager : MonoBehaviour 
{
    [Header ("Link Storage")]

    public GameObject PlayerObject;

    public PlayerController playerController;
    public InventorySystem inventory;

    public Indicators indicators;
    
    public Values values;

    public RedMoonWave redMoonWave;

    public GameObject FireBurningPlayer;

    public GameObject DarkMagicPlayer;

    public BloodScript bloodCntrl;

    public GameObject MessagePanel;

    public Text MessageText;
}