using System;
using Proyecto26;
using UnityEngine;

public class DatabaseHandler {

    private const string projectId = "crazycookoff-1fa14"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    public delegate void PostUserCallback();
    public delegate void Test_ScoreCallback();
    public delegate void GetUserCallback(User user);

    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PostUser(User user, string userId, PostUserCallback callback) {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json", user).Then(response => {
            Debug.Log("The user was successfully uploaded to the database");
        });

    }

    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetUser(string userId, GetUserCallback callback) {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json").Then(user => 
        {
            callback(user);
        });
    }


    /// <param name="score"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the score is successfully uploaded </param>
    public static void Test_IncreaseScore(int score) {

        User test_user = new User("dummy", "surdummy", 20);

        string string_score = score.ToString();
        Debug.Log("test increase score");

        Test_DBData data = new Test_DBData("title", "body");

        RestClient.Put<Test_DBData>($"{databaseURL}me/{string_score}.json", data).Then(response => {
            Debug.Log("The user was successfully uploaded to the database");
        });

    }

}
