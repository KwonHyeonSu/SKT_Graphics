namespace SKT
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Sector : MonoBehaviour
    {
        public void Interact_Sector()
        {
            GameManager.Instance.sectorNum = int.Parse(this.name[8].ToString());

            PopUpManager.Instance.ShowPopUp(PopUpManager.Instance.Sector);
        }       
    }
}