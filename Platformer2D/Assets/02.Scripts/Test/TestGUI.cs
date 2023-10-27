using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = Platformer.Controllers.CharacterController;

namespace Platformer.Test
{
    public class TestGUI : MonoBehaviour
    {
#if UNITY_EDITOR // if ��ó���⹮ : if ������ ���� ��� �ش� ���� ������. �ƴϸ� ����
        [SerializeField] private CharacterController _controller;

        private void Awake()
        {
            GameObject.Find("Player")?.TryGetComponent(out _controller);
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(10.0f, 10.0f, 200.0f, 140.0f), "Test");

            if (GUI.Button(new Rect(20.0f, 40.0f, 180.0f, 80.0f), "Hurt"))
            {
                _controller?.DepleteHp(this, 10.0f);
            }
        }
#endif
    }
}

