using SickscoreGames.HUDNavigationSystem;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUps : MonoBehaviour
{
    private Transform pickupText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!HUDNavigationSystem.Instance.isEnabled)
                return;

            HUDNavigationElement element = FindObjectOfType<HUDNavigationElement>();
            if (element != null)
            {
                // show pickup text
                if (element.Indicator != null)
                {
                    pickupText = element.Indicator.GetCustomTransform("pickupText");
                    if (pickupText != null)
                        pickupText.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!HUDNavigationSystem.Instance.isEnabled)
                return;

            HUDNavigationElement element = FindObjectOfType<HUDNavigationElement>();
            if (element != null)
            {
                // hide pickup text
                if (element.Indicator != null)
                {
                    pickupText = element.Indicator.GetCustomTransform("pickupText");
                    if (pickupText != null)
                        pickupText.gameObject.SetActive(false);
                }
            }
        }
    }
}
