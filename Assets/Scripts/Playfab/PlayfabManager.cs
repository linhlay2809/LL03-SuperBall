using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Playfab
{
    public class PlayfabManager : MonoBehaviour
    {
        private static PlayfabManager _instance;
        public static PlayfabManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                return;
            }
            _instance = this;
        }

        public Task Login(GameObject changeNameUI)
        {
            PlayFabClientAPI.LoginWithCustomID( new LoginWithCustomIDRequest {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            }, async resultCallback =>
            {
                string name = null;
                if (resultCallback.InfoResultPayload.PlayerProfile != null)
                {
                    await Task.Run(() =>
                    {
                        name = resultCallback.InfoResultPayload.PlayerProfile.DisplayName;
                    });
                
                }

                if (name == null)
                {
                    changeNameUI.SetActive(true);
                }
                else
                {
                    changeNameUI.SetActive(false);
                }

                Debug.Log("Login Success");
            }, error => Debug.LogError(error.GenerateErrorReport()));
            return Task.CompletedTask;
        }
        public async Task UpdateDisplayName(string name) {
            PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
                DisplayName = name
            }, result => {
                Debug.Log("The player's display name is now: " + result.DisplayName);
            }, error => Debug.LogError(error.GenerateErrorReport()));
            await Task.Yield();
        }
    
        public void SendLeaderboard(int score)
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
    
    
    }
}
