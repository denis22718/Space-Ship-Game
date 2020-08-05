using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _id = 1;
    [SerializeField] private float _asteroidSpawnInterval = 0.8f;
    [SerializeField] private float _asteroidsSpeed = 0.05f;
    [SerializeField] private int _scoreForWin = 100;

    public int ID { get => _id; }
    public int ScoreForWin { get => _scoreForWin;}
    public float AsteroidSpawnInterval { get => _asteroidSpawnInterval;}
    public float AsteroidsSpeed { get => _asteroidsSpeed;}
}
