using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    public float speed = .1f;

    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
        }

        if (IsClient && IsOwner)
        {
            UpdateClient();
        }

        UpdateServer();
    }

    private void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x+networkPositionDirection.Value.x,
            transform.position.y+networkPositionDirection.Value.y, 0.0f);
    }

    private void UpdateClient()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(xDirection, yDirection, 0.0f);

        UpdateClientPositionServerRpc(moveDirection * speed);
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(Vector3 newPosition)
    {
        networkPositionDirection.Value = newPosition;
    }
}
