using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;

public class AvatarSetup : MonoBehaviour
{
    public PhotonView PVPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PVPlayer = GetComponent<PhotonView>();
        	if(PVPlayer.IsMine)
			{
			    PVPlayer.RPC("RPC_SyncName", RpcTarget.AllBuffered, PhotonLobby.nameFieldSave.text.ToString());
			}
    }

     [PunRPC]
   public void RPC_SyncName(string fieldText)
   {  
       
     //   myAvatar = Instantiate(prefab, GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation );
			
	  transform.parent = GameObject.Find("PlayersParent").transform;
      transform.GetChild(2).transform.GetChild(0).transform.gameObject.GetComponent<TextMeshProUGUI>().text = fieldText;	
      
   }
}
