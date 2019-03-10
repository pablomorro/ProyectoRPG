using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image healthBar;
    private AI_live enemy;

    private float maxLife;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        enemy = GetComponentInParent<AI_live>();
        maxLife = enemy.GetEnemyLife();
        healthBar.fillAmount = (enemy.GetEnemyLife()) / maxLife;

    }

    // Update is called once per frame
    void Update()
    {
        if (maxLife == 0) {
            maxLife = enemy.GetEnemyLife();
        }
        healthBar.fillAmount = (enemy.GetEnemyLife()) / maxLife;
    }
}
