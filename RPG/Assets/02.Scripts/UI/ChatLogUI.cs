using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

namespace RPG.UI
{
    public class ChatLogUI : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _logs;
        private StringBuilder _stringBuilder;

        public override void Init()
        {
            base.Init();

            _stringBuilder = new StringBuilder(1000);
            gameObject.SetActive(true);
            StartCoroutine(C_Init());
        }

        private IEnumerator C_Init()
        {
            yield return new WaitUntil(() => ChatManager.instance != null);
            ChatManager.instance.onChatLogChanged += Refresh;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (ChatManager.instance != null)
                ChatManager.instance.onChatLogChanged -= Refresh;
        }

        private void Refresh(IEnumerable<string> logs)
        {
            _stringBuilder.Clear();

            foreach (var log in logs)
            {
                _stringBuilder.AppendLine(log);
            }

            _logs.text = _stringBuilder.ToString();
        }
    }
}
