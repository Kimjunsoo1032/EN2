﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Prefabs")]
    private Explosion explosionPrefab_;
    private Camera mainCamera_;
    [SerializeField]
    private Meteor meteorPrefab_;
    [SerializeField, Header("MeteorSpawner")]
    private BoxCollider2D ground_;
    [SerializeField]
    private float meteorInterval_ = 1;

    private float meteorTimer_;
    public void AddScore(int point) { }
    public void Damage(int point) { }

    [SerializeField]
    private List<Transform> spawnPositions_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        bool isGetComponent = mainCameraObject.TryGetComponent(out mainCamera_);
        Assert.IsTrue(isGetComponent, "MainCameraにCameraコンポーネントがありません");
        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_に要素が一つもありません");
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(
                t,
                "spawnPositions_にNUllが含まれています"
                );
        }
    }
    private void GenerateExplosion()
    {
        Vector3 clickPosition = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        Explosion explosion = Instantiate(
            explosionPrefab_,
            clickPosition,
            Quaternion.identity
            );
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { GenerateExplosion(); }
        UpdateMeteorTimer();
    }
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }
    private void GenerateMeteor()
    {
        int max = spawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Debug.Log(posIndex);
        Vector3 spawnPosition = spawnPositions_[posIndex].position;

        Meteor meteor = Instantiate(meteorPrefab_,
            spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }

}
