using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

   public static PhotonLobby lobby;
   public GameObject battleButton;
   public GameObject cancelButton;
   public GameObject offlineButton;
   public TextMeshProUGUI nameField;
   public static TextMeshProUGUI nameFieldSave;
   private LoadBalancingClient lbc;
   public GameObject textObject;
   public int count = 0;
   public bool atLeastOneNotspace = false;
   public string nameString;
   public Color readyColor;
   public Color battleHighlitedColor;
   public Color battleTextColor;
   public ColorBlock battleBlk;

   private void Awake()
   {
       lobby = this;
   }

    // Start is called before the first frame update
    void Start()
    {    battleBlk = battleButton.GetComponent<Button>().colors;
        //this.lbc = new LoadBalancingClient();
       // this.lbc.ConnectToRegionMaster("in");
        PhotonNetwork.ConnectUsingSettings();
    
    }

    void Update()
    {  nameString = nameField.text;
        if(Select.selectNo != 100 && nameString.Length >= 2)
        {    
              battleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = battleTextColor;
              battleBlk.normalColor = readyColor;
              battleBlk.highlightedColor = battleHighlitedColor;
              battleButton.GetComponent<Button>().colors = battleBlk;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to Photon server");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
        offlineButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void OnBattleButtonClicked()
    {   nameString = nameField.text;
    Debug.Log(nameString);
   
       if(Select.selectNo != 100 && nameString.Length >= 2)
       {  
           Debug.Log(atLeastOneNotspace);
       nameFieldSave = nameField;
         Debug.Log("Searching for a room");
        PhotonNetwork.JoinRandomRoom();
        battleButton.SetActive(false);
        offlineButton.SetActive(false);
        cancelButton.SetActive(true);
       }
    }
     
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joining random room failed");
        CreateRoom();
    }

    void CreateRoom()
    {   Debug.Log("Creating a room");

       int randomRoomName = Random.Range(0,10000);
        RoomOptions roomOps = new RoomOptions() {IsOpen = true, IsVisible = true, MaxPlayers = 10};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Successfully");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
       Debug.Log("Creating a room failed");
       CreateRoom();
    }

    public void OnCancelButtonClicked()
    {   Debug.Log("Cancelling Matchmaking");

        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
