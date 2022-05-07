namespace SKT
{    
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        }
        public void Interact_Item()
        {
            this.gameObject.SetActive(false);
        }   

    }

}