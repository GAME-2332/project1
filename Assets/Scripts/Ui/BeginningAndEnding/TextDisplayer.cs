using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BeginningAndEndingSceneUI
{

    public class TextDisplayer : MonoBehaviour
    {

        [SerializeField]
        SONarration _narrationList;

        [SerializeField]
        SceneReference NextScene;
        

        TMPro.TMP_Text _text;

        CanvasGroup _textCanvas;
        // Start is called before the first frame update
        int currentline = 0;
        void Start()
        {
            if(_narrationList == null)
            {
                _narrationList = Resources.Load<SONarration>("UI/BeginningAndEnding/IntroTextList");
            }
            if(_textCanvas == null)
            {
                _textCanvas = GetComponentInChildren<CanvasGroup>();
            }
            if(_text == null)
            {
                _text = GetComponentInChildren<TMPro.TMP_Text>();
            }
            PlayLine(_narrationList.lines[0]);
        }

        bool lineLock = false;
        void PlayLine(string s = "")
        {
            StartCoroutine("FadeIn");
            _text.text = _narrationList.lines[currentline];




        }

        bool isFinished = false;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(lineLock == false)//if the line has finished playing
                {
                    lineLock = true;
                    if(currentline < _narrationList.lines.Length)
                    {
                        currentline++;
                        if (currentline >= _narrationList.lines.Length - 1)
                        {
                            currentline = _narrationList.lines.Length - 1;
                            if(isFinished == false)
                            {
                                StartCoroutine("FadeIn");
                                _text.text = _narrationList.lines[currentline];
                                isFinished = true;
                            }
                            else
                            {
                                //LOAD NEXT SCENE HERE
                                GameManager.instance.saveState.LoadScene(NextScene.ScenePath);
                                _text.text = "";
                            }
                           
                        }
                        else
                        {
                            PlayLine();
                        }

                       
                    }
                   
                }
            }
        }

        float delta = 0.1f;
        IEnumerator FadeIn()
        {
            lineLock = true;
            _textCanvas.alpha = 0;
           
            while (_textCanvas.alpha < 1)
            {
                _textCanvas.alpha += delta;
                yield return new WaitForSeconds(0.1f);
            }
            lineLock = false;
            yield return new WaitForEndOfFrame();
        }

        IEnumerator FadeOut()
        {
            lineLock = true;
            _textCanvas.alpha = 1;
            while (_textCanvas.alpha >0)
            {
                _textCanvas.alpha -= delta;
                yield return new WaitForSeconds(0.05f);
            }
            lineLock = false;
            Debug.Log("fading out" + currentline);
            if (currentline < _narrationList.lines.Length)
            {
                _textCanvas.alpha = 0;
                _text.text = _narrationList.lines[currentline];
                yield return new WaitForSeconds(0.2f);
                
                StartCoroutine("FadeIn");
            }
            else
            {

            }
            
            yield return new WaitForEndOfFrame();
        }
    }

}