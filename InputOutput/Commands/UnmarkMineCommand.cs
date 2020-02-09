using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public class UnmarkMineCommand : AbstractCommand
    {
        public UnmarkMineCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = false;
        }

        public override void UndoExecution()
        {
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = true;
        }
    }
}
