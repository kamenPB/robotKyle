using System;

[Serializable]

public class Test_DBData {

    public string title;
    public string body;

    public Test_DBData(string title, string body) {
        this.title = title;
        this.body = body;
    }

    public override string ToString() {
        return UnityEngine.JsonUtility.ToJson(this, true);
    }
}

