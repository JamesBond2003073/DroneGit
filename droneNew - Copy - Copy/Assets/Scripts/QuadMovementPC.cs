using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class QuadMovementPC : MonoBehaviour
{
    public Rigidbody quad;
    public float verticalForce;
    public float fwdSpeed = 450f;
    public float fwdTilt = 0f;
    public float fwdTiltVelocity;
    public float sideSpeed = 450f;
    public float sideTilt;
    public float sideTiltVelocity;
    public float velY;
    public float refvelY;
    Vector3 hover;
    public float refHoverX;
    public float refHoverY;
    public float refHoverZ;
    public float refRotSide;
    public float timerHover = 0.3f;
    public Vector3 velocityToSmoothDampToZero;
    public Joystick joystickDir;
    public Joystick joystick2;
    public Joystick joyDirRef;
    public Joystick joyVerRef;
    public float hor;
    public float ver;
    public float ver2;
    public float verticalCheckTime = 1.5f;
    public float rotY;
    public float rotY2 = -1598f;
    public float rotSpeed = 45f;
    public int startFlag = 2;
    public int obs1Flag = 0;
    public int endflag = 0;
  //  public float flipForce = 700f;
  //  public float timerFlip = 10f;
   // public float flipCheck = 0f;

   public Animator anim;
   private PhotonView PV;
   public Renderer renderer;
   public Color color1;
   public int hoopNo = 0;
   public int currentHoopNo;
   public int loopCount = 0;

   public GameObject endUI;

     void OnTriggerEnter(Collider other)
     { 
          int.TryParse(other.gameObject.name, out currentHoopNo);

          if(PV.IsMine && other.gameObject.tag == "Hoop" && (currentHoopNo == (hoopNo + 1)) )
     {   hoopNo ++;
         renderer = other.gameObject.GetComponent<Renderer>();
         renderer.material.SetColor("_BaseColor", color1);
         if(hoopNo == 15)
         {loopCount ++;
          hoopNo = 0;
         }
     }

     if(other.gameObject.tag == "Obs1" && PV.IsMine)
     {
           obs1Flag = 1;
     }

     }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "ballObst")
        {
           // this.gameObject.GetComponent<QuadMovementPC>().enabled = false;
        }
    }


    void Start()
    {   endUI = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
       // endUI.SetActive(false);
        
         PV = GetComponent<PhotonView>();
       if(PV.IsMine)
       {
       // anim = GameObject.Find("Obstacle_balls(Clone)").transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<Animator>();  
        quad = GetComponent<Rigidbody>();
        hover = new Vector3(Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f)); 
     //   joystickDir =  GameObject.FindGameObjectWithTag("joyDir").GetComponent<Joystick>() as Joystick;
     //   joystick2 = GameObject.FindGameObjectWithTag("joyVer").GetComponent<Joystick>() as Joystick;
       }

    }

    void FixedUpdate()
    { 
       
        if(PV.IsMine) 
    { 
        if(loopCount == 2 && obs1Flag == 1 && endflag == 0)
        {
       PV.RPC("RPC_SyncEnd", RpcTarget.AllBuffered, transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
       endflag = 1;
        }
        if(Input.GetKey(KeyCode.R) && PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel(0);
        
    //display parameters
        if(Input.GetKey(KeyCode.A))
        hor = -1;
        else if (Input.GetKey(KeyCode.D))
        hor = 1;
        else
        hor = 0;
        
         if(Input.GetKey(KeyCode.W))
        ver = 1;
        else if (Input.GetKey(KeyCode.S))
        ver = -1;
        else
        ver = 0;

        velY = quad.velocity.y;

        quad.angularVelocity = Vector3.zero;
    if(startFlag == 0 || startFlag == 2)
    {
       MovementUpDown();
       MovementForward();
       MovementLeftRight();
       // if(startFlag == 2)
    //{
        RotationLeftRight();
    //}
       ClampingSpeed();
    }
   
   // if(startFlag == 0)
   // RotationLeftRight2();
        
       Hover();
       SmoothVelocityCheck();

      quad.AddRelativeForce(Vector3.up  * verticalForce);

     // if(startFlag == 2 || startFlag == 0)
      quad.rotation = Quaternion.Euler(new Vector3(fwdTilt,Mathf.SmoothDamp(quad.rotation.y,rotY,ref refRotSide,0.5f) ,sideTilt));
     // else if(startFlag == 0)
     // quad.rotation = Quaternion.Euler(new Vector3(fwdTilt,Mathf.SmoothDamp(quad.rotation.y,rotY2,ref refRotSide,0.5f) ,sideTilt));
      
    }
    }
      
    void MovementUpDown()
    {
      if(Input.GetKey(KeyCode.Space))
      verticalForce = 520f;
     else if(Input.GetKey(KeyCode.LeftControl))
      verticalForce = -420f;
      else
      verticalForce = 98.1f;
      
      /*if(Mathf.Abs(joystickDir.Horizontal) >= 0.4f && joystickDir.Vertical <= 0.2f)
      { Debug.Log("lol");
           if(joystick2.Vertical == 0)
           verticalForce = 111.8f;
      }
      if(Mathf.Abs(joystickDir.Vertical) >= 0.4f && joystickDir.Horizontal <= 0.2f)
      {
           if(joystick2.Vertical == 0)
           verticalForce = 108.4f * joystickDir.Vertical + 108.4f * joystickDir.Horizontal ;
      }
      if(Mathf.Abs(joystickDir.Vertical) >= 0.4f && joystickDir.Horizontal >= 0.4f)
      {
           if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl))
           verticalForce = 119.5f;
      }*/

      if(ver != 0 && hor != 0)
      {
          if((!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl)) && verticalCheckTime <= 0f)
           quad.velocity = new Vector3(quad.velocity.x,0f,quad.velocity.z);
          
      }

    }

    void MovementForward()
    { 
        if(ver != 0f)
        {
            quad.AddForce(new Vector3(transform.forward.x,0f,transform.forward.z) * ver  * fwdSpeed * Time.deltaTime);
         }
         fwdTilt = Mathf.SmoothDamp(fwdTilt,24* ver,ref fwdTiltVelocity,6f * Time.deltaTime);
    }

     void MovementLeftRight()
    {
         if(hor != 0f)
        {
            quad.AddForce(new Vector3(transform.right.x,0f,transform.right.z) * hor * sideSpeed * Time.deltaTime);
        }
         sideTilt = Mathf.SmoothDamp(sideTilt,-24* hor,ref sideTiltVelocity,6f * Time.deltaTime);
    }

    void RotationLeftRight()
    {   if(Input.GetAxis("Mouse X") != 0)
      { rotY += 1f * Input.GetAxis("Mouse X") * rotSpeed;
     
      }    

    }

     void RotationLeftRight2()
    {   if(Input.GetAxis("Mouse X") != 0)
      { rotY2 += 1f * Input.GetAxis("Mouse X") * rotSpeed;
     
      }    

    }

    void ClampingSpeed()
    {
        if(ver != 0f || hor != 0f )
        quad.velocity = Vector3.ClampMagnitude(quad.velocity,Mathf.Lerp(quad.velocity.magnitude,10f,Time.deltaTime * 5f));
         if(ver == 0f && hor == 0f )
         quad.velocity = Vector3.SmoothDamp(quad.velocity,Vector3.zero,ref velocityToSmoothDampToZero,20f * Time.deltaTime);
    }

    void Hover()
    {   timerHover -= Time.deltaTime;
        if(timerHover <= 0f)
        {
        hover = new Vector3(Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f));
        timerHover = 0.5f;
        }
        if(hor == 0f && ver == 0f && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl)))
        quad.velocity = new Vector3(Mathf.SmoothDamp(quad.velocity.x,hover.x, ref refHoverX, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.y,hover.y,ref refHoverY, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.z,hover.z, ref refHoverZ, 55f * Time.deltaTime));
        
        
    }

    void Flip()
    {
    //  quad.AddForce(quad.velocity.normalized * Time.deltaTime * flipForce);
    }

    void SmoothVelocityCheck()
    {
        if(ver == 0f)
    {
        verticalCheckTime -= Time.deltaTime;
    }
    if(ver != 0f)
    {
        verticalCheckTime = 1.5f;
    }
    }

    void OnCollisionEnter()
    {     if(PV.IsMine) 
        quad.angularVelocity = Vector3.zero;
    }

    void OnCollisionStay()
    {    if(PV.IsMine) 
        quad.angularVelocity = Vector3.zero;
    }

    [PunRPC]
   public void RPC_SyncEnd(string playerName)
   {
            
            endUI.GetComponent<TextMeshProUGUI>().text = playerName + " finished 1st!!";
            endUI.SetActive(true);
            foreach (Transform child in GameObject.Find("PlayersParent").transform)
            {
                child.GetComponent<QuadMovementPC>().endflag = 1;
            }
            
            
   } 

}
