namespace SKT
{
    using UnityEngine;
    using Cinemachine;
    using DG.Tweening;
    public class CameraController : MonoBehaviour
    {
        private static CameraController instance;
        public static CameraController Instance
        {
            get { return instance; }
        }


        public Collider2D mapBound;

        void Awake()
        {
            instance = this;
        }

        public CinemachineVirtualCamera VC;

        //초기화
        void Start()
        {
            VC.m_Lens.OrthographicSize = 15.0f;
        }

        public void Follow(Transform target)
        {
            VC.Follow = target;
        }


        public void GetInStore(Collider2D boundingBox)
        {
            //확대
            float _score = 15.0f;
            Tween t = DOTween.To(()=>_score, x=>_score = x, 2.0f, 0.5f).SetEase(Ease.OutExpo);
            t.OnUpdate(()=>{
                VC.m_Lens.OrthographicSize = _score;
            });
        }

        public void GetOutStore()
        {
            //축소
            float _score = 2.0f;
            Tween t = DOTween.To(()=>_score, x=>_score = x, 15.0f, 0.5f).SetEase(Ease.OutExpo);
            t.OnUpdate(()=>{
                VC.m_Lens.OrthographicSize = _score;
            });

        }


    }

   
}