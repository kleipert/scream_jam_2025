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
        public Data.Events lastEvent;
        public bool isEventActive = false;

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

            //WICHTIG:
            //HEJ ZUKUNFTS-KEVIN NÄCHSTES MAL BESCHEID SAGEN BEVOR ICH 5 MIN SUCHEN KANN WARUM MEIN SCHEISS NICHT MEHR LÄUFT!
            
            //StartEvent(Data.Events.None);
            //lastEvent = Data.Events.None;
            // Testing
            //StartEvent(Data.Events.DemonThrowsItems);
        }

        private EventBase GetEventFromEnum(Data.Events eventToStart)
        {
            if (eventToStart == Data.Events.None)
            {
                return null;
            }
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
            _activeEvent?.StartEvent();
        }

        public void StopCurrentEvent()
        {
            lastEvent = _activeEvent.GetEvent();
            _activeEvent?.StopEvent();
            _activeEvent = null;
        }

        public Data.Events GetActiveEvent() => _activeEvent != null ? _activeEvent.GetEvent() : Data.Events.None;
        public Data.PlayerItems GetActiveItem() => _activeEvent != null ? _activeEvent.GetItem() : Data.PlayerItems.None;
    }
}