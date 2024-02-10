using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 20;
    bool rotate = false;
 
    void FixedUpdate()
    {
        if (rotate == false)
            return;

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
 
    private void OnMouseDrag()
    {
        rotate = true;
        Debug.Log("startrotate");

    }

    private void OnMouseUp()
    {
        rotate = false;
    }

}