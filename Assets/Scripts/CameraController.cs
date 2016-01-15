using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject ball;
	public Vector3 startOffset;
    public Vector3 offset;
    public Quaternion startRotation;
	// Use this for initialization
	void Start () {
		startOffset = transform.position;
        offset = startOffset;
        startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
        Vector3 ballVelocity = ball.GetComponent<Rigidbody>().velocity;


        if (ball.transform.position.x < -9.0f)
        {
            offset.x += (-1f) * (ballVelocity.x * Time.deltaTime);
            transform.Rotate(new Vector3(ballVelocity.x * (-0.015f), 0.0f, 0.0f));
        }

        Vector3 ballPos = new Vector3(ball.transform.position.x + offset.x, 13.5f, ball.transform.position.z + offset.z);
        transform.position = ballPos;
    }

    public void ResetCamera()
    {
        transform.rotation = startRotation;
        offset = startOffset;
    }
}
