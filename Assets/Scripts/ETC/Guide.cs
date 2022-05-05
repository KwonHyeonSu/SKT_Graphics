namespace SKT
{
    using UnityEngine;

    public class Guide : MonoBehaviour
    {

        public void Interact_ShowGuide()
        {
            PopUpManager.Instance.ShowPopUp(PopUpManager.Instance.Guide);
        }
    }
}
