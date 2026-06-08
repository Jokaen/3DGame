using UnityEngine;
using System.Collections.Generic;

public class MagicSkills : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _playerCamera;

    [Header("Fireball")]
    [SerializeField] private float _fireballForce = 20f;
    [SerializeField] private float _fireballMaxDistance = 20f;

    [Header("Cube")]
    [SerializeField] private GameObject _cube;

    [SerializeField] private int _maxCubes = 3;

    private List<GameObject> _activeCube = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastFireball();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnBlock();
        }
    }

    private void CastFireball()
    {
        Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, _fireballMaxDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(_fireballMaxDistance);
        }

        _spawnPoint.LookAt(targetPoint);

        GameObject ball = Instantiate(_fireballPrefab, _spawnPoint.position, _spawnPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = _spawnPoint.forward * _fireballForce;
        }
        Destroy(ball, _fireballMaxDistance / _fireballForce);
    }

    private void SpawnBlock()
    {
        if (_activeCube.Count >= _maxCubes)
        {
            Destroy(_activeCube[0]);
            _activeCube.RemoveAt(0);
        }

        Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 spawnPos;

        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            spawnPos = hit.point + hit.normal * 0.5f;
        }
        else
        {
            spawnPos = ray.GetPoint(5f);
        }

        GameObject cube = Instantiate(_cube, spawnPos, Quaternion.identity);
        _activeCube.Add(cube);
    }
}