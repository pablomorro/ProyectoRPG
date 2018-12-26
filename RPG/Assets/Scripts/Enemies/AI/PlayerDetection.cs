using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    private AI_Movement ai;
    public Transform initPos;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponentInParent<AI_Movement>();
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

}
