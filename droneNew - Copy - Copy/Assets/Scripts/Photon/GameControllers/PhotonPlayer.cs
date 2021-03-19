using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class PhotonPlayer : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    public GameObject myAvatar;
    public GameObject prefab;
    public PhotonView PVPlayer;
    public Sprite tickSprite;
    public float uiHeight = 546f;
    public int readyCount = 0;
    public int startFlag = 0;
    private float delay = 0.6f;
    public GameObject count3;
    public GameObject count2;
    public GameObject count1;
    public int countdownAnimCounter = 0;
    public List<Vector3> startPosList;
    
  
    // Start is called before the first frame update
    void Start()
    {  count3 = GameObject.Find("Canvas").transform.GetChild(1).transform.gameObject;
    count2 = GameObject.Find("Canvas").transform.GetChild(2).transform.gameObject;
    count1 = GameObject.Find("Canvas").transform.GetChild(3).transform.gameObject;

        startPosList.Add(new Vector3(41f,8.6f,12.1f));
        startPosList.Add(new Vector3(43.4f,8.6f,16.7f));
        startPosList.Add(new Vector3(38.6f,8.6f,7.34f));
        startPosList.Add(new Vector3(36.2f,8.6f,2.6f));
        startPosList.Add(new Vector3(45.7f,8.6f,21.3f));

        
        tickSprite = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<ReadySync>().imgTick;
       PV = GetComponent<PhotonView>(); 
       
        if(PV.IsMine)
        {    int spawnPicker = Random.Range(0,GameSetup.GS.spawnPoints.Length);
            if(Select.selectNo == 1)
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0); 
             else if(Select.selectNo == 2)
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar1"), GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);        
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerUI1"),new Vector3(275f,uiHeight - (PhotonNetwork.CurrentRoom.PlayerCount-1)*50f,0f) , Quaternion.identity, 0);
        }
    }

    void Update()
    {  
          // MASTER COMMANDS

        if(PhotonNetwork.IsMasterClient && startFlag == 0)
     {
        foreach (Transform child in GameObject.Find("Canvas1").transform)
    {
       if(child.transform.GetChild(1).GetComponent<Image>().sprite == tickSprite)
       readyCount ++;   
    }  
    if(readyCount == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
    {
        Debug.Log("Start");
        PV.RPC("RPC_Start", RpcTarget.All);
      readyCount = 0;
        
    }
    else {
     readyCount = 0;
    }
     } 
 
    // COUNTDOWN

     if(startFlag == 1 && countdownAnimCounter <= 2)
     {
           delay -= Time.deltaTime;
           if(delay <= 0f)
           {  
                if(countdownAnimCounter == 0)
            {
               count3.SetActive(true);
               if(count3.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
               {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
                  countdownAnimCounter ++;
               }
             
            }
           else if(countdownAnimCounter == 1)
            {
               count2.SetActive(true);
               if(count2.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
               {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
                  countdownAnimCounter ++;
               }
             
            }
           else if(countdownAnimCounter == 2)
            {
               count1.SetActive(true);
               if(count1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
               {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
                  countdownAnimCounter ++;
                  foreach(Transform child in GameObject.Find("PlayersParent").transform)
                  {
                      child.GetComponent<QuadMovementPC>().startFlag = 0;
                    //  child.transform.localRotation = Quaternion.Euler(new Vector3(0f,-63f,0f));
                  }
               }
             
            }

           }
     }

    }

[PunRPC]
   public void RPC_Start()
   {  int i = 0;
        startFlag = 1;
       foreach(Transform child in GameObject.Find("PlayersParent").transform)
       {    child.GetComponent<QuadMovementPC>().startFlag = 1;
            child.transform.position = startPosList[i];
            //child.transform.localRotation = Quaternion.Euler(new Vector3(0f,-63f,0f));
            child.GetComponent<QuadMovementPC>().rotY = -1598f;
           
           // child.transform.rotation.eulerAngles = new Vector3(0f,-63f,0f);
            i++; 
          // child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
       }
   }
   

}
