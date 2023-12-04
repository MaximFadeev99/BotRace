public interface IInputHandler
{
    public float InquireInput();

    public void BlockRightTurn();

    public void BlockLeftTurn();

    public void RemoveTurnBlocks();
}