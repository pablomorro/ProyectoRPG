using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    private EnemyAI ai;
    public Transform initPos;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.name.Equals("Player"))
        {
            //Start following the player
            ai.target = col.gameObject.transform;
            ai.StartLookingForPath();
        }
	}

	void OnTriggerExit2D(Collider2D col)
	{
        if (col.gameObject.name.Equals("Player"))
        {
            //Stop following the player
            ai.target = null;

            //return to the original position 
            ai.MoveToLocationWithoutUpdate(initPos);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {

            Debug.Log("dsadsa");
        }
    }
}
