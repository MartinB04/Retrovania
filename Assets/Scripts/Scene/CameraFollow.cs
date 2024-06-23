using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow sharedInstance;
    [SerializeField] GameObject follow;
    [SerializeField] Vector2 minCamPos, maxCamPos;
   

    [SerializeField] float smoothTime;

    [SerializeField] Vector2 velocity;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(
            Mathf.Clamp(follow.transform.position.x, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(follow.transform.position.y, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x,
            follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y,
            follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);

    }
}
