using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public event EventHandler<IEnumerable<string>> OccurredSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertSQL()
    {
        Debug.Log("Insert Called.");
        SqlTester.Insert();
    }

    public void UpdateSQL()
    {
        Debug.Log("Update Called.");
        SqlTester.Update();
    }

    public void SelectSQL()
    {
        Debug.Log("Select Called.");
        var charactors = SqlTester.Select();

        OccurredSelect.Invoke(this, charactors);
    }
}
