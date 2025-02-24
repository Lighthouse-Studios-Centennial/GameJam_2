using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.Video;


public class GalleryController : MonoBehaviour
{
    [SerializeField] RectTransform canvasMask;
    [SerializeField] private Image canvasImage;
    [SerializeField] private RawImage canvasRawImage;
    [SerializeField] private Sprite[] galleryImages;
    [SerializeField] private VideoClip[] galleryVideos;
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private float canvasAnimationDuration = 0.25f;
    private int index = 0;
    private int imageSize = 0;
    private int videoSize = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageSize = galleryImages.Length;
        videoSize = galleryVideos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Next Image")]
    public void NextImage()
    {
        StartCoroutine(ShowNextImage());
    }

    [ContextMenu("Previous Image")]
    public void PreviousImage()
    {
        StartCoroutine(ShowPreviousImage());
    }

    IEnumerator ShowNextImage()
    {
        index++;

        if(index >= imageSize + videoSize)
        {
            index = 0;
        }
        // currentImageIndex++;

        // if (currentImageIndex >= galleryImages.Length)
        // {
        //     currentImageIndex = 0;
        // }

        CloseMask();

        yield return new WaitForSeconds(canvasAnimationDuration);

        if(index < imageSize)
        {
            canvasImage.sprite = galleryImages[index];
            canvasImage.gameObject.SetActive(true);
            canvasRawImage.gameObject.SetActive(false);
        }
        else
        {
            videoPlayer.clip = galleryVideos[index - imageSize];
            canvasRawImage.gameObject.SetActive(true);
            canvasImage.gameObject.SetActive(false);
        }

        OpenMask();   
    }

    IEnumerator ShowPreviousImage()
    {
        index--;

        if(index < 0)
        {
            index = imageSize + videoSize - 1;
        }

        CloseMask();

        yield return new WaitForSeconds(canvasAnimationDuration);

        if(index < imageSize)
        {
            canvasImage.sprite = galleryImages[index];
            canvasImage.gameObject.SetActive(true);
            canvasRawImage.gameObject.SetActive(false);
        }
        else
        {
            videoPlayer.clip = galleryVideos[index - imageSize];
            canvasRawImage.gameObject.SetActive(true);
            canvasImage.gameObject.SetActive(false);
        }

        OpenMask();
    }
    void CloseMask()
    {
        canvasMask.DOSizeDelta(new Vector2(19.2f, 0), canvasAnimationDuration).SetEase(Ease.InCubic);
    }

    void OpenMask()
    {
        canvasMask.DOSizeDelta(new Vector2(19.2f, 10.8f), canvasAnimationDuration).SetEase(Ease.OutCubic);
    }
}
