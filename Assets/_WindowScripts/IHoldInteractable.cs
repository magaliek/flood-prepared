public interface IHoldInteractable
{
    void HoldInteract(float dt);
    void ResetHold();
    float GetProgress01();
    bool ShouldShowProgress();
}