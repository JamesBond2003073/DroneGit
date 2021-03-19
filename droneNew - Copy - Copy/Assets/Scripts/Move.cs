using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{ 
    public Vector3 end1;
    public Vector3 end2;
    public int flag = 0;
    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
         if(this.gameObject.tag == "HorizontalObs")
    {
        end1 = new Vector3(transform.localPosition.x,transform.localPosition.y,end1.z);
         end2 = new Vector3(transform.localPosition.x,transform.localPosition.y,end2.z);
    }

         if(this.gameObject.tag == "VerticalObs")
         {
             end1 = new Vector3(transform.localPosition.x,end1.y,transform.localPosition.z);
         end2 = new Vector3(transform.localPosition.x,end2.y,transform.localPosition.z);
         }


    }
    // Update is called once per frame
    void LateUpdate()
    { 
        if(this.gameObject.tag == "HorizontalObs")
      {

        if(flag == 0)
        {
         // transform.position = Vector3.Lerp(transform.position,end2, 0.05f);
         transform.localPosition = Vector3.Lerp(transform.localPosition,end2,movementSpeed * Time.deltaTime);
          if(transform.localPosition.z > (end2.z - 0.1f))
          {
          flag = 1;
          }
        }
     
        if(flag == 1)
        {
            // transform.localPosition = Vector3.Lerp(transform.localPosition,end1, 0.05f);
             transform.localPosition = Vector3.Lerp(transform.localPosition,end1,movementSpeed * Time.deltaTime);
          if(transform.localPosition.z < (end1.z + 0.1f))
          flag = 0;
        }

      }

      else if(this.gameObject.tag == "VerticalObs")
      {

        if(flag == 0)
        {
         // transform.position = Vector3.Lerp(transform.position,end2, 0.05f);
         transform.localPosition = Vector3.Lerp(transform.localPosition,end2,movementSpeed * Time.deltaTime);
          if(transform.localPosition.y > (end2.y - 0.1f))
          {
          flag = 1;
          }
        }
     
        if(flag == 1)
        {
            // transform.localPosition = Vector3.Lerp(transform.localPosition,end1, 0.05f);
             transform.localPosition = Vector3.Lerp(transform.localPosition,end1,movementSpeed * Time.deltaTime);
          if(transform.localPosition.y < (end1.y + 0.1f))
          flag = 0;
        }

      }  
    }
}
