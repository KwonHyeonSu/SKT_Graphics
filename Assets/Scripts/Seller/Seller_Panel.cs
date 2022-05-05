namespace SKT
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;
    using TMPro;

    public class Seller_Panel : MonoBehaviour
    {


        [Header("할당해주기")]
        public List<GameObject> buildings;
        public GameObject highlight_icon;
        public TMP_InputField store_name_inputfield;
        public TextMeshProUGUI warning;
        public Button Btn_confirm;




        [Header("변수들")]
        public int selected_building_num = 0;
        private Sprite buildingSprite;


        // Btn 콜백함수 할당
        void Start()
        {
            foreach(var b in buildings)
            {
                // 원하는 건물 디자인을 클릭했을 때
                b.GetComponent<Button>().onClick.AddListener(()=>{
                    selected_building_num = int.Parse(b.name[10].ToString());
                    buildingSprite = b.GetComponent<Image>().sprite;
                    highlight_icon.transform.position = b.transform.position;
                });
            }

            Btn_confirm.onClick.AddListener(Confirm);
        }




        // 확인버튼 -> 건물을 만든다.
        public void Confirm()
        {
            //건물 이름
            string s = store_name_inputfield.text;

            //건물 이름 안적었을 때 예외 처리
            if(s == "")
            {
                warning.DOFade(1, 0.3f).OnComplete(()=>{
                    warning.DOFade(0, 0.3f);
                });
            }

            else
            {
                //건물을 짓고
                GameManager.Instance.BuildStructure(store_name_inputfield.text, buildingSprite);

                //창을 닫는다.
                PopUpManager.Instance.ClosePopUp();
            }

        }

    }
}
