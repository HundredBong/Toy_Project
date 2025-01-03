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
        if (socket.hasSelection) 
        {
            Debug.Log("ReleasObject �ܺ� if�� ����");

            var selectedInteractable = socket.firstInteractableSelected;
            Debug.Log($"����Ʈ���ͷ��ͺ� : {selectedInteractable.transform.name}");
            if (selectedInteractable != null)
            {
                Debug.Log("ReleasObject ���� if�� ����");
                socket.interactionManager.SelectExit(socket, selectedInteractable);
            }
        }
    }
}
