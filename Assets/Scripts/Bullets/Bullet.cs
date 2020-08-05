using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 0.6f;
    [SerializeField] private float _lifeTime = 2.5f;

    [SerializeField] private int _damage = 1;

    private Coroutine _lifeTimeCoroutine = null;

    public int Damage { get => _damage; }

    private void OnEnable()
    {
        if (_lifeTimeCoroutine != null)
            StopCoroutine(_lifeTimeCoroutine);

        StartCoroutine(LifeTimeRoutine());
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, _speed));
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }
}
