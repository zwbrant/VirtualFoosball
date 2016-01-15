using UnityEngine;
using System.Collections;

public class oculus : MonoBehaviour {
	public GameObject oculusCam;
	public KeyCode key;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(key)) {
			oculusCam.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}
}
