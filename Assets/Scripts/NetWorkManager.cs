using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkManager : Photon.PunBehaviour {

    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
    public List<PhotonPlayer> currentPlayersInRoom = new List<PhotonPlayer>();

    private void Start()
    {

        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings("0.1");

    }

    private void OnGUI()
    {

        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if(PhotonNetwork.room == null)
        {
            //Luodaan huone (CREATE)
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            {
                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
            }

            //Liitytään huoneeseen (JOIN)

            if(roomsList != null)
            {

                for (int i = 0; i < roomsList.Length; i++)
                {

                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].Name))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].Name);
                    }

                }

            }



        }

    }

    public override void OnReceivedRoomListUpdate()
    {

        roomsList = PhotonNetwork.GetRoomList();

    }

    private void OnConnectedToServer()
    {

        Debug.Log("Yhdistettiin serveriin");

    }

    public override void OnJoinedLobby()
    {

        Debug.Log("Tultiin lobbyyn eli aulaan");

    }

    public override void OnJoinedRoom()
    {

        Debug.Log("Mentiin huoneeseen pelaamaan");
        // Instansioi pelaaja, huom Network Pelaaja.

        GameObject player = PhotonNetwork.Instantiate("PlayerBox", new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
        
    }

    private void OnPlayerDisconnected(NetworkCharacter player)
    {
        
        

    }

    public override void OnCreatedRoom()
    {



    }



}
