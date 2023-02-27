using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aimer : MonoBehaviour
{
    [SerializeField] private float turretSpeed = 1.0f;
    [SerializeField] private Transform target = null;

    public Action OnTargetReach;

    private IEnumerator MoveCannonCoroutine = null;

    private void Awake()
    {

    }
    private void OnDestroy()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartAiming(target.position);


            //Vector3 targetDir = target.position - transform.position;

           
            //transform.rotation = Quaternion.LookRotation(targetDir.normalized);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += transform.forward;
        }

    }

    private void StartAiming(Vector3 targetPos)
    {
        if (MoveCannonCoroutine == null)
        {
            MoveCannonCoroutine = MoveTheCannon(targetPos);
            StartCoroutine(MoveCannonCoroutine);
        }
    }

    IEnumerator MoveTheCannon(Vector3 targetPos)
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.y = 0f;
        while (!LookingTheTarget(targetPos))
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(transform.forward), targetRotation, turretSpeed * Time.deltaTime);
            yield return null;
        }

        //OnTargetReach();

        Debug.Log("SHOOT!");

    }

    bool LookingTheTarget(Vector3 targetPos)
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.y = 0f;
        bool aux = targetDir.normalized == transform.forward;
        Debug.Log(targetDir.normalized + " == " + transform.forward + " : " + aux);
        return aux;
    }

}
