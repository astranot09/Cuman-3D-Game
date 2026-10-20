using UnityEngine;

public interface IInteractable
{
    public string nameInteract { get; }
    public void InInteractRange();
    public void OnInteract();
}
