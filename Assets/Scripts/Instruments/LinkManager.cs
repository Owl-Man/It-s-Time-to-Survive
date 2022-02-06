using System;
using Player;
using Spawner;
using UnityEngine;
using UnityEngine.Serialization;

namespace Instruments
{
    public class LinkManager : MonoBehaviour 
    {
        [Header ("Links Storage")]

        [FormerlySerializedAs("PlayerObject")] public GameObject playerObject;

        public PlayerController playerController;
        public InventorySystem inventory;

        public Indicators indicators;
    
        public Values values;

        public RedMoonWave redMoonWave;

        [FormerlySerializedAs("FireBurningPlayer")] public GameObject fireBurningPlayer;

        [FormerlySerializedAs("DarkMagicPlayer")] public GameObject darkMagicPlayer;

        public BloodScript bloodCntrl;

        public MessageSystem messageSystem;

        public static LinkManager Instance;

        private void Awake() => Instance = this;
    }
}