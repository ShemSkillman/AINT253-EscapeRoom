using EscapeRoom.Interact.Item;
using EscapeRoom.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EscapeRoom.Item;
using System;

namespace EscapeRoom.Interact.Drop
{
    public class AlarmClock : DropZone
    {
        [SerializeField] Battery batteryPrefab;
        [SerializeField] TextMeshProUGUI timeDisplay;
        [SerializeField] Transform batterySlotOne;
        [SerializeField] Transform batterySlotTwo;
        [SerializeField] float timeShowDuration = 1f;
        [SerializeField] float timeFlashDuration = 0.5f;

        bool hasOneBattery = false;
        bool isPowered = false;
        string code;

        GameObject[] hiddenBatteryLocations;

        Animator animator;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();

            hiddenBatteryLocations = GameObject.FindGameObjectsWithTag("Battery location");
        }

        protected override void Start()
        {
            base.Start();
            code = FindObjectOfType<Safe>().GetCode();

            timeDisplay.text = code.Substring(0, 2) + ":" + code.Substring(2, 2);

            ScatterBatteries();
        }

        private void ScatterBatteries()
        {
            int randomIndexOne = UnityEngine.Random.Range(0, hiddenBatteryLocations.Length);
            int randomIndexTwo = UnityEngine.Random.Range(0, hiddenBatteryLocations.Length);

            if (randomIndexTwo == randomIndexOne) UnityEngine.Random.Range(0, hiddenBatteryLocations.Length);

            Transform locationOne = hiddenBatteryLocations[randomIndexOne].transform;
            Transform locationTwo = hiddenBatteryLocations[randomIndexTwo].transform;

            Instantiate(batteryPrefab.gameObject, locationOne.position, locationOne.rotation, locationOne.parent);
            Instantiate(batteryPrefab.gameObject, locationTwo.position, locationTwo.rotation, locationTwo.parent);
        }

        public override bool IsInteractionValid()
        {
            InventoryItem item = inventory.GetSelectedItem();

            if (item == null || item.Item == null) return false;

            Battery battery = item.Item.GetComponent<Battery>();
            
            if (battery == null) return false;

            ShowItemGhost();
            return true;
        }

        protected override void ShowItemGhost()
        {
            if (itemGhost != null) return;

            if (!hasOneBattery)
            {
                itemGhost = Instantiate(itemGhostPrefab, batterySlotOne.position, batterySlotOne.rotation, batterySlotOne);
            }
            else
            {
                itemGhost = Instantiate(itemGhostPrefab, batterySlotTwo.position, batterySlotTwo.rotation, batterySlotTwo);
            }
        }

        protected override void Trigger(bool isActive)
        {
            if (!isActive) return;

            InventoryItem item = inventory.GetSelectedItem();

            inventory.RemoveItem(item);

            Battery battery = item.Item.GetComponent<Battery>();

            Transform spawnTransform;

            if (!hasOneBattery)
            {
                spawnTransform = batterySlotOne;
                hasOneBattery = true;
            }
            else
            {
                spawnTransform = batterySlotTwo;
                CloseBatteryCover();
            }

            Battery instance = Instantiate(battery, spawnTransform.position, spawnTransform.rotation, spawnTransform);

            instance.SetItemPrefab(battery.gameObject);
            instance.SetIsInteractable(false);
        }

        private void CloseBatteryCover()
        {
            SetIsInteractable(false);
            animator.SetTrigger("closeBatteryCover");
        }

        private void PowerAlarmClock()
        {
            StartCoroutine(DisplayTime());
        }

        private IEnumerator DisplayTime()
        {

            while(true)
            {
                timeDisplay.gameObject.SetActive(true);
                yield return new WaitForSeconds(timeShowDuration);

                timeDisplay.gameObject.SetActive(false);
                yield return new WaitForSeconds(timeFlashDuration);
            }
        }
    }
}

