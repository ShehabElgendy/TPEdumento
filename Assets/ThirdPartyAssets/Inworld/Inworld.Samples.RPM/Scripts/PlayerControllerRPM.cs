/*************************************************************************************************
 * Copyright 2022 Theai, Inc. (DBA Inworld)
 *
 * Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
 * that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
 *************************************************************************************************/
using UnityEngine;


namespace Inworld.Sample.RPM
{
    public class PlayerControllerRPM : PlayerController3D
    {
        InworldCameraController m_CameraController;
        InworldCharacter inworldCharacter;

        protected override void Awake()
        {
            inworldCharacter = FindObjectOfType<InworldCharacter>();

            if (inworldCharacter != null)
            {
                base.Awake();
                m_CameraController = GetComponent<InworldCameraController>();
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        protected override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
               //if (m_CameraController.enabled = !m_ChatCanvas.activeSelf)
               // {
               //     Cursor.visible = false;
               //     Cursor.lockState = CursorLockMode.Locked;
               // }
               // else if (m_CameraController.enabled = m_ChatCanvas.activeSelf)
               // {
               //     Cursor.visible = true;
               //     Cursor.lockState = CursorLockMode.None;
               // }

            }

        }
    }
}