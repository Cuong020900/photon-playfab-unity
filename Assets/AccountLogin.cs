using System;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class AccountLogin : MonoBehaviourPunCallbacks
{
    private string _playFabPlayerIdCache;
    public TMP_InputField username;
    public TMP_InputField password;

    private void Start()
    {
        this.username.text = "CuongTapChoi";
        this.password.text = "123456";
    }

    public virtual void Login()
    {
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest
        {
            Username = this.username.text,
            Password = this.password.text
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, this.RequestPhotonToken, this.OnPlayFabError);
    }

    protected virtual void LoginSuccess(LoginResult result)
    {
        Debug.Log("LoginSuccess");
        Debug.Log("PlayFabId:" + result.PlayFabId);
        Debug.Log("SessionTicket:" + result.SessionTicket);
    }

    protected virtual void RegisterError(PlayFabError error)
    {
        string textError = error.GenerateErrorReport();
        Debug.LogWarning(textError);
    }
    
    private void RequestPhotonToken(LoginResult obj) {
        LogMessage("PlayFab authenticated. Requesting photon token...");
        //We can player PlayFabId. This will come in handy during next step
        _playFabPlayerIdCache = obj.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = "5681a853-56af-4f7f-9c51-a734e9f06713"
        }, AuthenticateWithPhoton, OnPlayFabError);
    }

    /*
     * Step 3
     * This is the final and the simplest step. We create new AuthenticationValues instance.
     * This class describes how to authenticate a players inside Photon environment.
     */
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj) {
        LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        //We set AuthType to custom, meaning we bring our own, PlayFab authentication procedure.
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        //We add "username" parameter. Do not let it confuse you: PlayFab is expecting this parameter to contain player PlayFab ID (!) and not username.
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);    // expected by PlayFab custom auth service

        //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        //We finally tell Photon to use this authentication parameters throughout the entire application.
        PhotonNetwork.AuthValues = customAuth;
        // PhotonNetwork.LocalPlayer.NickName = "CuongPro123456";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster() // callback function for when first connection is made
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    private void OnPlayFabError(PlayFabError obj) {
        LogMessage(obj.GenerateErrorReport());
    }

    public void LogMessage(string message) {
        Debug.Log("PlayFab + Photon Example: " + message);
    }
}
