public interface IInputService
{
    float Horizontal { get; }
    bool JumpPressed { get; }
    bool ShootPressed { get; }

    void SetLeft(bool state);
    void SetRight(bool state);
    void Jump();
    void Shoot();
    void Reset();
}

public class UIInputService : IInputService
{
    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool ShootPressed { get; private set; }

    public void SetLeft(bool state) => Horizontal = state ? -1 : 0;
    public void SetRight(bool state) => Horizontal = state ? 1 : 0;
    public void Jump() => JumpPressed = true;
    public void Shoot() => ShootPressed = true;

    public void Reset() => (JumpPressed, ShootPressed) = (false, false);
}
