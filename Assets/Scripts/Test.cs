using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text OwnerObject;
    private List<string> Charactors = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            var fileName = "sample.db";


            var db = new SqliteDatabase(fileName);
            var query = db.ExecuteQuery("SELECT * FROM charactors");

            foreach (var row in query.Rows)
            {
                var id = row["id"];
                var name = row["name"];
                var data1 = row["data1"];
                var data2 = row["data2"];

                var text = $"ID:{id}, Name:{name}, DATA1:{data1}, DATA2:{data2}";

                Charactors.Add(text);
            }

        } 
        catch (Exception ex)
        {
            Charactors.Add(ex.Message);
        }
    }

    private IEnumerator InitializeDB(string fileName)
    {
        var mementoDB = $"{Application.streamingAssetsPath}/{fileName}";
        var gameDB = $"{Application.persistentDataPath}/{fileName}";

        var www = UnityWebRequest.Get(mementoDB);

        yield return www.SendWebRequest();

        if (!www.isNetworkError)
        {
            if (!System.IO.File.Exists(gameDB))
            {
                System.IO.File.WriteAllBytes(gameDB, www.downloadHandler.data);
            }
        }
        else
        {
            throw new Exception(www.error);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OwnerObject != null && Charactors.Count > 0)
        {
            string text = "";
            
            if (Charactors.Count > 0)
            {
                text = string.Join("\r\n", Charactors);
            }
            else
            {
                text = "キャラクターが存在しません";
            }

            OwnerObject.text = text;
        }
    }
}
