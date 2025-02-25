using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

using Vuforia;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<DayNightObjectHandler> dayNightObjectHandlers = new List<DayNightObjectHandler>();

    [SerializeField] private float _speed = 10;
    [SerializeField] private GameObject currentGameObject;
    [SerializeField] private Vector3 currentObjectScale;
    [SerializeField] private Vector3 currentObjectRotation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isInteractable;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Light directionalLight;
    [SerializeField] private GameObject Butterflies;
    [SerializeField] private GameObject Fireflies;

    [Header("UI Files")]
    [SerializeField] private Sprite playButton;
    [SerializeField] private Sprite pauseButton;
    [SerializeField] private Button playPauseButton;

    [SerializeField] private GameObject sliderControls;
    [SerializeField] private Slider zoomSlider;
    [SerializeField] private Slider rotationSlider;

    [SerializeField] private GameObject gamificationObject;
    [SerializeField] private AudioSource baobabRattleSource;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private UnityEngine.UI.Image headerPanel;
    [SerializeField] private Color rightColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private string rightAnswer;
    [SerializeField] private AudioClip rightAnswerClip;
    [SerializeField] private AudioClip wrongAnswerClip;
    [SerializeField] private AudioSource guessAudioSource;
    [SerializeField] private ParticleSystem firework;
    [SerializeField] private ParticleSystem confetti;

    private void Awake()
    {
        // Initialize Vuforia manually
        VuforiaApplication.Instance.Initialize();
        playerState = PlayerState.PLAY;
        currentObjectScale = currentGameObject.transform.localScale;
        currentObjectRotation = currentGameObject.transform.localRotation.eulerAngles;
        sliderControls.SetActive(false);
        playPauseButton.gameObject.SetActive(false);
        playPauseButton.image.sprite = pauseButton;

        inputField.onEndEdit.AddListener(delegate
        {
            CheckAnswer(inputField);
        });
    }

    public void OnObjectTracked()
    {
        var dayNightObject = dayNightObjectHandlers[Random.Range(0, dayNightObjectHandlers.Count)];
        directionalLight.color = dayNightObject.lightColor;

        if (audioSource != null)
        {
            audioSource.clip = dayNightObject.cycleSound;
            audioSource.Play();
        }
        SwitchEffectsDayNightHandlers(dayNightObject);
        sliderControls.SetActive(false);
        playPauseButton.gameObject.SetActive(true);
        playPauseButton.image.sprite = pauseButton;
    }

    private void SwitchEffectsDayNightHandlers(DayNightObjectHandler selectedDayNightObjectHandler)
    {
        foreach (var handler in dayNightObjectHandlers)
        {
            switch (handler.objectToEnable)
            {
                case "Fireflies":
                    Fireflies.SetActive(handler == selectedDayNightObjectHandler);
                    break;

                case "Butterflies":
                    Butterflies.SetActive(handler == selectedDayNightObjectHandler);
                    break;
            }
        }
    }

    public void OnObjectLost()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        sliderControls.SetActive(false);
        playPauseButton.gameObject.SetActive(false);
        playPauseButton.image.sprite = pauseButton;
        gamificationObject.SetActive(false);
    }

    public void Update()
    {
        UpdateCurrentObject();
        //HandleInteractable();
    }

    private void UpdateCurrentObject()
    {
        if (currentGameObject == null || isInteractable)
        {
            return;
        }

        currentGameObject.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    }

    private void HandleInteractable()
    {
        if (!isInteractable)
        {
            return;
        }
    }

    private void RotateObject()
    {
        if (!isInteractable)
        {
            return;
        }

        var rot = currentGameObject.transform.localRotation.eulerAngles; //get the angles
        rot.Set(currentObjectRotation.x, currentObjectRotation.y + rotationSlider.value, currentObjectRotation.z); //set the angles
        currentGameObject.transform.localRotation = Quaternion.Euler(rot); //update the transform
    }

    private void ScaleObject()
    {
        if (!isInteractable)
        {
            return;
        }

        currentGameObject.transform.localScale = currentObjectScale * zoomSlider.value;
    }

    public void PressPlayPause()
    {
        switch (playerState)
        {
            case PlayerState.PLAY:
                playPauseButton.image.sprite = playButton;
                isInteractable = true;
                sliderControls.SetActive(true);
                audioSource.Pause();

                zoomSlider.onValueChanged.AddListener(delegate
                {
                    ScaleObject();
                });

                rotationSlider.onValueChanged.AddListener(delegate
                {
                    RotateObject();
                });

                playerState = PlayerState.PAUSE;
                break;

            case PlayerState.PAUSE:
                playPauseButton.image.sprite = pauseButton;
                isInteractable = false;
                sliderControls.SetActive(false);
                audioSource.Play();

                zoomSlider.onValueChanged.RemoveListener(delegate
                {
                    ScaleObject();
                });

                rotationSlider.onValueChanged.RemoveListener(delegate
                {
                    RotateObject();
                });

                //zoomSlider.value = 1f;
                rotationSlider.value = rotationSlider.minValue;
                //currentGameObject.transform.localScale = currentObjectScale;
                playerState = PlayerState.PLAY;
                break;

            default:
                break;
        }
    }

    public void OnMorePressed()
    {
        gamificationObject.SetActive(!gamificationObject.activeInHierarchy);

        if (gameObject.activeInHierarchy)
        {
            inputField.onEndEdit.AddListener(delegate
            {
                CheckAnswer(inputField);
            });
        }
        else
        {
            inputField.onEndEdit.RemoveListener(delegate
            {
                CheckAnswer(inputField);
            });
        }
    }

    public void PlayBaobab()
    {
        if (gamificationObject.activeInHierarchy)
        {
            if (baobabRattleSource.isPlaying)
            {
                baobabRattleSource.Stop();
                return;
            }

            baobabRattleSource.Play();
        }
    }

    private void CheckAnswer(TMP_InputField inputField)
    {
        if (inputField.text.Length == 0)
        {
            return;
        }

        if (inputField.text.Trim().ToLower().Equals(rightAnswer.Trim().ToLower()))
        {
            headerPanel.color = rightColor;
            guessAudioSource.PlayOneShot(rightAnswerClip, 1.1f);
            firework.Play();
            confetti.Play();
            return;
        }

        headerPanel.color = wrongColor;
        guessAudioSource.PlayOneShot(wrongAnswerClip, 1.1f);
    }

    public enum PlayerState
    {
        PLAY,
        PAUSE
    }

}
