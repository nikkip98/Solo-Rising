using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{
    [SerializeField]
    private Button startHostButton;

    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private GameObject buttonsPanel;

    //[SerializeField]
    //private GameObject menuCamera;

    void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                OnSuccessConnection();
                print("Host Started");
            }
            else
            {
                print("Host NOT started");
            }
        });
        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                OnSuccessConnection();
                print("Server Started");
            }
            else
            {
                print("Server NOT started");
            }
        });
        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                OnSuccessConnection();
                print("Client Started");
            }
            else
            {
                print("Client NOT started");
            }
        });
    }

    private void OnSuccessConnection()
    {
        buttonsPanel.SetActive(false);
        //menuCamera.SetActive(false);
    }
}