using UnityEngine;

namespace Game.Weapons
{
    public class Gun : MonoBehaviour
    {
        public int GunDamage = 10;

        public void PrintGunDamage()
        {
            Debug.Log("The damage amount is: " + GunDamage);
        }
    }
}