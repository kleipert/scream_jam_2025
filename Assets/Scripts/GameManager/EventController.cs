using System;
using Events;
using UnityEngine;

namespace GameManager
{
    public class EventController : MonoBehaviour
    {
        public static EventController instance;
        private EventBase _activeEvent;
        public EventBase[] allEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
            
            
        }

        private void Start()
        {
            // WICHTIG:
            // HALLO LEON. VERGANGENHEITS-KEVIN HIER!
            // HIER KANNST DU EINFACH DEIN EVENT STARTEN, DASS DU GERADE ENTWICKELST, EINFACH DIE NAECHSTE ZEILE AENDERN!
            // GANZ LIEBE GRUESSE, LANG LEBE PROF DR. BLICK, AUF DAS ER MIR EINE EINFACHE ANALYSIS KLAUSUR GOENNT!
            StartEvent(Data.Events.LightsOut);
        }

        private EventBase GetEventFromEnum(Data.Events eventToStart)
        {
            foreach (var possibleEvent in allEvents)
            {
                if (possibleEvent.GetEvent() == eventToStart)
                    return possibleEvent;
            }
            throw new Exception("NO EVENT");
        }

        public void StartEvent(Data.Events eventToStart)
        {
            _activeEvent = GetEventFromEnum(eventToStart);
            _activeEvent.StartEvent();
        }

        public Data.Events GetActiveEvent() => _activeEvent.GetEvent();
        public Data.PlayerItems GetActiveItem() => _activeEvent.GetItem();
    }
}