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
        if (socket.hasSelection) // 소켓에 물건이 있을 때만 실행
        {
            Debug.Log("ReleasObject 외부 if문 진입");

            var selectedInteractable = socket.firstInteractableSelected;

            if (selectedInteractable != null)
            {
                Debug.Log("ReleasObject 내부 if문 진입");
                // interactionManager를 통해 선택 해제
                socket.interactionManager.SelectExit(socket, selectedInteractable);
            }
        }
    }
}
