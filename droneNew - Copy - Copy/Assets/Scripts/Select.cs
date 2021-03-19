using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Sprite drone1;
    public Sprite drone2;
    public Sprite drone1Selected;
    public Sprite drone2Selected;
    public static int selectNo = 100;

    void Start()
    {
        
    }

  public void OnClick1()
   {
       button1.GetComponent<Image>().sprite = drone1Selected;
       button2.GetComponent<Image>().sprite = drone2;
       selectNo = 1;
   }
  public void OnClick2()
   {
       button2.GetComponent<Image>().sprite = drone2Selected;
       button1.GetComponent<Image>().sprite = drone1;
       selectNo = 2;
   }
}
