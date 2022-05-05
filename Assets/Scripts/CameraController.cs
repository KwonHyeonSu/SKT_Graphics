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

        ///<params>
        /// store => 해당 store위치에 카메라를 고정한다.
        /// 
        ///</params>
        public void GetInStore(Transform store, Collider2D boundingBox)
        {
            //확대
            float _score = 15.0f;
            Tween t = DOTween.To(()=>_score, x=>_score = x, 2.0f, 0.5f).SetEase(Ease.OutExpo);
            t.OnUpdate(()=>{
                VC.m_Lens.OrthographicSize = _score;
            });




            VC.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = boundingBox;
            VC.gameObject.GetComponent<CinemachineConfiner>().m_ConfineScreenEdges = false;
        }

        public void GetOutStore()
        {
            //축소
            float _score = 2.0f;
            Tween t = DOTween.To(()=>_score, x=>_score = x, 15.0f, 0.5f).SetEase(Ease.OutExpo);
            t.OnUpdate(()=>{
                VC.m_Lens.OrthographicSize = _score;
            });



            VC.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = mapBound;
            VC.gameObject.GetComponent<CinemachineConfiner>().m_ConfineScreenEdges = true;
        }


    }

   
}