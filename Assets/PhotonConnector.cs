using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Assertions.Must;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;
    // Start is called before the first frame update
    // Update is called once per frame
    // void Start()
    // {
    //         this.CreateRoom();
    // }
    
    public void CreateRoom()
    {
        // create room
        Debug.Log(this.roomName.text);
        PhotonNetwork.CreateRoom(this.roomName.text);
        PhotonNetwork.LoadLevel("MiniGame");
    }

    public void JoinRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinRoom(this.roomName.text);
        PhotonNetwork.LoadLevel("MiniGame");
    }
    
    public void TestCloudScript() {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                GeneratePlayStreamEvent = true,
                FunctionName = "RoomCreated",
                SpecificRevision = 2
            }, exito =>
            {
                Debug.Log("Exito: "+exito.ToString());
            }, error =>
            {
                Debug.Log("Error: "+error.ErrorMessage);
            }
        );
    }
    
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        Debug.Log(message);
    }
    
    public static string RandomString()
    {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
    }
}
