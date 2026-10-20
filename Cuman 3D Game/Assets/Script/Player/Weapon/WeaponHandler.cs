using UnityEngine;


public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    [SerializeField] private TrailRenderer weaponTrailRenderer;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);

        weaponTrailRenderer.emitting = true;
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);

        weaponTrailRenderer.emitting = false;
    }
}
