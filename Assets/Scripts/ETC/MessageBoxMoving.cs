namespace SKT
{
    using UnityEngine;
    using DG.Tweening;

    public class MessageBoxMoving : MonoBehaviour
    {
        // Interactable 오브젝트와 가까워지면 활성화됨

        Tween moveTween;
        void OnEnable()
        {
            this.transform.localPosition = Vector2.zero;
            moveTween = this.transform.DOLocalMoveY(this.transform.localPosition.y + 1, 0.2f).SetLoops(-1, LoopType.Yoyo);
        }

        void OnDisable()
        {
            moveTween.Kill();
        }


    }
}