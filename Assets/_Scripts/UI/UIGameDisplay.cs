using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentCurrency;

    private void Update()
    {
        currentCurrency.text = PlayerManager.instance.GetCurrency().ToString();
    }
}
