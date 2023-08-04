using UnityEngine;
using System;

namespace GT
{

    public class InputManager : MonoBehaviour
    {

        public static Action<MovementDirection> OnMovementPress;
        public static Action OnPausePress;
        public static Action<Vector3> OnClicPress;

        private void Update()
        {
            if (!PauseSystem.Paused)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) OnMovementPress?.Invoke(MovementDirection.Backward);

                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) OnMovementPress?.Invoke(MovementDirection.Forward);

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) OnMovementPress?.Invoke(MovementDirection.Left);

                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) OnMovementPress?.Invoke(MovementDirection.Right);

                if (Input.GetMouseButtonDown(0)) OnClicPress?.Invoke(Input.mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                OnPausePress?.Invoke();
            }


        }
    }

}