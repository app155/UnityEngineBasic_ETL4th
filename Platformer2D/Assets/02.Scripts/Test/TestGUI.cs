using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = Platformer.Controllers.CharacterController;

namespace Platformer.Test
{
    public class TestGUI : MonoBehaviour
    {
#if UNITY_EDITOR // if 전처리기문 : if 내용이 참일 경우 해당 내용 컴파일. 아니면 안함
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

