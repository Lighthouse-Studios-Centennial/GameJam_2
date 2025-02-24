using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class GalleryController : MonoBehaviour
{
    [SerializeField] RectTransform canvasMask;
    [SerializeField] private Image canvasImage;
    [SerializeField] private Sprite[] galleryImages;

    [SerializeField] private float canvasAnimationDuration = 0.25f;
    private int currentImageIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        currentImageIndex++;
        if (currentImageIndex >= galleryImages.Length)
        {
            currentImageIndex = 0;
        }

        CloseMask();

        yield return new WaitForSeconds(canvasAnimationDuration);

        canvasImage.sprite = galleryImages[currentImageIndex];

        OpenMask();   
    }

    IEnumerator ShowPreviousImage()
    {
        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentImageIndex = galleryImages.Length - 1;
        }

        CloseMask();

        yield return new WaitForSeconds(canvasAnimationDuration);

        canvasImage.sprite = galleryImages[currentImageIndex];

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
