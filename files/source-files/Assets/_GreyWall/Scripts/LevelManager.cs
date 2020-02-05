using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    // Componentes;
    [SerializeField]
    private GameObject[] _templates = null;
    [SerializeField]
    private GameObject[] _spawnPoints = null;
    [SerializeField]
    private NavMeshSurface _surface = null;

    // Variaveis;
    private int _randomTemplate;

    private void Awake()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _randomTemplate = Random.Range(0, _templates.Length);
            var _template = Instantiate(_templates[_randomTemplate], _spawnPoints[i].transform.position, transform.rotation);
            _template.transform.parent = gameObject.transform;
        }

        _surface.BuildNavMesh();
    }
}
