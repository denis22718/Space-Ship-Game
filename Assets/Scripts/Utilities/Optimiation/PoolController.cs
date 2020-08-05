using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoSingleton<PoolController>
{
    [Header("Game objects:"), Space(10)]
    public PoolGameObjects Bullets;
    public PoolGameObjects Asteroids;

    [Header("Particles:"), Space(10)]
    public PoolParticleObjects particlesAsteroidExplosion;

    public override void Init()
    {
        base.Init();

        Bullets.AddObjectsToPool();
        Asteroids.AddObjectsToPool();

        particlesAsteroidExplosion.AddObjectsToPool();
    }

    [System.Serializable]
    public class PoolGameObjects
    {
        public GameObject prefab;
        public Transform parent;
        private List<GameObject> _listOfObjects = new List<GameObject>();
        public int amountCreateObjects = 1;

        /// <summary>
        /// Filling pool
        /// </summary>
        public void AddObjectsToPool()
        {
            if (amountCreateObjects <= 0)
                Debug.LogError("ERROR amountCreateObjects == 0 in PoolController. Name: " + prefab.name, Instance.gameObject);

            for (int i = 0; i < amountCreateObjects; i++)
            {
                GameObject newObj = Instantiate(prefab);
                newObj.SetActive(false);
                newObj.transform.SetParent(parent);
                _listOfObjects.Add(newObj);
            }
        }

        /// <summary>
        /// Get one object from pool
        /// </summary>
        /// <returns></returns>
        public GameObject GetObject()
        {
            for (int i = 0; i < _listOfObjects.Count; i++)
            {
                if (!_listOfObjects[i].activeInHierarchy)
                {
                    _listOfObjects[i].SetActive(true);
                    return _listOfObjects[i];
                }
            }

            AddObjectsToPool();
            return GetObject();
        }

        /// <summary>
        /// Add to pool one new object
        /// </summary>
        private GameObject GetNewObject()
        {
            GameObject newObj = Instantiate(prefab);
            newObj.transform.SetParent(parent);
            _listOfObjects.Add(newObj);

            return newObj;
        }
    }

    [System.Serializable]
    public class PoolParticleObjects
    {
        public ParticleSystem prefab;
        public Transform parent;
        private List<ParticleSystem> _listOfObjects = new List<ParticleSystem>();
        public int amountCreateObjects = 1;

        public void AddObjectsToPool()
        {
            for (int i = 0; i < amountCreateObjects; i++)
            {
                ParticleSystem newObj = Instantiate(prefab);
                newObj.gameObject.SetActive(false);
                newObj.transform.SetParent(parent);
                _listOfObjects.Add(newObj);
            }
        }

        public ParticleSystem PlayParticles(Vector3 position)
        {
            for (int i = 0; i < _listOfObjects.Count; i++)
            {
                if (!_listOfObjects[i].gameObject.activeInHierarchy)
                {
                    _listOfObjects[i].transform.position = position;
                    _listOfObjects[i].gameObject.SetActive(true);
                    return _listOfObjects[i];
                }
            }

            AddObjectsToPool();
            return PlayParticles(position);
        }
    }
}
