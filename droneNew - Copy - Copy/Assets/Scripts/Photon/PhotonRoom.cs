using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom room;
    private PhotonView PV;
    private GameObject player;
    private GameObject playerUI;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;
    public GameObject ball3Prefab;
    public GameObject ball4Prefab;
    public GameObject ball5Prefab;
    public GameObject ball6Prefab;
    public GameObject ball7Prefab;
    public GameObject ball8Prefab;
    public GameObject ball9Prefab;
    public GameObject ball10Prefab;
    public float uiHeight = 546f;
    

   // public bool isGameLoaded;
    public int currectScene;
    public int MultiplayerScene;

    /*
    // Player info
    Player[] photonPlayers;

    public int playersInRoom;
    public int myNumberInRoom;
    public int playersInGame;
    */

    private void Awake()
    {
     if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
     else
        {
            if(PhotonRoom.room != this)
            {
                UnityEngine.Object.Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }

          DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {

    }

   
    public override void OnEnable()
    {
        //subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Now Inside a room");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();

       /* photonPlayers = PhotonNetwork.PlayerList;
         playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        */

    }

    void StartGame()
    {
        Debug.Log("Loading Level");
       PhotonNetwork.LoadLevel(MultiplayerScene);
    }

   
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currectScene = scene.buildIndex;
        if(currectScene == MultiplayerScene)
        {
            CreatePlayer();
        }
       
    }

    private void CreatePlayer()

    {
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer1"), transform.position, Quaternion.identity, 0);
        
        
        
        if(PhotonNetwork.IsMasterClient)
       {
           PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Obstacle_ballsnew 1"), new Vector3(-158.7f,141.7f,-118f), Quaternion.identity, 0);
          
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball1"), ball1Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball2"), ball2Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball3"), ball3Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball4"), ball4Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball5"), ball5Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball6"), ball6Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball7"), ball7Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball8"), ball8Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
            //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball9"), ball9Prefab.transform.position, Quaternion.Euler(-90f,90f,0f), 0);
           //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "ball10"), ball10Prefab.transform.position, ball10Prefab.transform.rotation, 0);
           
            PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "HoopLoop1"), new Vector3(-126.6f,79.7f,67.4f), Quaternion.identity, 0);
            PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "HoopLoop2"), new Vector3(-144.2f,79.7f,-234.2f), Quaternion.Euler(0f,180f,0f), 0);
            PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ObstaclePattern"), new Vector3(-41.6f,25.7f,-24f), Quaternion.Euler(0f,45f,0f), 0);
            //PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Checkpoint"), new Vector3(-134.8f,99.8f,-169f), Quaternion.identity, 0);
             //PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Checkpoint"), new Vector3(-134.8f,99.8f,-4.4f), Quaternion.identity, 0);

           
            
            
       } 

        
        

    }

    
  
}