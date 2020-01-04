using EscapeRoom.Interact.Drop;
using EscapeRoom.Interact.Item;
using EscapeRoom.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EscapeRoom.Core
{
    public class ProgressTracker : MonoBehaviour
    {
        [SerializeField] GameState[] gameStates;
        [SerializeField] TextMeshProUGUI tipText;
        [SerializeField] float delayBeforeTip = 15f;
        [SerializeField] float delayBeforeTipSwitchOver = 10f;

        List<GameState> objectivesLeftToDo = new List<GameState>();
        Coroutine currentTip;

        Safe safe;
        Lamp lamp;
        AlarmClock alarmClock;
        Door door;
        Bookshelf bookshelf;

        bool showingTip = false;

        private void Awake()
        {
            safe = FindObjectOfType<Safe>();
            lamp = FindObjectOfType<Lamp>();
            alarmClock = FindObjectOfType<AlarmClock>();
            door = FindObjectOfType<Door>();
            bookshelf = FindObjectOfType<Bookshelf>();            
        }

        private void Start()
        {
            for (int i = 0; i < gameStates.Length; i++)
            {
                objectivesLeftToDo.Add(gameStates[i]);
            }

            currentTip = StartCoroutine(ShowTips(objectivesLeftToDo[0]));
        }

        private void OnEnable()
        {
            safe.onSafeOpen += ObjectiveCompleted;
            lamp.onScrewInBulb += ObjectiveCompleted;
            alarmClock.onPowerAlarmClock += ObjectiveCompleted;
            door.onDoorOpen += ObjectiveCompleted;
            bookshelf.onSequenceSolved += ObjectiveCompleted;
        }

        private void OnDisable()
        {
            safe.onSafeOpen -= ObjectiveCompleted;
            lamp.onScrewInBulb -= ObjectiveCompleted;
            alarmClock.onPowerAlarmClock -= ObjectiveCompleted;
            door.onDoorOpen -= ObjectiveCompleted;
            bookshelf.onSequenceSolved -= ObjectiveCompleted;
        }

        private void ObjectiveCompleted(Objective objective)
        {
            for(int i = 0; i < objectivesLeftToDo.Count; i++)
            {
                GameState state = objectivesLeftToDo[i];

                if (objective == state.GetObjective())
                {
                    objectivesLeftToDo.RemoveAt(i);
                    if (objectivesLeftToDo.Count > 0 && i == 0)
                    {
                        if (currentTip != null) StopCoroutine(currentTip);
                        currentTip = StartCoroutine(ShowTips(objectivesLeftToDo[0]));
                    }
                }
            }
        }

        IEnumerator ShowTips(GameState gameState)
        {
            tipText.text = "";
            yield return new WaitForSeconds(delayBeforeTip);

            while(true)
            {
                tipText.text = "Tip: " + gameState.GetTipOne();

                yield return new WaitForSeconds(delayBeforeTipSwitchOver);

                tipText.text = "Tip: " + gameState.GetTipTwo();

                yield return new WaitForSeconds(delayBeforeTipSwitchOver);
            }
        }
    }    
    
    [System.Serializable]
    public class GameState
    {
        [SerializeField] Objective objective;
        [SerializeField] string tipOne;
        [SerializeField] string tipTwo;

        public Objective GetObjective()
        {
            return objective;
        }

        public string GetTipOne()
        {
            return tipOne;
        }

        public string GetTipTwo()
        {
            return tipTwo;
        }
    }

    public enum Objective
    {
        LookForLightBulb,
        ArrangeBooks,
        LookForBatteries,
        EnterSafeCode,
        OpenDoor
    }
}

