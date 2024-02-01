using UnityEngine;

public class Wizard : InteractOnTrigger
{
    public void WizardInteraction()
    {
        if (isInteracted)
        {
            GetComponentInParent<Animator>().enabled = true;
        }
        else if (!isInteracted)
        {
            GetComponentInParent<Animator>().enabled = false;
        }

        Debug.Log("Wizard");
    }
}
