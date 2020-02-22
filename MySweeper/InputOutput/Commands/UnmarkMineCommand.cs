using MySweeper.Basic;
using log4net;

namespace MySweeper.InputOutput.Commands
{
    public class UnmarkMineCommand : AbstractCommand
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnmarkMineCommand));
        public UnmarkMineCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            Logger.DebugFormat("Unmark field [{0}] as mine.", this.Coordinate);
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = false;
        }

        public override void UndoExecution()
        {
            Logger.DebugFormat("Undo unmarking field [{0}] as mine.", this.Coordinate);
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = true;
        }
    }
}
