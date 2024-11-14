using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Button frame;
    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve angularCurve;

    private Sprite cardSprite;
    private bool isDisplayingImage = false;

    public Card card { get; private set; }

    public void Initialize(Card _card, Sprite sprite)
    {
        card = _card;
        cardSprite = sprite;

        frame.onClick.AddListener(OnClickCard);
    }

    private void OnClickCard()
    {
        if (!isDisplayingImage)
        {
            GameEvents.OnSelectCard?.Invoke(this);
            Display();
        }
    }

    public void Hide()
    {
        if (isDisplayingImage)
        {
            GameEvents.OnHideCard?.Invoke(this);
            isDisplayingImage = false;
            StartCoroutine(FlipAnimation(null));
        }
    }

    public void Display()
    {
        if (!isDisplayingImage)
        {
            isDisplayingImage = true;
            StartCoroutine(FlipAnimation(cardSprite));
        }
    }

    private IEnumerator FlipAnimation(Sprite newImage)
    {
        var instance = frame.transform;
        float initialAngle = instance.localEulerAngles.y;
        float targetAngle = initialAngle + 180;
        float diff = targetAngle - initialAngle;

        bool isNewImageSet = false;
        float animatedTime;

        yield return LoopUtility.Tween((normalizedTime) =>
        {
            animatedTime = angularCurve.Evaluate(normalizedTime);
            instance.localEulerAngles = Vector3.up * (initialAngle + diff * animatedTime);

            //when it reaches the half of rotation
            if (!isNewImageSet && animatedTime > 0.5f)
            {
                isNewImageSet = true;
                ChangeImage(newImage);
            }
        }
        , 0.38f);
    }

    private void ChangeImage(Sprite newImage)
    {
        image.color = newImage == null ? Color.clear : Color.white;
        image.sprite = newImage;
    }
}
