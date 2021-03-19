using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ReadySync : MonoBehaviour
{

  
    public GameObject canvas;
    public GameObject UIMine;
    public static int flag = 2;
    public Sprite imgTick;
    public Sprite imgCross;
    public Color readyColor;
    public Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
   public void CallRPC()
    {
        if(flag == 0 || flag == 2)
       {   canvas = GameObject.Find("Canvas1"); 
           transform.GetComponent<Image>().color = readyColor;
           flag = 1;
           foreach (Transform child in canvas.transform)
       {
           if(child.transform.gameObject.GetComponent<PhotonView>().IsMine)
            {
                UIMine = child.transform.gameObject;
                break;
            }
        
       }
          //  PV.RPC("RPC_SyncReady", RpcTarget.AllBuffered);
       }
       else if(flag == 1)
       {
           canvas = GameObject.Find("Canvas1"); 
           transform.GetComponent<Image>().color = defaultColor;
           flag = 0;
           foreach (Transform child in canvas.transform)
       {
           if(child.transform.gameObject.GetComponent<PhotonView>().IsMine)
            {
                UIMine = child.transform.gameObject;
                break;
            }
        
       }
           // PV.RPC("RPC_SyncReadyNot", RpcTarget.AllBuffered);
       }
       
    }

  
}
