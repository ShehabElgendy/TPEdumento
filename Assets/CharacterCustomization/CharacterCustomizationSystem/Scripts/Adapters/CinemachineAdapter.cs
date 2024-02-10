/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;
using System;
using System.Reflection;

namespace ComfortGames.CharacterCustomization
{
    public class CinemachineAdapter : MonoBehaviour
    {
        [Tooltip("Name of the object that has the cinemachineStateDrivenCamera component")]
        public string cinemachineStateDrivenCameraName = "Climbing Camera State Driven";
        [Tooltip("Name of the object that you want CinemachineStateDrivenCamera to Follow and LookAt")]
        public string cameraTargetName = "Camera Track Pos";

        public bool DoesAssemblyExists(string assemblyName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith(assemblyName))
                    return true;
            }
            return false;
        }

        public void SetupCinemachine(GameObject characterObject)
        {
            //Using reflection to see if Cinemachine is in our project
            string assemblyName = "Cinemachine";
            string scriptName = "CinemachineStateDrivenCamera";
            Assembly assembly = null;
            if (DoesAssemblyExists(assemblyName))
                assembly = Assembly.Load(assemblyName);

            if (assembly == null)
                return;

            Type cinemachineStateDrivenCameraType = assembly.GetType(string.Format("{0}.{1}", assemblyName, scriptName));
            if (cinemachineStateDrivenCameraType == null)
                return;

            GameObject cinemachineStateDrivenCameraObject = GameObject.Find(cinemachineStateDrivenCameraName);
            if (cinemachineStateDrivenCameraObject == null)
                return;

            var cinemachineStateDrivenCamera = cinemachineStateDrivenCameraObject.GetComponent(cinemachineStateDrivenCameraType);
            if (cinemachineStateDrivenCamera == null)
                return;

            PropertyInfo followPropertyInfo = cinemachineStateDrivenCameraType.GetProperty("Follow");
            PropertyInfo lookAtPropertyInfo = cinemachineStateDrivenCameraType.GetProperty("LookAt");
            FieldInfo animatedTargetPropertyInfo = cinemachineStateDrivenCameraType.GetField("m_AnimatedTarget");

            GameObject cameraTarget = GameObject.Find(cameraTargetName);
            if (cameraTarget == null)
                return;

            followPropertyInfo?.SetValue(cinemachineStateDrivenCamera, cameraTarget.transform, null);
            lookAtPropertyInfo?.SetValue(cinemachineStateDrivenCamera, cameraTarget.transform, null);

            if (characterObject == null)
                return;

            Animator animator = characterObject.GetComponent<Animator>();
            animatedTargetPropertyInfo?.SetValue(cinemachineStateDrivenCamera, animator);
        }
    }
}
