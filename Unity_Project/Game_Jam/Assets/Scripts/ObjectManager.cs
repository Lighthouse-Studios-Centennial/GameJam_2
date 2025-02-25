using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        playerState = PlayerState.PLAY;
        currentObjectScale = currentGameObject.transform.localScale;
        currentObjectRotation = currentGameObject.transform.localRotation.eulerAngles;
        sliderControls.SetActive(false);
        playPauseButton.gameObject.SetActive(false);
        playPauseButton.image.sprite = pauseButton;
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
        playPauseButton.gameObject.SetActive(true);
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

    public enum PlayerState
    {
        PLAY,
        PAUSE
    }

}
