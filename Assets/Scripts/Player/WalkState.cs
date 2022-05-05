namespace SKT
{
    using UnityEngine;
    public class WalkState : IState
    {
        private Player player;


        void IState.OnEnter(Player player)
        {
            //player 프로퍼티 초기화
            this.player = player;
            //초기화 구현하기
            
            player.playerState = PlayerState.Walk;
        }

        void IState.Update()
        {
            //충돌처리 Raycast
            bool canMove = CollitionDetect(player.dir);
            if(player.dir != Vector2.zero && canMove)
            {
                player.animator.SetBool("walk", true);
                player.transform.Translate(player.dir * player.speed * Time.deltaTime);
                XFlip(player.dir.x);
            }
            else
            {
                player.SetState(new IdleState());        
                player.animator.SetBool("walk", false);
            }
        }

        //움직일 때 충돌처리를 말끔하게 하기 위함 (Raycast)
        //"body" 태그와 부딪힐 때 움직임 제한
        private bool CollitionDetect(Vector2 dir)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, dir, 1.0f);

            for(int i=0;i<hits.Length;i++)
            {
                RaycastHit2D hit = hits[i]; 
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Body"))
                {
                    //Debug.Log("못감");
                    return false;
                }
            }

            //for debug
            if(GameManager.Instance.DebugMode == true)
            {
                Debug.DrawRay(player.transform.position, dir, Color.red);
            }
            
            return true;
        }

        void IState.OnExit()
        {
            player.animator.SetBool("walk", false);
        }


        //sprite x 반전
        
        void XFlip(float x)
        {
            
            if(x < 0)
            {
                player.spriteRenderer.flipX = true;
            }
            else if(x > 0)
            {
                player.spriteRenderer.flipX = false;
            }
        }

    }
}
