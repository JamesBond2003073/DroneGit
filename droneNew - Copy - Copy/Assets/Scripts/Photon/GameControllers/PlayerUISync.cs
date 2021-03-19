using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class PlayerUISync : MonoBehaviour
{
     private PhotonView PV;
     public float uiHeight = 546f;
     public int localFlag = 0;
     public Sprite imgTick;
     public Sprite imgCross;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
         
           // if(PhotonNetwork.IsMasterClient)
             //PV.RPC("RPC_SyncUICanvas", RpcTarget.AllBuffered);

             PV.RPC("RPC_SyncPlayerUI", RpcTarget.AllBuffered, PhotonLobby.nameFieldSave.text.ToString());
        }
    }
    void Update()
    {   if(PV.IsMine)
    {
        if(ReadySync.flag == 1 && localFlag == 0)
        { 
               PV.RPC("RPC_SyncReady", RpcTarget.AllBuffered);
               localFlag = 1;
        }
        else if(ReadySync.flag == 0 && localFlag == 1)
        {
            PV.RPC("RPC_SyncReadyNot", RpcTarget.AllBuffered);
            localFlag = 0;
        }
    }
    }

 [PunRPC]
   public void RPC_SyncUICanvas()
   {        
    
             GameObject Canvas1 = new GameObject("Canvas1");
            Canvas canvas =  Canvas1.AddComponent<Canvas>();
             canvas.renderMode = RenderMode.ScreenSpaceOverlay;
             CanvasScaler cs = Canvas1.AddComponent<CanvasScaler>();
             cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
             cs.matchWidthOrHeight  = 0.5f;
           
   } 

   [PunRPC]
   public void RPC_SyncPlayerUI(string fieldText)
   {
            
            transform.parent = GameObject.Find("Canvas1").transform;
            transform.GetChild(0).transform.GetChild(0).transform.gameObject.GetComponent<TextMeshProUGUI>().text = fieldText;
            
   } 

   [PunRPC]
   public void RPC_SyncReady()
   {
       transform.GetChild(1).GetComponent<Image>().sprite = imgTick;
   }

   [PunRPC]
   public void RPC_SyncReadyNot()
   {
       transform.GetChild(1).GetComponent<Image>().sprite = imgCross;
   }  
}
