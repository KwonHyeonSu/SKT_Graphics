namespace SKT
{
    using UnityEngine;
    using DG.Tweening;
    public class JumpState : IState
    {
        private Player player;
        private Vector2 oldPos = Vector2.zero;
        void IState.OnEnter(Player player)
        {
            //player 프로퍼티 초기화
            this.player = player;
            oldPos = player.transform.position;
            
            //초기화 구현하기
            player.animator.SetBool("jump", true);
            player.playerState = PlayerState.Jump;

            player.transform.DOLocalMoveY(oldPos.y + 1,jumpTime*0.5f).SetEase(Ease.OutExpo).OnComplete(()=>{
                player.transform.DOLocalMoveY(oldPos.y, jumpTime*0.5f).SetEase(Ease.InExpo);
            }); //점프 구현
        }

        float jumpTime = 0.6f;
        float jumpTime_t = 0.0f;

        void IState.Update()
        {
            jumpTime_t += Time.deltaTime;

            if(jumpTime_t > jumpTime)
            {
                player.SetState(new IdleState());
            }
        
        }
        void IState.OnExit()
        {
            player.animator.SetBool("jump", false);
        }

    }
}
