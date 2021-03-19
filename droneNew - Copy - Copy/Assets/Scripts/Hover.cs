using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
   public float timerHover = 0.3f;
    Vector3 hover;
    private float refHoverX;
    private float refHoverY;
    private float refHoverZ;
    public Rigidbody quad;
   
    void Update()
    {
       // HoverSscript();
    }
     void HoverSscript()
    {   timerHover -= Time.deltaTime;
        if(timerHover <= 0f)
        {
        hover = new Vector3(Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f),Random.Range(-3.1f,3.1f));
        timerHover = 0.5f;
        }
        
        quad.velocity = new Vector3(Mathf.SmoothDamp(quad.velocity.x,hover.x, ref refHoverX, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.y,hover.y,ref refHoverY, 55f * Time.deltaTime),Mathf.SmoothDamp(quad.velocity.z,hover.z, ref refHoverZ, 55f * Time.deltaTime));
        
        
    }
}
