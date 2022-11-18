using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField username;
    public TMP_InputField password;

    private void Start()
    {
        this.email.text = "trancuong.02092000@gmail.com";
        this.username.text = "CuongTapChoi";
        this.password.text = "123456";
    }

    public virtual void SignUp()
    {
        RegisterPlayFabUserRequest userRequest = new RegisterPlayFabUserRequest
        {
            Email = this.email.text,
            Username = this.username.text,
            Password = this.password.text,
            DisplayName = this.username.text,
        };
        
        PlayFabClientAPI.RegisterPlayFabUser(userRequest, this.RegisterSuccess, this.RegisterError);
    }
    
    protected virtual void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("RegisterSuccess");
    }

    protected virtual void RegisterError(PlayFabError error)
    {
        string textError = error.GenerateErrorReport();
        Debug.LogWarning(textError);
    }
}