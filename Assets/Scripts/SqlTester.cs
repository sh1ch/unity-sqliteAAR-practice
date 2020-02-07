﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqlTester
{
    private static string _fileName = "sample.db";

    public static void Insert()
    {
        try
        {
            var db = new SqliteDatabase(_fileName);

            var rand = UnityEngine.Random.Range(2, 100);
            var name = $"athena No.{rand}";
            var data1 = rand;
            var data2 = "arrow";

            db.ExecuteQuery($"INSERT INTO charactors (name, data1, data2) VALUES ('{name}', {data1}, '{data2}')");
        }
        catch
        {
            throw;
        }
    }

    public static void Update()
    {
        try
        {
            var db = new SqliteDatabase(_fileName);

            var power = UnityEngine.Random.Range(100, 1000);

            db.ExecuteQuery($"UPDATE charactors SET data1 = {power} WHERE id = 1");
        }
        catch
        {
            throw;
        }
    }

    public static IEnumerable<string> Select()
    {
        var charactors = new List<string>();

        try
        {
            var db = new SqliteDatabase(_fileName);
            var query = db.ExecuteQuery("SELECT * FROM charactors");

            foreach (var row in query.Rows)
            {
                var id = row["id"];
                var name = row["name"];
                var data1 = row["data1"];
                var data2 = row["data2"];

                var text = $"ID:{id}, Name:{name}, DATA1:{data1}, DATA2:{data2}";

                charactors.Add(text);
            }

        }
        catch
        {
            throw;
        }

        return charactors;
    }
}
