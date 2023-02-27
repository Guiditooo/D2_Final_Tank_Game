using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedRotation = 100;
    [SerializeField] private float distanceFromObject = 1;
    [SerializeField] private float minZoom = 1;
    [SerializeField] private float maxZoom = 1;
    [SerializeField] private float sensitive = 1;


    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
        transform.LookAt(target.position);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speedRotation, Vector3.up) * offset;
            //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * speedRotation, Vector3.right) * offset;
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speedRotation, transform.up) * offset;
            offset = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * speedRotation, transform.right) * offset;
        }

        distanceFromObject += Input.GetAxis("Mouse ScrollWheel") * -sensitive;
        distanceFromObject = Mathf.Clamp(distanceFromObject, minZoom, maxZoom);

        transform.position = target.position + offset * distanceFromObject;

        transform.LookAt(target.position);

        //transform.eulerAngles = new Vector3(target.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

    }

}
