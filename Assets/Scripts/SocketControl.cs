using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketControl : MonoBehaviour
{
    private XRSocketInteractor socket;
    private InteractionLayerMask defaultLayer; 
    public InteractionLayerMask ungrabableLayer; 

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        defaultLayer = socket.interactionLayers;
        socket.selectEntered.AddListener(OnSelectEntered);
        socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
        socket.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        //if (args.interactableObject is XRGrabInteractable grabInteractable)
        //{
        //    grabInteractable.interactionLayers = ungrabableLayer;
        //    Debug.Log(grabInteractable.interactionLayers);
        //    Debug.Log(grabInteractable.name);
        //}
        args.interactableObject.transform.GetComponent<XRGrabInteractable>().interactionLayers = ungrabableLayer;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject is XRGrabInteractable grabInteractable)
        {
            grabInteractable.interactionLayers = defaultLayer; 
        }
    }
}

