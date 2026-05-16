using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FirebaseExample : MonoBehaviour
{
    private DatabaseReference _database;

    private void Start()
    {
        string name = "Klara";
        string userID = name + "_id";
        int score = 80;

        _database = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance.GetReference("users").ChildChanged += OnUserChildChanged;

        WriteUserInDatabase(_database, userID, name, score);
        //ReadUserName(_database, userID);
        //ReadUserData(_database, userID);
        //ReadAllData(_database);
        //UpdateUserName(_database, userID, "Gertruda_id");
        UpdateUserScore(_database, userID, 100);
    }

    private void OnUserChildChanged(object sender, ChildChangedEventArgs args)
    {
        if(args.Snapshot != null)
        {
            DataSnapshot snapshot = args.Snapshot;
            Debug.Log($"Data in {snapshot.Key} updated, not it is {snapshot.GetRawJsonValue()}");
            //foreach (var Child in snapshot.Children)
            //{
            //    Debug.Log(Child.Key);
            //    Debug.Log(Child.Value);
            //}
        }
        else if(args.DatabaseError != null) 
            Debug.Log(args.DatabaseError.Message);
    }

    private void ReadAllData(DatabaseReference databaseReference)
    {
        databaseReference.Child("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach(DataSnapshot child in snapshot.Children)
                {
                    User userData = JsonUtility.FromJson<User>(child.GetRawJsonValue());
                    Debug.Log(JsonUtility.ToJson(userData));
                }
            }
        });
    }

    private void WriteUserInDatabase(DatabaseReference databaseReference, string userID, string name, int score)
    {
        User user = new(name,score);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("users").Child(userID).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
        if (task.IsFaulted)
            {
                Debug.Log("ErrorOfWriting " + task.Exception);
            }
        else if(task.IsCompleted) { Debug.Log("record is done"); }
        });
    }

    private void ReadUserName(DatabaseReference databaseReference, string userID)
    {
        databaseReference.Child("users").Child(userID).Child("name").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                Debug.Log(snapShot.Value.ToString());
            }
            else if (task.IsFaulted)
            {
                Debug.Log(task.Exception.Message);
            }
        });
    }

    private void ReadUserData(DatabaseReference databaseReference, string userID)
    {
        databaseReference.Child("users").Child(userID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                User userData = JsonUtility.FromJson<User>(snapShot.GetRawJsonValue());
                Debug.Log(JsonUtility.ToJson(userData).ToString());
            }
            else if (task.IsFaulted)
            {
                Debug.Log(task.Exception.Message);
            }
        });
    }

    private void UpdateUserName(DatabaseReference databaseReference, string userID, string newName)
    {
        databaseReference.Child("users").Child(userID).Child("name").SetValueAsync(newName).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception.Message);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("name is changed");
            }
        });
    }

    private void UpdateUserScore(DatabaseReference databaseReference, string userID, int newScore)
    {
        databaseReference.Child("users").Child(userID).Child("score").SetValueAsync(newScore).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Score is changed");
            }
            else if (task.IsFaulted)
            {
                Debug.Log(task.Exception.Message);
            }
        });
    }
}
