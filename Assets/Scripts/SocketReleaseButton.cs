using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketReleaseButton : MonoBehaviour
{
    private XRSocketInteractor socket;
    public InputActionProperty AButton;
    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void Update()
    {
        if (AButton.reference.action.WasPressedThisFrame())
        {
            Debug.Log("A버튼");
            ReleaseObject();
        }
    }

    public void ReleaseObject()
    {
        Debug.Log("ReleasObject");
        if (socket.hasSelection) 
        {
            Debug.Log("ReleasObject 외부 if문 진입");

            var selectedInteractable = socket.firstInteractableSelected;
            Debug.Log($"셀렉트인터렉터블 : {selectedInteractable.transform.name}");
            if (selectedInteractable != null)
            {
                Debug.Log("ReleasObject 내부 if문 진입");
                socket.interactionManager.SelectExit(socket, selectedInteractable);
            }
        }
    }
}
