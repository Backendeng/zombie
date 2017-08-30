﻿using UnityEngine;

public abstract class PlayerSoldier: MonoBehaviour {

    [SerializeField] private short rank;
    [SerializeField] private int price;
    [SerializeField] private float offset_x;
    [SerializeField] private float offset_y;

    private StrategyEditor strategyEditor;
    protected Vector3 originPosition;
    protected Animator anim;
    protected PolygonCollider2D playerCollider;

    public short Rank { get { return rank; } }
    public int Price { get { return price; } }
    public float OffsetX { get { return offset_x; } }
    public float OffsetY { get { return offset_y; } }
    public Tile CurrentTile { get; set; }
    public GameSide CurrentSide { get; set; }
    public Animator Anim { get { return anim; } }
    public PolygonCollider2D PlayerCollider { get { return playerCollider; } }


    private void Awake() {
        originPosition = transform.position;
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<PolygonCollider2D>();
        strategyEditor = StrategyEditor.Instance;
    }

    public void FlipSide() {
        offset_x = -offset_x;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        CurrentSide = CurrentSide == GameSide.LeftSide ? GameSide.RightSide : GameSide.LeftSide;
    }

    public bool IsEnemy(PlayerSoldier enemy) {
        return CurrentSide != enemy.CurrentSide;
    }

    protected void OnMouseDown() {
        if(strategyEditor.PlayerBtnPressed == null && StrategyEditor.IsInEdit) {
            TileManager.Instance.MarkAvailableBuildTiles();
        }
    }

    protected void OnMouseDrag() {
        if(strategyEditor.PlayerBtnPressed == null && StrategyEditor.IsInEdit) {
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    protected void OnMouseUp() {
        if(strategyEditor.PlayerBtnPressed == null && StrategyEditor.IsInEdit) {
            TileManager.Instance.UnmarkAvailableBuildTiles();
            if(strategyEditor.ChangeSoldierPosition(this)) {
                originPosition = transform.position;
            }
            else {
                transform.position = originPosition;
            }
        }
    }
}