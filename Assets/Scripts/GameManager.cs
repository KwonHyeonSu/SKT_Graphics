namespace SKT
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum GameState
    {
        Stop,
        Playing
    }

    public class GameManager : MonoBehaviour
    {
        #region 싱글톤
        private static GameManager instance = null;

        void Awake()
        {
            if(null == instance)
            {
                instance = this;

                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //게임매니저 인스턴스에 접근할 수 있는 프로퍼티
        public static GameManager Instance
        {
            get{
                if(null == instance)
                {
                    return null;
                }
                return instance;
            }
        }

        #endregion

        //////////////////Variables///////////////

        public GameState gameState = GameState.Stop;
        public Player player;
        public List<GameObject> Sectors; // 맵의 여러 섹터들 (건물을 지을 수 있는 곳)

        [Header("할당해주기")]
        public Transform Buildings_parent;
        
        //상호작용 델리게이트
        public delegate void InteractDelegate();
        public InteractDelegate interactFunc;


        [Header("For DebugMode")]
        public bool DebugMode = false;
        public UIManager uIManager;


        //////////////////Functions///////////////


        void Start()
        {
            //개발자모드
            if(DebugMode == true)
            {
                InitCharactor("Astronaut");
                uIManager.Select_Charactor.SetActive(false);
            }

            else
            {
                uIManager.Select_Charactor.SetActive(true);
                //시간을 멈춰서 캐릭터를 고를 시간에 불필요한 오버헤드를 줄이도록 한다.
                Time.timeScale = 0.0f;
            }

            gameState = GameState.Stop;

        }

        //UIManager에서 캐릭터 선택 후 수행
        public void InitCharactor(string currentSelected)
        {
            string path = "Animations/" + currentSelected + "/" + currentSelected;

            var PlayerGO = Resources.Load("Player") as GameObject;
            player = Instantiate(PlayerGO, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            PlayerGO.name = "Player_" + currentSelected;

            
            player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(path) as RuntimeAnimatorController;

            Time.timeScale = 1.0f;

            gameState = GameState.Playing;

            CameraController.Instance.VC.Follow = player.transform;

        }

        
        //건물을 짓는 함수
        public int sectorNum = 0; //어디 섹터에 지을것인지
        public void BuildStructure(string storeName, Sprite storeSprite)
        {
            player.GetInStore(Sectors[sectorNum-1]);

            //빌딩을 짓고 컴포넌트를 할당해준다.
            GameObject go = new GameObject("Building_" + sectorNum);
            go.AddComponent<SpriteRenderer>().sprite = storeSprite;
            go.AddComponent<BoxCollider2D>().isTrigger = true;
            go.transform.position = Sectors[sectorNum-1].transform.position;
            go.transform.SetParent(Buildings_parent);


            //sector sprite 끄고 layer 바꾸기
            Sectors[sectorNum-1].GetComponent<SpriteRenderer>().enabled = false;
            Sectors[sectorNum-1].layer = LayerMask.NameToLayer("Store");
            UIManager.Instance.Interacting(false, Vector3.zero);
        }

        
    }    
}
