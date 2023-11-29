using RPG.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        public Dictionary<Type, IUI> uis = new Dictionary<Type, IUI>();
        public LinkedList<IUI> showns = new LinkedList<IUI>();

        public T Get<T>()
            where T : IUI
        {
            if (uis.TryGetValue(typeof(T), out IUI ui))
            {
                return (T)ui;
            }

            else
            {
                throw new Exception($"[UIManager] - Get<T>");
            }
        }

        public void Register(IUI ui)
        {
            Type type = ui.GetType();

            if (uis.TryAdd(type, ui) == false)
            {
                throw new Exception($"[UIManager] - Register");
            }

            Debug.Log($"[UIManager] registered {type}");
        }

        public void Push(IUI ui)
        {
            if (showns.Count > 0 && showns.Last.Value == ui)
                return;

            int sortingOrder = 0;

            if (showns.Last?.Value != null)
            {
                sortingOrder = showns.Last.Value.sortingOrder + 1;
                showns.Last.Value.inputActionEnabled = false;
            }

            ui.sortingOrder = sortingOrder;
            ui.inputActionEnabled = true;
            showns.Remove(ui);
            showns.AddLast(ui);

            if (showns.Count == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void Pop(IUI ui)
        {
            // 마지막거면, 앞의 ui의 InputAction 활성화
            if (showns.Count > 1 && showns.Last.Value == ui)
            {
                showns.Last.Previous.Value.inputActionEnabled = true;
            }

            showns.Remove(ui);

            if (showns.Count == 0)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void HideLast()
        {
            if (showns.Count <= 0)
                return;

            showns.Last.Value.Hide();
        }
    }
}