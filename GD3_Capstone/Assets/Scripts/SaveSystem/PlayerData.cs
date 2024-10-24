using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {
    public string username;
    public PlayerPosition playerPosition;

    public PlayerData(string username, Vector3 position) {
        this.username = username;
        this.playerPosition = new PlayerPosition(position);
    }
}

[Serializable]
public class PlayerPosition {
    public float x, y, z;

    public PlayerPosition(Vector3 position) {
        x = position.x;
        y = position.y;
        z = position.z;
    }

    public Vector3 GetPosition() {
        return new Vector3(x, y, z);
    }
}