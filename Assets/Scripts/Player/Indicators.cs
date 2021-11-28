using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Indicators : MonoBehaviour
    {
        [SerializeField] private Image[] Lives;
        [SerializeField] private Sprite fullLive, emptyLive;

        [SerializeField] private Image[] Satiety;
        [SerializeField] private Sprite fullSatiety, emptySatiety;

        [SerializeField] private float HungeringSpeed;

        [SerializeField] private Animator animator;

        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private GameObject Pockets;

        public int health;
        [SerializeField] private int numberOfLives;

        public int satiety;
        [SerializeField] private int numberOfSatiety;


        private bool _isSatietyDying;

        private void Start() 
        {
            UpdateAllValues();
            StartCoroutine(Hungering());
        }

        public void UpdateAllValues()
        {
            HealthDiagnostic();
            SatietyDiagnostic();
        }

        public void HealthDiagnostic() 
        {
            if (health > numberOfLives)
            {
                health = numberOfLives;
            }

            for (int i = 0; i < Lives.Length; i++)
            {
                Lives[i].sprite = i < health ? fullLive : emptyLive;
                Lives[i].enabled = i < numberOfLives;
            }

            if (health <= 0)
            {
                StartCoroutine(DiePlayer());
            }

            if (health < 0) 
            {
                health = 0;
            }
        }

        public void SatietyDiagnostic() 
        {
            if (satiety > numberOfSatiety) 
            {
                satiety = numberOfSatiety;
            }

            for (int i = 0; i < Satiety.Length; i++)
            {
                Satiety[i].sprite = i < satiety ? fullSatiety : emptySatiety;
                Satiety[i].enabled = i < numberOfSatiety;
            }

            if (satiety <= 0 && _isSatietyDying == false) 
            {
                _isSatietyDying = true;
                StartCoroutine(SatietyDying());
            }
            else if (satiety > 0)
            {
                _isSatietyDying = false;
                StopCoroutine(SatietyDying());
            }

            if (satiety < 0) 
            {
                satiety = 0;
            }
        }

        private IEnumerator SatietyDying()
        {
            yield return new WaitForSeconds(3.5f);
            health--;
            HealthDiagnostic();
            StartCoroutine(SatietyDying());
        }

        private IEnumerator Hungering() 
        {
            yield return new WaitForSeconds(HungeringSpeed);
            satiety--;
            SatietyDiagnostic();
            StartCoroutine(Hungering());
        }

        private IEnumerator DiePlayer() 
        {
            animator.SetBool("isDead", true);
            yield return new WaitForSeconds(1);
            GameOverPanel.SetActive(true);
            Pockets.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
