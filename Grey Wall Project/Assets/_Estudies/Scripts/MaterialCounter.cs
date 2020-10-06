using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCounter : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject _obj = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(_obj, transform.position, Quaternion.identity);
        }

        print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    }
}
