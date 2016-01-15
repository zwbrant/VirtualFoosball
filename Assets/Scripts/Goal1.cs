using UnityEngine;
using System.Collections;

public class Goal1 : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Point Scored");
        if (GameManager.scored == false)
        {
            GameManager.scored = true;
            if (this.transform.name == "PlayerOneGoal")
            {
                GameManager.p1Score++;

            }
            else
            {
                GameManager.p2Score++;
            }
        }
    }
}
