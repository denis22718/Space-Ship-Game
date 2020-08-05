using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private int _lifes = 1;
    [SerializeField] private int _scoreValue = 1;
    [SerializeField] private float _lifeTime = 7.5f;

    [SerializeField] private int _damage = 1;

    private Coroutine _lifeTimeCoroutine = null;
    private int _lifesDefault = 1;

    public int Damage { get => _damage;}

    private void Start()
    {
        _lifesDefault = _lifes;

        _speed = SaveManager.Instance.CurrentLevelData.AsteroidsSpeed;
    }

    private void OnEnable()
    {
        if (_lifeTimeCoroutine != null)
            StopCoroutine(_lifeTimeCoroutine);

        _lifes = _lifesDefault;

        StartCoroutine(LifeTimeRoutine());
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, -_speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Hit(other.GetComponent<Bullet>().Damage);
            other.gameObject.SetActive(false);
        }
    }

    public void Hit(int damage)
    {
        _lifes -= damage;
        if (_lifes <= 0)
        {
            _lifes = 0;
            GameManager.Instance.Score += _scoreValue;
            PoolController.Instance.particlesAsteroidExplosion.PlayParticles(transform.position);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }
}
