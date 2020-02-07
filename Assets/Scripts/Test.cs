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
    private string ExceptionText = "";

    // Start is called before the first frame update
    void Start()
    {
        var test = GameObject.Find("SelectButton").GetComponent<TestButton>();

        test.OccurredSelect += (owner, args) => 
        {
            Debug.Log($"Called OccurredSelect Event at {nameof(Test)} Class.");

            var charactors = SqlTester.Select();

            AddCharactors(charactors, true);
        };

        try
        {
            var charactors = SqlTester.Select();

            AddCharactors(charactors, true);
        }
        catch (Exception ex)
        {
            ExceptionText = ex.Message;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OwnerObject == null) return;

        string text = "";

        if (ExceptionText == "")
        {
            if (Charactors.Count > 0)
            {
                text = string.Join("\r\n", Charactors);
            }
            else
            {
                text = "キャラクターが存在しません";
            }
        }
        else
        {
            text = ExceptionText;
        }

        OwnerObject.text = text;

    }

    private void AddCharactors(IEnumerable<string> charactors, bool isReset)
    {
        if (isReset)
        {
            Charactors.Clear();
        }

        foreach (var chara in charactors)
        {
            Charactors.Add(chara);
        }
    }
}
