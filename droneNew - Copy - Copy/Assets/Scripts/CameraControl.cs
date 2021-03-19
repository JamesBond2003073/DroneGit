using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControl : MonoBehaviour
{
	public GameObject CameraFollowObj;

	
	public float CameraMoveSpeed = 120.0f;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
	 float mouseX;
	 float mouseY;
	public float rotX = 0.0f;
	float refVelocityX;
	float refVelocityZ;
	float refVelocityY;
	float refvelRot;
	float refRot;
	public float rotSpeed = 50f;
public GameObject quad;
public PhotonView PVPlayer;

	// Use this for initialization
	void Start () {

		
		Vector3 rot = transform.localRotation.eulerAngles;
		 

	
		
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  if(CameraFollowObj == null)
	  {
	  foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
			
			PVPlayer = player.transform.gameObject.GetComponent<PhotonView>();

			if(PVPlayer.IsMine)
			{
			quad = player.transform.gameObject;	 
			CameraFollowObj = quad.transform.GetChild(0).gameObject;
			break;
			}
		}
	  }	  
	}

	void FixedUpdate () {
		if(CameraFollowObj != null)
		{
		CameraUpdater ();
		 RotationLeftRight();
		// if(transform.rotation.eulerAngles.x <= 30f || transform.rotation.eulerAngles.x >= 340f)
		{
			 transform.rotation = Quaternion.Euler(Mathf.SmoothDamp(transform.rotation.x,rotX,ref refRot,14f * Time.deltaTime),quad.transform.eulerAngles.y,0f);
		}
		/*
		  if(transform.rotation.eulerAngles.x > 30f && transform.rotation.eulerAngles.x < 340f)
		 {
			 if(transform.rotation.eulerAngles.x < 31f)
			 {   Vector3 tmp = transform.rotation.eulerAngles;
                 tmp.x = 30f;
                 transform.eulerAngles = tmp;
				// transform.rotation = Quaternion.Euler(30f,transform.rotation.y,transform.rotation.z);
			 }
			 else if(transform.rotation.eulerAngles.x > 339f)
			 {    Vector3 tmp = transform.rotation.eulerAngles;
                 tmp.x = 340f;
                 transform.eulerAngles = tmp;
				// transform.rotation = Quaternion.Euler(340f,transform.rotation.y,transform.rotation.z);
			 }
		 }
		 */
	}
	}

	void CameraUpdater()
{
		// set the target object to follow
		Transform target = CameraFollowObj.transform;
		transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x,target.position.x, ref refVelocityX, 2f * Time.deltaTime),quad.transform.position.y - 1.5f,Mathf.SmoothDamp(transform.position.z,target.position.z, ref refVelocityZ, 2f * Time.deltaTime));
	
	
	    
}

 void RotationLeftRight()
    {   if(Input.GetAxis("Mouse Y") != 0)
      { rotX += 1f * Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
      // transform.Rotate(0f,0f,0f);
      }    

    }


}
