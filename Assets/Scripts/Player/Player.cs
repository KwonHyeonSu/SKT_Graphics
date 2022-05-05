namespace SKT
{
    using System.Collections;
    using UnityEngine;
    using DG.Tweening;

    //Player State를 관장하는 인터페이스
    public interface IState
    {
        // OnEnter에 파라미터가 들어간 이유 : 각 State클래스 내에서 필요한 함수를 player를 통해 접근할 수 있도록 하기 위해
        void OnEnter(Player player); 
        void Update();
        void OnExit();
    }

    // 플레이어 상태 정의
    public enum PlayerState{
        Idle, Walk, Jump
    }

    public class Player : MonoBehaviour
    {
        //////////////////Variables///////////////

        [Header("외부 오브젝트 참조 (자동할당)")]
        public CameraController cameraController;



        [Header("상태 관련")]
        private IState currentState;        // 현재 상태 IState
        public PlayerState playerState;     // 플레이어 상태


        [Header("컴포넌트 (자동할당)")]
        public SpriteRenderer spriteRenderer;// spriteRenderer
        public Animator animator;           // animator



        [Header("움직임 관련")]
        public Vector2 dir = Vector2.zero;  // Player Move
        public float speed = 5.0f;


        [Header("변수")]
        public bool canInteract = false;            //상호작용할 수 있는 상태인지
        


        //////////////////Functions///////////////


        /// <summary>
        /// 1. Allocate : 변수 및 컴포넌트 할당
        /// 2. SetState : 초기 Idle 상태 할당
        /// 3. 움직임 구현 (Coroutine)
        /// </summary>
        private void Start()
        {
            Allocate();
            SetState(new IdleState());
            StartCoroutine(MoveRoutine());
        }


        /// 변수 및 컴포넌트 할당
        private void Allocate()
        {
            cameraController = GameObject.Find("CM vcam1").GetComponent<CameraController>();
            animator = this.GetComponent<Animator>();
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        }


        
        /// 각종 로직 수행
        /// 1. 현재 상태에 따른 수행
        /// 2. 키보드 입력 움직임
        /// 3. 상호작용
        private IEnumerator MoveRoutine()
        {
            while(true)
            {
                //게임 상태가 Playing 상태일 때만 움직일 수 있다.
                if(GameManager.Instance.gameState == GameState.Playing)
                {
                    //현재 상태에 대한 수행을 매 프레임 진행한다.
                    currentState.Update();

                    //키보드 입력에 따른 수행
                    KeyboardInput();

                    //상호작용 키
                    Interact();
                }

                yield return null;
            }
        }

        

        #region KeyboardInput()

        ///<summmary>
        /// space           -> 점프
        /// 방향키 및 wasd   -> 상하좌우 이동
        ///</summary>
        float h = 0.0f;
        float v = 0.0f;        
        private void KeyboardInput()
        {
            //JUMP
            if(Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Jump)
            {
                SetState(new JumpState());
            }

            //MOVE
            else if (playerState != PlayerState.Jump)
            {

                //Get I/O Input
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
                
                //Set Moving Direction
                dir = Vector2.up * v + Vector2.right * h;

                //Move 적용
                if(dir != Vector2.zero && playerState != PlayerState.Walk)
                    SetState(new WalkState());

            }
        }

        #endregion

        #region Interact()

        

        void Interact()
        {
            if(!canInteract) return;

            if(Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.interactFunc();
            }
        }

        #endregion

        #region SetState()
        ///<summmary>
        /// 상태 적용
        ///</summary>
        public void SetState(IState nextState)
        {
            //현재 State 종료
            if(currentState != null)
            {
                currentState.OnExit();
            }    

            //다음 State 시작
            currentState = nextState;
            currentState.OnEnter(this);
        }

        #endregion

        #region 충돌 감지
        ///<summary>
        ///플레이어와 Trigger 충돌이 발생했을 때 가이드를 표시해줌
        ///
        private void OnTriggerEnter2D(Collider2D other) {
            
            // Guide
            if(other.gameObject.layer == LayerMask.NameToLayer("Guide"))
            {
                Vector3 spawnPos = new Vector3( other.transform.localPosition.x, 
                other.transform.localPosition.y + other.transform.localScale.y + 1);

                UIManager.Instance.Interacting(true, spawnPos);

                //델리게이트 함수 적용
                GameManager.Instance.interactFunc = other.GetComponent<Guide>().Interact_ShowGuide;
            }



            // Sector
            if(other.gameObject.layer == LayerMask.NameToLayer("Sector"))
            {
                Vector3 spawnPos = new Vector3( other.transform.localPosition.x, 
                other.transform.localPosition.y + other.transform.localScale.y + 1);

                //건물지을 상호작용 활성화
                UIManager.Instance.Interacting(true, spawnPos);

                //상호작용 함수 적용 (델리게이트)
                GameManager.Instance.interactFunc = other.GetComponent<Sector>().Interact_Sector;
            }


            // Store
            if(other.gameObject.layer == LayerMask.NameToLayer("Store"))
            {
                GetInStore(other.gameObject);
            }




        }

        private void OnTriggerExit2D(Collider2D other) {
            UIManager.Instance.Interacting(false, Vector3.zero);
            /*
            //Guide
            if(other.gameObject.layer == LayerMask.NameToLayer("Guide"))
            {
                UIManager.Instance.Interacting(false, Vector3.zero);
            }

            // Sector
            if(other.gameObject.layer == LayerMask.NameToLayer("Sector"))
            {
                UIManager.Instance.Interacting(false, Vector3.zero);
            }*/

            // Sector
            if(other.gameObject.layer == LayerMask.NameToLayer("Store"))
            {
                GetOutStore();
            }

        }

        #endregion



        //상점에 들어갔을 때
        public void GetInStore(GameObject store)
        {
            cameraController.GetInStore(store.transform, store.GetComponent<Collider2D>());
            
            //플레이어 작게
            this.transform.DOMoveX(this.transform.position.x + dir.x, 0.2f).SetEase(Ease.OutExpo);
            this.transform.DOScale(new Vector3(0.2f, 0.2f, 1), 0.5f);
            //속도 줄이기
            speed = 1.0f;
        }

        //상점에서 나왔을 때
        public void GetOutStore()
        {
            cameraController.GetOutStore();
            this.transform.DOMoveX(this.transform.position.x + dir.x, 0.1f).SetEase(Ease.OutExpo);
            this.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.5f);
            speed = 5.0f;
        }

    }

}
