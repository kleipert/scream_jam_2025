using GameManager;
using UnityEngine;

public class SchoolGirlAnimations : MonoBehaviour
{
    private Animator _anim;

    private readonly float randomAnimCooldownBase = 7f;
    private float randomAnimCooldownCurrent = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventController.instance.GetActiveEvent() == Data.Events.DemonThrowsUp) return;
        
        if (randomAnimCooldownCurrent <= 0)
        {
            randomAnimCooldownCurrent = randomAnimCooldownBase;
            var randomAnim = Random.Range(0, 3);

            switch (randomAnim)
            {
                case 0:
                    _anim.SetTrigger("writhing");
                    break;
                case 1:
                    _anim.SetTrigger("severe");
                    break;
                case 2:
                    _anim.SetTrigger("seizure");
                    break;
            }
        }

        randomAnimCooldownCurrent -= Time.deltaTime;
    }
}
