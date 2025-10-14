using UnityEngine;

namespace Player
{
    public class Event14PlayerData : MonoBehaviour
    {
        public bool getsAttacked = false;
        public uint satanistsKilled = 0;
        public bool allSatanistsKilled = false;

        private void Update()
        {
            if (satanistsKilled == 5)
                allSatanistsKilled = true;
        }
    }
}