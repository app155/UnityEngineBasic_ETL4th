using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    StartLoading,
    OnLoading,
    EndLoading,
}

[Serializable]
public class Sample : MonoBehaviour
{
    public int num { get; set; }
    private int _num;

    public States state { get; set; }
    private States _state;

    public struct Data
    {
        public string key;
        public string value;
    }

    public Data data;
}
