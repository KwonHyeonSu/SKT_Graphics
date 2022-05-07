namespace SKT
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {
        //UI Manager 인스턴스에 접근할 수 있는 프로퍼티
        private static UIManager instance = null;
        public static UIManager Instance
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

        private Transform selectPanel;
        public GameObject messageIcon;
        public GameObject Press_E;

        [Header("할당해주기")]
        public PopUpManager popUpManager;
        public Transform arrow;
        public GameObject Select_Charactor;

        private string currentSelected = "Astronaut";


        //////////////////Functions///////////////
        

        void Awake()
        {
            instance = this;
        }




        void Start()
        {

            //게임 시작할 때 가이드를 연다.
            popUpManager.ShowPopUp(popUpManager.Guide);
        }


        #region 버튼
        public void Btn_Astrounaut()
        {
            currentSelected = "Astronaut";
            Select(currentSelected);

        }

        public void Btn_Detective()
        {
            currentSelected = "Detective";
            Select(currentSelected);
        }

        public void Confirm()
        {
            GameManager.Instance.InitCharactor(currentSelected);
            Select_Charactor.SetActive(false);
            
        }


        private void Select(string s)
        {
            if(selectPanel == null)
                selectPanel = GameObject.Find("Canvas").transform.Find("Select_Charactor").Find("SelectPanel").transform;
            arrow.SetParent(selectPanel.Find(s));
            arrow.localPosition = new Vector3(0, 100, 0);
        }

        #endregion

        ///<summary>
        /// 상호작용 가능 UI 표시
        /// -> 왔다갔다 하는 오브젝트 띄우기
        ///</summary>
        public void Interacting(bool flag, Vector3 pos, bool ItemMode = false)
        {
            if(pos == null) pos = Vector3.zero;

            //메시지 아이콘 활성화
            messageIcon.SetActive(flag);
            messageIcon.transform.position = pos;
            if(ItemMode)
            {
                messageIcon.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            else messageIcon.transform.localScale = Vector3.one;


            //E키 가이드 버튼 활성화
            Press_E.SetActive(flag);

            //플레이어 접근 활성화 및 비활성화
            GameManager.Instance.player.canInteract = flag;
        }
    }
}
