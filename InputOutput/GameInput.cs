using MySweeper.Basic;

namespace MySweeper.InputOutput
{
    public enum Action
    {
        MarkAsMine,
        UnmarkMine,
        RevealField,
        RevealAdjacent
    }

    public struct GameInput
    {
        public Coordinate Coordinate;
        public Action Action;
    }
}