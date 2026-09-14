public interface IInteractable
{
    public bool Cancelable { get; set; }
    public void ShowInfos();
    public void HideInfos();
    public void Interact();
    public void CancelInteraction();
}
