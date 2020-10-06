using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
    // Valores;
    [SerializeField] bool _canBePressed = false;

    private void Start()
    {
        BoxCollider _bC = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider _bT = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        _bT.size = new Vector3(2, 1, 2);
        _bT.isTrigger = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canBePressed == true)
        {
            Debug.Log("Interagiu!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Dentro de alcance!");
            _canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Fora de alcance!");
            _canBePressed = false;
        }
    }
}
