using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour, IDamagable
{
    [SerializeField] private Transform _startTransformOfBullet = null;
    [SerializeField] private float _movingSpeed = 1f;

    [SerializeField] private SimpleTouchController _touchController = null;

    private int _lifes = 3;
    private Transform _camTransform;

    public int Lifes { get => _lifes; private set => _lifes = value; }

    private void Start()
    {
        _camTransform = Camera.main.transform;

        _lifes = UIGameManager.Instance.LifeImages.Length;
    }

    private void FixedUpdate()
    {
        Moving();

        if (Input.GetKeyDown("space"))
            Attack();
    }

    private void Moving()
    {
        if (!GameManager.IsPause)
        {
#if UNITY_EDITOR
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
            float horizontal = _touchController.GetTouchPosition.x;
            float vertical = _touchController.GetTouchPosition.y;
#endif

            if ((transform.position.x > LevelController.Instance.LevelBoundsHorizontal[0] || horizontal > 0) &&
                (transform.position.x < LevelController.Instance.LevelBoundsHorizontal[1] || horizontal < 0))
            {
                transform.Translate(new Vector3(horizontal * _movingSpeed, 0, 0));
            }

            if ((transform.position.z > LevelController.Instance.LevelBoundsVertical[0] || vertical > 0) &&
                (transform.position.z < LevelController.Instance.LevelBoundsVertical[1] || vertical < 0))
            {
                transform.Translate(new Vector3(0, 0, vertical * _movingSpeed));
            }
        }
    }

    public void Attack()
    {
        GameObject newBullet = PoolController.Instance.Bullets.GetObject();
        newBullet.transform.position = _startTransformOfBullet.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>();

            Hit(asteroid.Damage);
            asteroid.Hit(999);
        }
    }

    public void Hit(int damage)
    {
        Lifes -= damage;
        UIGameManager.OnUpdateUI();
        if (Lifes <= 0)
        {
            Lifes = 0;
            gameObject.SetActive(false);
            GameManager.OnLose();
        }
    }
}
