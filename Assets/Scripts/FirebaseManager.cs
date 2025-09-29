using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    private DatabaseReference dbReference;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("? Firebase conectado correctamente.");
            }
            else
            {
                Debug.LogError("?? Error al conectar Firebase: " + task.Result);
            }
        });
    }

    //  Guardar disparo
    public void SaveShot(ShotResult shotResult)
    {
        string key = dbReference.Child("shots").Push().Key;
        string json = JsonUtility.ToJson(shotResult);
        dbReference.Child("shots").Child(key).SetRawJsonValueAsync(json);
    }

    //  Leer disparos
    public void GetAllShots(Action<bool, List<ShotResult>> callback)
    {
        dbReference.Child("shots").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                callback(false, null);
                return;
            }

            DataSnapshot snapshot = task.Result;
            List<ShotResult> results = new List<ShotResult>();

            foreach (DataSnapshot child in snapshot.Children)
            {
                string json = child.GetRawJsonValue();
                ShotResult result = JsonUtility.FromJson<ShotResult>(json);
                results.Add(result);
            }

            results.Sort((a, b) => b.timestamp.CompareTo(a.timestamp));

            callback(true, results);
        });
    }
}