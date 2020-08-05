using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoSingleton<LevelController>
{
    [Header("Win settings"), Space(10)]
    [SerializeField] private int _maxScore = 100;

    [Header("Player"), Space(10)]
    [SerializeField] private ShipController _playerShip = null;

    [Header("Asteroids"), Space(10)]
    [SerializeField] private float _asteroidSpawnInterval = 0.8f;

    [Header("Bounds of world"), Space(10)]
    [SerializeField] private float[] levelBoundsHorizontal = new float[2];
    [SerializeField] private float[] levelBoundsVertical = new float[2];

    private Coroutine _asteroidsSpawnRoutine = null;

    public int MaxScore { get => _maxScore; private set => _maxScore = value; }
    public ShipController PlayerShip { get => _playerShip; }
    public float[] LevelBoundsHorizontal { get => levelBoundsHorizontal; }
    public float[] LevelBoundsVertical { get => levelBoundsVertical; }

    private void Start()
    {
        _asteroidsSpawnRoutine = StartCoroutine(AsteroidsSpawnRoutine());

        MaxScore = SaveManager.Instance.CurrentLevelData.ScoreForWin;
        _asteroidSpawnInterval = SaveManager.Instance.CurrentLevelData.AsteroidSpawnInterval;
    }

    private IEnumerator AsteroidsSpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_asteroidSpawnInterval);

            GameObject newAsteroid = PoolController.Instance.Asteroids.GetObject();
            newAsteroid.transform.position = new Vector3(Random.Range(LevelBoundsHorizontal[0], LevelBoundsHorizontal[1]),0, LevelBoundsVertical[1] + 2);
        }
    }
}
