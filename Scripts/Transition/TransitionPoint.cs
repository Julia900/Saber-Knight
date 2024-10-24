using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType { SameScene, DifferentScene}     
    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;

    private bool canTransit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTransit)
        {
            SceneController.Instance.TransitionToDestination(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.CompareTag("Player"))
            canTransit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canTransit = false;
    }
}
