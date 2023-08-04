using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Aimer : MonoBehaviour
    {

        [SerializeField] private float aimTime = 1f; //en segundos que quiero que tarde
        [SerializeField] private float aimSpeed = 1f; //en segundos que quiero que tarde

        public static System.Action<Quaternion> OnAim;

        private Coroutine moveCoroutine = null;

        private void Update()
        {
            if (!PauseSystem.Paused)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                        if (moveCoroutine != null)
                        {
                            StopCoroutine(moveCoroutine);
                        }
                        moveCoroutine = StartCoroutine(MoveCannon(targetPosition));
                    }
                }
            }
        }

        private IEnumerator MoveCannon(Vector3 target)
        {
            Vector3 direction = target - transform.position;
            Quaternion actualRotation = transform.rotation;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);

            float time = 0;

            while (time < aimTime)
            {
                time += Time.deltaTime * aimSpeed;
                transform.rotation = Quaternion.Lerp(actualRotation, targetRotation, time);

                yield return null;
            }
            //Debug.LogWarning("VOY A CREAR UNA BALA");
            OnAim?.Invoke(targetRotation);
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
}