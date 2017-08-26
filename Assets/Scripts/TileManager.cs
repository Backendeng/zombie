﻿using System.Collections.Generic;
using UnityEngine;

public class TileManager: Singleton<TileManager> {

    public Dictionary<int, Tile> buildTiles;
    //FFD452FF

    private void Start() {
        buildTiles = new Dictionary<int, Tile>();
        var buildTilesGameObjects = GameObject.FindGameObjectsWithTag("BuildTile");
        foreach(var tile in buildTilesGameObjects) {
            buildTiles.Add(tile.GetHashCode(), tile.GetComponent<Tile>());
        }
    }

    void Update() {

    }


    public void MarkAvailableBuildTiles() {
        foreach(var tile in buildTiles.Values) {
            if(tile.tag == "BuildTile") {
                tile.GetComponent<SpriteRenderer>().color = new Color(0.929f, 0.850f, 0.670f);
            }
        }
    }

    public void UnmarkAvailableBuildTiles() {
        foreach(var tile in buildTiles.Values) {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            if(tile.tag == "BuildTileFull" && !tile.IsInUse) {
                tile.IsInUse = true;
            }
        }
    }
}