using System;
using UnityEngine;

namespace Player
{
    public class Event11PlayerData : MonoBehaviour
    {
        public bool getsAttacked = false;
        public uint ghostsKilled = 0;
        public bool allGhostsKilled = false;

        private void Update()
        {
            if (ghostsKilled == 5)
                allGhostsKilled = true;
        }
    }
}