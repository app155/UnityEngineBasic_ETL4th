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
                    // Reflection -> 런타임 중 메타데이터 접근 ... 코드의 정보를 검색
                    // Reflection을 이용해 생성자 정보를 받아와서 호출.
                    //Type type = typeof(T);
                    //ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
                    //_instance = (T)constructorInfo.Invoke(null);

                    // 인스턴스 생성에 필요한 기능들을 제공하는 Activator
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