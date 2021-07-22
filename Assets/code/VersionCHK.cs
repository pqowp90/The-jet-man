using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class VersionCHK : MonoBehaviour
{
    public string URL = ""; // 버전체크를 위한 URL
    public string StoreURL="";
    public string CurVersion; // 현재 빌드버전
    string latsetVersion; // 최신버전
    public GameObject newVersionAvailable; // 버전확인 UI
    public GameObject loading;

    void Start()
    {
        StartCoroutine(LoadTxtData(URL));
    }

    public void VersionCheck()
    {
        if (CurVersion != latsetVersion)
        {
            newVersionAvailable.SetActive(true);
        }
        else
        {
            newVersionAvailable.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
        loading.SetActive(false);
        Debug.Log("Current Version" + CurVersion + "Lastest Version" + latsetVersion);
        }


    IEnumerator LoadTxtData(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest(); // 페이지 요청
        if (URL=="123")//www.isNetworkError
        {
            Debug.Log("error get page");
        }
        else
        {
            latsetVersion = www.downloadHandler.text; // 웹에 입력된 최신버전
        }
        VersionCheck();
    }

    public void OpenURL() // 스토어 열기
    {
        Application.OpenURL(StoreURL);
    }
}
