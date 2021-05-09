using UnityEngine;

public abstract class LinkManager : MonoBehaviour 
{
    [HideInInspector] public GameObject ManagerPlayerObject;

    [HideInInspector] public PlayerController ManagerPlayerCntrl;
    [HideInInspector] public InventorySystem ManagerInventory;

    [HideInInspector] public Indicators ManagerIndicators;

    private void Awake() 
    {
        ManagerPlayerObject = GameObject.FindGameObjectWithTag("Player");
        ManagerPlayerCntrl = ManagerPlayerObject.GetComponent<PlayerController>();
        ManagerInventory = ManagerPlayerObject.GetComponent<InventorySystem>();
        ManagerIndicators = ManagerPlayerObject.GetComponent<Indicators>();
    }
}