using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField]
    private Character playerData;

    public void InitilizePlayer()
    {
        currentData = Instantiate(playerData);
    }

    public void PlayerAttackEnd()
    {
        if (OnAttackEnded != null)
        {
            OnAttackEnded();
        }
        OnAttackEnded = null;
    }
}
