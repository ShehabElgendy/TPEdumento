using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool interact;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private Interactor interactor;

        private void Awake()
        {
            interactor = GetComponent<Interactor>();
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private void Start()
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (Cursor.lockState == CursorLockMode.None && cursorLocked)
            {
                MoveInput(Vector2.zero);
                return;
            }

            MoveInput(ctx.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext ctx)
        {
            if (cursorInputForLook)
            {
                if (Cursor.lockState == CursorLockMode.None && cursorLocked)
                {
                    LookInput(Vector2.zero);
                    return;
                }

                LookInput(ctx.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (Cursor.lockState == CursorLockMode.None && cursorLocked)
            {
                JumpInput(ctx.action.WasPressedThisFrame());
                return;
            }

            JumpInput(ctx.action.WasPressedThisFrame());
        }

        public void OnSprint(InputAction.CallbackContext ctx)
        {
            if (Cursor.lockState == CursorLockMode.None && cursorLocked)
            {
                SprintInput(ctx.action.WasPressedThisFrame());
                return;
            }

            SprintInput(ctx.action.WasPressedThisFrame());
        }

        //public void OnInteractPrimary(InputValue value)
        //{
        //	if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(0)) return;
        //	SetCursorState(cursorLocked);
        //}

        //public void OnInteractSecondary(InputValue value)
        //{
        //	if (EventSystem.current && EventSystem.current.IsPointerOverGameObject(1)) return;
        //	SetCursorState(cursorLocked);
        //}

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (Cursor.lockState == CursorLockMode.None && cursorLocked)
            {
                InteractInput(ctx.action.WasPressedThisFrame());
                return;
            }

            if (ctx.action.WasPressedThisFrame())
            {
                interactor.Interact();
            }

            InteractInput(ctx.action.WasPressedThisFrame());
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }


        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}