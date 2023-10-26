using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Datum
{
    [CreateAssetMenu(fileName = "new SkillCastSetting", menuName = "Platformer/ScriptableObjects/SkillCastSetting")]
    public class SkillCastSetting : ScriptableObject
    {
        public int targetMax;
        public LayerMask targetMask;
        public float damageGain;
        public Vector2 castCenter;
        public Vector2 castSize;
        public float castDistance;
    }
}

