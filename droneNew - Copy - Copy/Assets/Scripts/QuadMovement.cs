using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadMovement : MonoBehaviour
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
    public float rotSpeed = 45f;
  //  public float flipForce = 700f;
  //  public float timerFlip = 10f;
   // public float flipCheck = 0f;
    void Start()
    { 
        quad = GetComponent<Rigidbody>();
        hover = new Vector3(Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f)); 
        joystickDir =  GameObject.FindGameObjectWithTag("joyDir").GetComponent<Joystick>() as Joystick;
        joystick2 = GameObject.FindGameObjectWithTag("joyVer").GetComponent<Joystick>() as Joystick;

    }

    void FixedUpdate()
    { //  timerFlip -= Time.deltaTime;
   // if(timer <= 0f)
  //  {   flipCheck = 1f;
       // Flip();
    //    timerFlip = 10f;
  //  }
    //display parameters
        hor = joystickDir.Horizontal;
        ver = joystickDir.Vertical;
        velY = quad.velocity.y;

        quad.angularVelocity = Vector3.zero;

       MovementUpDown();
       MovementForward();
       MovementLeftRight();
       RotationLeftRight();
       ClampingSpeed();
       Hover();
       SmoothVelocityCheck();

      quad.AddRelativeForce(Vector3.up  * verticalForce);
      quad.rotation = Quaternion.Euler(new Vector3(fwdTilt,Mathf.SmoothDamp(quad.rotation.y,rotY,ref refRotSide,0.5f) ,sideTilt));
      
      
    }
      
    void MovementUpDown()
    {
      if(joystick2.Vertical > 0.4f)
      verticalForce = 380f;
      if(joystick2.Vertical < 0.4f)
      verticalForce = -250f;
      if(Mathf.Abs(joystick2.Vertical) < 0.4f)
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

      if(Mathf.Abs(joystickDir.Vertical) != 0f && joystickDir.Horizontal != 0f)
      {
          if(joystick2.Vertical == 0 && verticalCheckTime <= 0f)
           quad.velocity = new Vector3(quad.velocity.x,0f,quad.velocity.z);
          
      }

    }

    void MovementForward()
    { 
        if(joystickDir.Vertical != 0f)
        {
            quad.AddForce(new Vector3(transform.forward.x,0f,transform.forward.z) * joystickDir.Vertical  * fwdSpeed);
         }
         fwdTilt = Mathf.SmoothDamp(fwdTilt,25* joystickDir.Vertical,ref fwdTiltVelocity,0.1f);
    }

     void MovementLeftRight()
    {
         if(joystickDir.Horizontal != 0f)
        {
            quad.AddForce(new Vector3(transform.right.x,0f,transform.right.z) * joystickDir.Horizontal * sideSpeed);
        }
         sideTilt = Mathf.SmoothDamp(sideTilt,-25* joystickDir.Horizontal,ref sideTiltVelocity,0.1f);
    }

    void RotationLeftRight()
    {   if(Mathf.Abs(joystick2.Horizontal) > 0.4f)
      { rotY += 1f * joystick2.Horizontal * rotSpeed;
      // transform.Rotate(0f,0f,0f);
      }    

    }

    void ClampingSpeed()
    {
        if(Mathf.Abs(joystickDir.Vertical)> 0.2f || Mathf.Abs(joystickDir.Horizontal) > 0.2f )
        quad.velocity = Vector3.ClampMagnitude(quad.velocity,Mathf.Lerp(quad.velocity.magnitude,10f,Time.deltaTime * 5f));
         if(Mathf.Abs(joystickDir.Vertical) < 0.2f && Mathf.Abs(joystickDir.Horizontal) < 0.2f )
         quad.velocity = Vector3.SmoothDamp(quad.velocity,Vector3.zero,ref velocityToSmoothDampToZero,0.4f);
    }

    void Hover()
    {   timerHover -= Time.deltaTime;
        if(timerHover <= 0f)
        {
        hover = new Vector3(Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f));
        timerHover = 0.5f;
        }
        if(joystickDir.Horizontal == 0f && joystickDir.Vertical == 0f && joystick2.Vertical == 0f)
        quad.velocity = new Vector3(Mathf.SmoothDamp(quad.velocity.x,hover.x, ref refHoverX, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.y,hover.y,ref refHoverY, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.z,hover.z, ref refHoverZ, 55f * Time.deltaTime));
        
        
    }

    void Flip()
    {
    //  quad.AddForce(quad.velocity.normalized * Time.deltaTime * flipForce);
    }

    void SmoothVelocityCheck()
    {
        if(joystick2.Vertical == 0f)
    {
        verticalCheckTime -= Time.deltaTime;
    }
    if(joystick2.Vertical != 0f)
    {
        verticalCheckTime = 1.5f;
    }
    }

    void OnCollisionEnter()
    {
        quad.angularVelocity = Vector3.zero;
    }

    void OnCollisionStay()
    {
        quad.angularVelocity = Vector3.zero;
    }

}
