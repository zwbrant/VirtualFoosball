using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static int p1Score;
    public static int p2Score;
	public Text p1ScoreText;
    public Text p2ScoreText;
	public GameObject ball;
	public CameraController cameraControl;
    public MovementManager movement;
	public static bool scored;
    private float timestamp;
    private float spawnDelay = 0.5f;
	
	// Use this for initialization
	void Start () {
        p1Score = 0;
        p2Score = 0;
		scored = false;
	}
	
	// Update is called once per frame
	void Update () {
		p1ScoreText.text = p1Score.ToString ();
        p2ScoreText.text = p2Score.ToString();
		if (Input.GetButtonUp("A") && Time.time >= timestamp) {
            scored = false;
			Debug.Log("New Ball Spawned");
			GameObject newBall = (GameObject)Instantiate(ball);
			newBall.transform.SetParent(GameObject.Find("Balls").transform);
			newBall.transform.localPosition = new Vector3 (7.0f, 1.0f, 8.5f);
			cameraControl.ball = newBall;
            cameraControl.ResetCamera();
            movement.ball = newBall;
            Destroy(ball);
			ball = newBall;

            timestamp = Time.time + spawnDelay;
		}
	}


}
