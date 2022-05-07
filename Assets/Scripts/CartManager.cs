namespace SKT
{

    using UnityEngine;
    using UnityEngine.UI;

    public class CartManager : MonoBehaviour
    {
        public GameObject Panel_Cart;
        public Button Btn_Cart;

        void Start()
        {
            Btn_Cart.onClick.AddListener(() => {
                PopUpManager.Instance.ShowPopUp(Panel_Cart);
            });
        }

    }
}
