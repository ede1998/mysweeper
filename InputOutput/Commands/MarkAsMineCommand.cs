using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public class MarkAsMineCommand : AbstractCommand
    {
        public MarkAsMineCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = true;
        }

        public override void UndoExecution()
        {
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = false;
        }
    }
}