namespace SKT
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class CartPopup : MonoBehaviour
    {
        public Button Btn_Buy;
        
        public GameObject Buy_Complete_OBJ;

        void Awake()
        {
            Buy_Complete_OBJ.SetActive(false);
        }

        public void Btn_Buy_Click()
        {
            Buy_Complete_OBJ.SetActive(true);

            StartCoroutine(DeActive());

        }

        
        IEnumerator DeActive()
        {
            yield return new WaitForSeconds(2.0f);

            Buy_Complete_OBJ.SetActive(false);
        }        
    }

}