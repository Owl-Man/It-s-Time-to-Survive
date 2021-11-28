using Player;
using UnityEngine;

public class LinkManager : MonoBehaviour 
{
    [Header ("Links Storage")]

    public GameObject PlayerObject;

    public PlayerController playerController;
    public InventorySystem inventory;

    public Indicators indicators;
    
    public Values values;

    public RedMoonWave redMoonWave;

    public GameObject FireBurningPlayer;

    public GameObject DarkMagicPlayer;

    public BloodScript bloodCntrl;

    public MessageSystem messageSystem;

    public static LinkManager instance;

    private void Awake() => instance = this;
}