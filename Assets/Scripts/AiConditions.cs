using UnityEngine;
using System.Collections;

public static class AiConditions {

    //SIGNATURE
    public static bool BallIsStill(GameObject ball)
    {
        if (ball.GetComponent<Rigidbody>().velocity == new Vector3(0,0,0))
        {

        }
        return true;
    }

    //SIGNATURE
    public static bool BallIsThreateningGoal(GameObject ball)
    {
        return (GetRigid(ball).velocity.z > 0.1);
    }

    /*SIGNATURE
    public bool BallHasOpenPath(GameObject ball)
    {
        return true;
    }

    //SIGNATURE
    public bool BallIsThreateningOwnGoal(GameObject ball)
    {
        return true;
    }

    //SIGNATURE
    public bool BallIsMovingVertically()
    {
        return true;
    }*/

    public static Rigidbody GetRigid(GameObject gameObject)
    {
        return gameObject.GetComponent<Rigidbody>();
    }
}
