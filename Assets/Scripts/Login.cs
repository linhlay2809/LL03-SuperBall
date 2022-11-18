using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject changeNamePnl;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private Button okBtn;
    // Start is called before the first frame update
    void Start()
    {
        PlayfabManager.Instance.Login(changeNamePnl);
        okBtn.onClick.AddListener(() =>
        {
            ChangeName();
        });
    }

    private void ChangeName()
    {
        PlayfabManager.Instance.UpdateDisplayName(inputFieldName.text);
        changeNamePnl.SetActive(false);
    }
    
}
