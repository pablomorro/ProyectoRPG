using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    Image liveOrb;

    public Player player;
    private float hitPoints;
    // Start is called before the first frame update
    void Start()
    {
        liveOrb = GetComponent<Image>();
        liveOrb.fillAmount = 1f;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        hitPoints = player.hitPoints;
        liveOrb.fillAmount = hitPoints / 100;
    }
}
