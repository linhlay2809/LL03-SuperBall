using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    private string playerId = "3A60D464E263147F";
    public void Start()
    {
        // if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
        //     /*
        //     Please change the titleId below to your own titleId from PlayFab Game Manager.
        //     If you have already set the value in the Editor Extensions, this can be skipped.
        //     */
        //     PlayFabSettings.staticSettings.TitleId = "42";
        // }
        // var request = new LoginWithCustomIDRequest
        // {
        //     CustomId = "GettingStartedGuide", 
        //     CreateAccount = true
        // };
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        
        Invoke("LoginUser",1f);
        // StartCoroutine(SetLeaderboard(5));
        Invoke("GetLeaderboard",10f);
    }

    IEnumerator SetLeaderboard(int score)
    {
        yield return new WaitForSeconds(4f);
        SendLeaderboard(score);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
    
    public void ClientGetTitleData() {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result => {
                if(result.Data == null || !result.Data.ContainsKey("Score")) Debug.Log("No MonsterName");
                else Debug.Log("Score: "+result.Data["Score"]);
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
    
    void CreatePlayerAndUpdateDisplayName() {
        PlayFabClientAPI.LoginWithCustomID( new LoginWithCustomIDRequest {
            CustomId = "PlayFabGetPlayerProfileCustomId",
            CreateAccount = true
        }, result => {
            Debug.Log("Successfully logged in a player with PlayFabId: " + result.PlayFabId);
            UpdateDisplayName();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    void UpdateDisplayName() {
        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
            DisplayName = "LinhLay123"
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    void GetPlayerProfile() {
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
                PlayFabId = playerId,
                ProfileConstraints = new PlayerProfileViewConstraints() {
                    ShowDisplayName = true
                }
            },
            result => Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName),
            error => Debug.LogError(error.GenerateErrorReport()));
    }
    
    string displayName;

    private Task<string> GetDisplayName(string playfabId)
    {
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
                PlayFabId = playfabId,
                ProfileConstraints = new PlayerProfileViewConstraints() {
                    ShowDisplayName = true
                }
            },
            result =>
            {
                Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName);
                displayName = result.PlayerProfile.DisplayName;
            },
            error => Debug.LogError(error.GenerateErrorReport()));
        return Task.FromResult(displayName);
    }

    void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Rank",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log(obj.Error);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log(obj.Request);
    }

    void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Rank",
            StartPosition = 0,
            MaxResultsCount = 10,
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult obj)
    {
        foreach (var item in obj.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }
    }

    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = "dangquanglinh2809@gmail.com",
            DisplayName = "DangQuangLinh",
            Password = "123456",
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        Debug.Log("Register Success");
    }

    public void LoginUser()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = "dangquanglinh2809@gmail.com",
            Password = "123456"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
}
