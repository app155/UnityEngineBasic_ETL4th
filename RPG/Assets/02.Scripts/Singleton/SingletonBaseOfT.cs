using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPG.Singleton
{
    public class SingletonBase<T>
        where T : SingletonBase<T>
    {
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    // Reflection -> ��Ÿ�� �� ��Ÿ������ ���� ... �ڵ��� ������ �˻�
                    // Reflection�� �̿��� ������ ������ �޾ƿͼ� ȣ��.
                    //Type type = typeof(T);
                    //ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
                    //_instance = (T)constructorInfo.Invoke(null);

                    // �ν��Ͻ� ������ �ʿ��� ��ɵ��� �����ϴ� Activator
                    _instance = Activator.CreateInstance<T>();
                    _instance.Init();
                }

                return _instance;
            }
        }

        private static T _instance;

        protected virtual void Init()
        {

        }
    }
}