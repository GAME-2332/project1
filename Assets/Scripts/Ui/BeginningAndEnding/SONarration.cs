using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeginningAndEndingSceneUI
{

    [CreateAssetMenu(fileName = "SOItemInfo", menuName = "Narration/New Narration List")]
    public class SONarration : ScriptableObject
    {
        [SerializeField]
        public string[] lines;



    }
}
