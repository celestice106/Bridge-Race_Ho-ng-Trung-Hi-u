using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float cameraSpeed = 5f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position - offset, cameraSpeed * Time.deltaTime);
    }
}
