using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;
	public float accelerationSensibility;
	public float smooth;
	
	private Rigidbody rb;
	private int count;

	private Vector3 curAc;
	private Vector3 zeroAc;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";

		ResetAxes();
	}
	
	void FixedUpdate () {
		float moveHorizontal;
		float moveVertical;


		if (SystemInfo.deviceType == DeviceType.Handheld) {
			curAc = Vector3.Lerp(curAc, Input.acceleration-zeroAc, Time.deltaTime/smooth);
			moveHorizontal = Mathf.Clamp(curAc.x * accelerationSensibility, -1, 1);
			moveVertical = Mathf.Clamp(curAc.y * accelerationSensibility, -1, 1);
		} else {
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
		}
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}
	
	void SetCountText () {
		countText.text = "Count: " + count.ToString ();

		if (count >= 12) {
			winText.text = "You win";
		}
	}

	void ResetAxes(){
		if (SystemInfo.deviceType == DeviceType.Handheld) {
			zeroAc = Input.acceleration;
			curAc = Vector3.zero;
		}
	}
}