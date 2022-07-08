using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Tanks
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        public static MainMenu instance; // �令 Public
        private GameObject m_ui;
        private TMP_InputField m_accountInput; // �s�W ��J�J
        private Button m_loginButton; // �s�W �n�J���s
        private Button m_joinGameButton;
        void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
            m_ui = transform.FindAnyChild<Transform>("UI").gameObject;
            m_accountInput = transform.FindAnyChild<TMP_InputField>("AccountInput"); // �����J�J����
            m_loginButton = transform.FindAnyChild<Button>("LoginButton"); // ����n�J���s����
            m_joinGameButton = transform.FindAnyChild<Button>("JoinGameButton");
            ResetUI(); // ��X UI ��l��
        }

        private void ResetUI() // ���m UI
        {
            m_ui.SetActive(true);
            m_accountInput.gameObject.SetActive(true);
            m_loginButton.gameObject.SetActive(true);
            m_joinGameButton.gameObject.SetActive(false);
            m_accountInput.interactable = true;
            m_loginButton.interactable = true;
            m_joinGameButton.interactable = true;
        }

    
        public override void OnConnectedToMaster() // �B�z�s�u�� UI �ܤ�
        {
            m_accountInput.gameObject.SetActive(false);
            m_loginButton.gameObject.SetActive(false);
            m_joinGameButton.gameObject.SetActive(true);
        }

        public override void OnEnable()
        {
            // Always call the base to add callbacks
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
            m_ui.SetActive(!PhotonNetwork.InRoom);
        }

        public void Login() // �B�z �n�J���A���y�{
        {
            if (string.IsNullOrEmpty(m_accountInput.text))
            {
                Debug.Log("Please input your account!!");
                return;
            }
            m_accountInput.interactable = false;
            m_loginButton.interactable = false;
            if (!GameManager.instance.ConnectToServer(m_accountInput.text))
            {
                Debug.Log("Connect to PUN Failed!!");
            }
        }
       
    }
}