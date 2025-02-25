using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Vuforia;


public class Permission : MonoBehaviour
{
    [SerializeField] private bool _isCamAvailable;
    private WebCamTexture _backCameraTexture;
    private Texture _defaultBackground;

    [SerializeField] private RawImage _background;
    [SerializeField] private AspectRatioFitter _fit;

    private void Start()
    {
        SetupCamera();
    }

    private void OnEnable()
    {
        //SetupCamera();
    }

    private void SetupCamera()
    {
        _defaultBackground = _background.texture;

        var devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            D("Didn't find any camera module...");
            _isCamAvailable = false;
            return;
        }

        D("Device Count: " + devices.Length);

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                D($"{i}: {devices[i].name}");
                _backCameraTexture = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                break;
            }
        }

        if (_backCameraTexture == null)
        {
            D("Didn't find Front camera module...");
            return;
        }

        _backCameraTexture.Play();
        _background.texture = _backCameraTexture;

        _isCamAvailable = true;
    }

    private void Update()
    {
        if (!_isCamAvailable)
        {
            return;
        }

        float ratio = (float)_backCameraTexture.width / (float)_backCameraTexture.height;
        _fit.aspectRatio = ratio;

        float scaleY = _backCameraTexture.videoVerticallyMirrored ? -1f : 1f;
        _background.rectTransform.localScale = new Vector3(-1f, scaleY, 1f);

        //int orientation = -_backCameraTexture.videoRotationAngle;
        //_background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orientation);
    }

    public void OnButtonClicked()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Application.RequestUserAuthorization(UserAuthorization.WebCam);
            }
            else
            {
                SceneManager.LoadScene("Test_AR");
                VuforiaApplication.Instance.Initialize();
            }
        }
        else
        {
            SceneManager.LoadScene("Test_AR");
            VuforiaApplication.Instance.Initialize();
        }
    }

    #region Debug
    private static void D(string message)
    {
        Debug.Log($"<<Permission>>  {message}");
    }
    #endregion
}
