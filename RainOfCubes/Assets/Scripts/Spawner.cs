using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private ObjectPool<GameObject> _pool;
    [SerializeField] private ExtraPlatform _extraPlatform;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    private ColorChanger _colorChanger;
    private int _amountOfContact = 0;
    private float timeUntilDeletion = 0;

    private void Awake()
    {
        _colorChanger = gameObject.AddComponent<ColorChanger>();
        _pool = new ObjectPool<GameObject>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, 7f);
    }

    private void OnEnable()
    {
        _extraPlatform.WasContact += IncreaseNumberOfTouch;
    }

    private void OnDisable()
    {
        _extraPlatform.WasContact -= IncreaseNumberOfTouch;
    }

    public void StartTime(GameObject obj, ref int count)
    {
        float minTime = 2f;
        float maxTime = 5f;

        timeUntilDeletion = Random.Range(minTime, maxTime + 1);
        StartCoroutine(WaitForSeconds(timeUntilDeletion, obj));
        ResetCount(ref count);
    }

    private void ActionOnGet(GameObject obj)
    {
        int minCoordinate = -1;
        int maxCoordinate = 1;
        int coordinateY = 5;
        Vector3 randomSpawnPosition = new Vector3(Random.Range(minCoordinate, maxCoordinate), coordinateY, Random.Range(minCoordinate, maxCoordinate));
        obj.transform.position = randomSpawnPosition;
        obj.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_amountOfContact == 0)
        {
            _amountOfContact++;
            _colorChanger.ChangeColor(other.gameObject);
            StartTime(other.gameObject, ref _amountOfContact);
        }
    }

    private void IncreaseNumberOfTouch()
    {
        _amountOfContact++;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private IEnumerator WaitForSeconds(float seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        _pool.Release(gameObject);
    }

    private void ResetCount(ref int count)
    {
        count = 0;
    }
}
