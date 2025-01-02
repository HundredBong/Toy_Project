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
            Debug.Log("A��ư");
            ReleaseObject();
        }
    }

    public void ReleaseObject()
    {
        Debug.Log("ReleasObject");
        if (socket.hasSelection) // ���Ͽ� ������ ���� ���� ����
        {
            Debug.Log("ReleasObject �ܺ� if�� ����");

            var selectedInteractable = socket.firstInteractableSelected;

            if (selectedInteractable != null)
            {
                Debug.Log("ReleasObject ���� if�� ����");
                // interactionManager�� ���� ���� ����
                socket.interactionManager.SelectExit(socket, selectedInteractable);
            }
        }
    }
}
