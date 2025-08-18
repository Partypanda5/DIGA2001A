using UnityEngine;

namespace Game.Weapons
{
    public class Sword : MonoBehaviour
    {
        public int SwordDamage = 10;

        public void PrintSwordDamage()
        {
            Debug.Log("The damage amount is: " + SwordDamage);
        }
    }
}