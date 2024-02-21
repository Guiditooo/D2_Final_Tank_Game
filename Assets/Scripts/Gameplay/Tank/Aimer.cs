using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class Aimer : MonoBehaviour
    {

        [SerializeField] private float aimTime = 1f; //en segundos que quiero que tarde
        [SerializeField] private float aimSpeed = 1f; //en segundos que quiero que tarde
        [SerializeField] private LayerMask layerRaycast; 

        public static System.Action<Quaternion> OnAim;

        private Coroutine moveCoroutine = null;
        private bool alreadyRunning = false;
        private AudioSource audioSource = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!PauseSystem.Paused)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerRaycast))
                    {
                        Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                        if (!alreadyRunning)
                        {
                            moveCoroutine = StartCoroutine(MoveCannon(targetPosition));
                        }
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
            alreadyRunning = true;

            audioSource.Play();

            while (time < aimTime)
            {
                time += Time.deltaTime * aimSpeed;
                transform.rotation = Quaternion.Lerp(actualRotation, targetRotation, time);

                yield return null;
            }
            alreadyRunning = false;
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