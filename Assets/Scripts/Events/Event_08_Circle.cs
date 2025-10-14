using Player;
using UnityEngine;

public class Event_08_Circle : MonoBehaviour

{
   [SerializeField] GameObject interaction;
   
   void Awake()
   {
      interaction.GetComponent<PlayerInteraction>().CircleCntIncrease();
   }
}
