using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/export?format=tsv";

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
