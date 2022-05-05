namespace SKT
{
    
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    public class PopUpManager : MonoBehaviour
    {

        
        //UI Manager 인스턴스에 접근할 수 있는 프로퍼티
        private static PopUpManager instance = null;
        public static PopUpManager Instance
        {
            get{
                if(null == instance)
                {
                    return null;
                }
                return instance;
            }
        }

        //////////////////Variables///////////////

        [Header("할당해주기")]
        public GameObject background; //팝업 백그라운드

        public Transform popupParent;


        [Header("자동 할당 확인용")]
        public Image bg_Image; //백그라운드 스프라이트
        public GameObject Guide;
        public GameObject Sector;
        [SerializeField]
        private GameObject currentPopup;


        //////////////////Functions///////////////
        


        ///<summary>
        /// background 초기화
        ///</summary>
        void Awake()
        {
            instance = this;

            //background 클릭 시 ClosePopUp 호출
            background.GetComponent<Button>().onClick.AddListener(()=>{
                ClosePopUp();
            });

            Allocate();
        }

        void Allocate()
        {
            bg_Image = background.GetComponent<Image>();
            Guide = Resources.Load("Popup/Guide") as GameObject;
            Sector = Resources.Load("Popup/Sector") as GameObject;
            
        }
        
        void LateUpdate()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePopUp();
            }
        }

        ///<summary>
        /// 팝업을 띄운다.
        ///</summary>
        public void ShowPopUp(GameObject PopUp)
        {
            Debug.Log("show PopUp");

            //background On
            background.SetActive(true);
            bg_Image.DOFade(0.6f, 0.5f);

            //show PopUp
            var GO = Instantiate(PopUp, Vector3.zero, Quaternion.identity);
            GO.transform.SetParent(popupParent);
            GO.transform.localScale = Vector3.one;
            GO.transform.localPosition = Vector3.zero;
            currentPopup = GO;

            GameManager.Instance.gameState = GameState.Stop;

        }

        ///<summary>
        ///현재 띄워진 팝업을 종료한다.
        ///</summary>
        public void ClosePopUp()
        {
            if (currentPopup == null) return;

            Debug.Log("close PopUp");

            //background off
            bg_Image.DOFade(0, 0.5f).OnComplete(()=>{
                background.SetActive(false);
            });

            //Popup Close
            currentPopup.SetActive(false);
            Destroy(currentPopup);

            currentPopup = null;

            GameManager.Instance.gameState = GameState.Playing;
        }
    }

}
