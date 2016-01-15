using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 

public class MovementManager : MonoBehaviour {
	public GameObject[] poles;
    public GameObject ball;
    //private GameObject closestMan;
    //private GameObject closestPole;

	private int currPoleP1;
	private int currPoleP2;
    private PoleAi currPoleAi1;
    private PoleAi currPoleAi2;
	public const float rotateSpeed = 100.0f;
	public const float slideSpeed = 10.0f;
	public const float snapSpeed = 30.0f;
	private float snapP1;
	private float snapP2;


	// Use this for initialization
	void Start () {
		snapP1 = 0.0f;
		snapP2 = 0.0f;
		currPoleP1 = 3;
		currPoleP2 = 4;

        GameObject[] aiPoles = {poles[2], poles[4], poles[6], poles[7]};
        currPoleAi1 = new PoleAi(aiPoles, this);

		for (int i = 0; i < poles.Length; i++) 
			poles[i].GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement (poles [currPoleP1], "P1");
		poles[currPoleP1].GetComponent<Renderer> ().material.color = Color.yellow;
		PlayerMovement (poles [currPoleP2], "P2");
		poles[currPoleP2].GetComponent<Renderer> ().material.color = Color.red;


        currPoleAi1.BetaMovement();
        currPoleAi1.Movement();
	}


    /*void AiMovement() {
        //ChooseClosestMan();

        GameObject closestMan = currPoleAi1.NearestMan;
        GameObject pole = closestMan.transform.parent.gameObject;
        float xDist = closestMan.transform.position.x - ball.transform.position.x;
        float zDist = closestMan.transform.position.z - ball.transform.position.z;

        //Debug.Log("Position of ball: ( " + xDist + ", " + zDist + " )");
        pole.GetComponent<Rigidbody>().AddForce(new Vector3(xDist * -100.0f, 0.0f, 0.0f));

        if (xDist > -1.3f && xDist < 1.3f && zDist > -2.0f && zDist < 2.0f)
        {
            pole.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0.0f, 1500.0f, 0.0f));
        }
    }*/

    /*
    GameObject ChooseClosestMan()
    {
        GameObject pole = ChooseClosestPole();
  
        //GameObject[] men = GameObject.FindGameObjectsWithTag("Man");
        float minDist = Vector3.Distance(closestMan.transform.position, ball.transform.position);

        for (int i = 0; i < pole.transform.childCount - 1; i++)
        {
                if (minDist > Vector3.Distance(pole.transform.GetChild(i).transform.position, ball.transform.position))
                {
                    minDist = Vector3.Distance(pole.transform.GetChild(i).position, ball.transform.position);
                    closestMan.GetComponent<Renderer>().material.color = Color.white;
                    closestMan = pole.transform.GetChild(i).gameObject;
                }
        }
        closestMan.GetComponent<Renderer>().material.color = Color.red;
        return closestMan;
    }

    GameObject ChooseClosestPole()
    {
        int[] onePlayOpp = {2,4,6,7};

        float minDist = Vector3.Distance(closestPole.transform.position, ball.transform.position);
        for (int i = 0; i < onePlayOpp.Length; i++)
        {
            if (minDist > Vector3.Distance(poles[onePlayOpp[i]].transform.position, ball.transform.position))
            {
                minDist = Vector3.Distance(poles[onePlayOpp[i]].transform.position, ball.transform.position);
                closestPole.GetComponent<Renderer>().material.color = Color.white;
                closestPole = poles[onePlayOpp[i]];
            }
        }
        closestPole.GetComponent<Renderer>().material.color = Color.red;
        return closestPole;
    }*/

    void PlayerMovement(GameObject currPole, string player) {
		float polarity = (player == "P1") ? 1.0f : -1.0f;
		currPole.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0.0f, -Input.GetAxis("Horizontal_" + player) * rotateSpeed,  0.0f));
		currPole.GetComponent<Rigidbody>().AddForce(new Vector3((polarity) * Input.GetAxis("Vertical_" + player) * slideSpeed, 0.0f, 0.0f));
		if (player == "P1") {
			if (Input.GetAxis ("Triggers_" + player) == 0.0f) {
				currPole.GetComponent<Rigidbody> ().AddRelativeTorque (new Vector3 (0.0f, snapP1 * 30.0f, 0.0f));
				snapP1 = 0;
			} else {
				snapP1 += Input.GetAxis ("Triggers_P1");
			}
		} else {
			if (Input.GetAxis ("Triggers_" + player) == 0.0f) {
				currPole.GetComponent<Rigidbody> ().AddRelativeTorque (new Vector3 (0.0f, snapP2 * snapSpeed, 0.0f));
				snapP2 = 0;
			} else {
				snapP2 += Input.GetAxis ("Triggers_P2");
			}
		}

	}

	void LateUpdate() {
        PoleSwitching();

	}

    void PoleSwitching()
    {
        if (Input.GetButtonDown("LB_P1"))
        {
            poles[currPoleP1].GetComponent<Renderer>().material.color = Color.white;
            if (currPoleP1 == 1)
                currPoleP1--;
            else if (currPoleP1 == 0)
                currPoleP1 = 5;
            else
                currPoleP1 = currPoleP1 - 2;
        }
        else if (Input.GetButtonDown("RB_P1"))
        {
            poles[currPoleP1].GetComponent<Renderer>().material.color = Color.white;
            if (currPoleP1 == 0)
                currPoleP1++;
            else if (currPoleP1 == 5)
                currPoleP1 = 0;
            else
                currPoleP1 = currPoleP1 + 2;
        }

        if (Input.GetButtonDown("LB_P2"))
        {
            poles[currPoleP2].GetComponent<Renderer>().material.color = Color.white;
            if (currPoleP2 == 2)
                currPoleP2 = 7;
            else if (currPoleP2 == 7)
                currPoleP2--;
            else
                currPoleP2 = currPoleP2 - 2;
        }
        else if (Input.GetButtonDown("RB_P2"))
        {
            poles[currPoleP2].GetComponent<Renderer>().material.color = Color.white;
            if (currPoleP2 == 6)
                currPoleP2++;
            else if (currPoleP2 == 7)
                currPoleP2 = 2;
            else
                currPoleP2 = currPoleP2 + 2;
        }
    }

}
