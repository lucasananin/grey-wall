using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    // this script pushes all rigidbodies that the character touches
    [SerializeField] float _pushPower = 2.0f;

    private void Start()
    {
        _pushPower = 2f;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rb = hit.collider.attachedRigidbody;

        // no rigidbody
        if (_rb == null || _rb.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 _pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        _rb.velocity = _pushDir * _pushPower;
    }
}
