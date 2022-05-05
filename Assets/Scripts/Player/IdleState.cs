namespace SKT
{
    using UnityEngine;
    public class IdleState : IState
    {
        private Player player;

        void IState.OnEnter(Player player)
        {
            //player 프로퍼티 초기화
            this.player = player;
            //초기화 구현하기

            player.playerState = PlayerState.Idle;

            player.animator.SetBool("walk", false);
            player.animator.SetBool("jump", false);
        }

        void IState.Update()
        {
            // Debug.Log("Idle State Enter");
        }

        void IState.OnExit()
        {
            // Debug.Log("Idle State Enter");
        }

    }
}
