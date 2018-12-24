using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    Image liveOrb;

    // Start is called before the first frame update
    void Start()
    {
        liveOrb = GetComponent<Image>();
        liveOrb.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            liveOrb.fillAmount -= 0.2f;
        }
    }
}
