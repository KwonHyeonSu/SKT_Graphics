namespace SKT
{
    using UnityEngine;

    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance
        {
            get { return instance; }
        }

        public AudioSource ClickSound;

        public void Click_Sound()
        {
            ClickSound.Play();
        }
    }
}
