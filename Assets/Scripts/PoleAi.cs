using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoleAi {
    public GameObject[] aiPoles;
    //public GameObject ball;
    public MovementManager moveMgmt;

    private GameObject nearestPole;
    private GameObject nearestMan;

    #region Properties
    public GameObject Ball
    {
        get { return moveMgmt.ball; }
    }
    
    public GameObject NearestPole
    {
        get { return GetNearestPole(); }
    }

    public GameObject NearestMan
    {
        get { return GetNearestMan(); }
    }
    #endregion

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PoleAi(GameObject[] controlledPoles, MovementManager movementManager)
    {
        aiPoles = controlledPoles;
        moveMgmt = movementManager;

        GetNearestPole();
        GetNearestMan();
    }

    public void Movement()
    {
        Debug.Log("Ball velocity: " + AiConditions.GetRigid(Ball).velocity.ToString());

        /*
        if (AiConditions.BallIsThreateningGoal(Ball))
        {
            Debug.Log("Ball is threatening your goal");
        }
        else
        {
            Debug.Log("Ball ain't threating your goal");
        }
        */
    }

    public void BetaMovement()
    {
        GameObject pole = NearestMan.transform.parent.gameObject;
        float xDist = NearestMan.transform.position.x - Ball.transform.position.x;
        float zDist = GetZDist(NearestMan, Ball);

        //Debug.Log("Position of ball: ( " + xDist + ", " + zDist + " )");
        pole.GetComponent<Rigidbody>().AddForce(new Vector3(xDist * -100.0f, 0.0f, 0.0f));

        if (xDist > -1.3f && xDist < 1.3f && zDist > -2.0f && zDist < 2.0f)
        {
            pole.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0.0f, 1500.0f, 0.0f));
        }
    }

    GameObject GetNearestMan()
    {
        GetNearestPole();

        if (nearestMan == null)
            nearestMan = nearestPole.transform.GetChild(0).gameObject;    //Defaults to first man as a placeholder before checking distances
        else
            nearestMan.GetComponent<Renderer>().material.color = Color.white;

        float minDist = Vector3.Distance(nearestMan.transform.position, Ball.transform.position);
        for (int i = 0; i < nearestPole.transform.childCount - 1; i++)
        {
            if (minDist > Vector3.Distance(nearestPole.transform.GetChild(i).transform.position, Ball.transform.position))
            {
                minDist = Vector3.Distance(nearestPole.transform.GetChild(i).position, Ball.transform.position);
                nearestMan.GetComponent<Renderer>().material.color = Color.white;
                nearestMan = nearestPole.transform.GetChild(i).gameObject;
            }
        }
        nearestMan.GetComponent<Renderer>().material.color = Color.red;
        return nearestMan;
    }

    GameObject GetNearestPole()
    {
        if (nearestPole == null) 
            nearestPole = aiPoles[0];    //Defaults to first pole as a placeholder before checking distances
        else
            nearestPole.GetComponent<Renderer>().material.color = Color.white;

        float minDist = GetZDist(nearestPole, Ball);
        for (int i = 0; i < aiPoles.Length; i++)
        {
            if (GetZDist(aiPoles[i], Ball) < minDist)
            {
                minDist = GetZDist(aiPoles[i], Ball);
                nearestPole = aiPoles[i];
            }
        }
        nearestPole.GetComponent<Renderer>().material.color = Color.red;
        return nearestPole;
    } 

    float GetZDist(GameObject firstObj, GameObject secondObj)
    {
        float dist = firstObj.transform.position.z - secondObj.transform.position.z;
        if (dist < 0)
            dist *= -1;
        return dist;
    }
}
