using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuUI_Components {



    public class NoDataAnimator : MonoBehaviour
    {
        [SerializeField]
        Animator _animator;
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.playbackTime = 0;
            _animator.StopPlayback();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.Play(0);
            }
        }
    }



}
