using System;
using MySweeper.Basic;
using MySweeper.InputOutput.Commands;

namespace MySweeper.InputOutput.Terminal
{
    public class Terminal
    {
        public TerminalPrinter TerminalPrinter { get; }
        public TerminalReader TerminalReader { get; }

        public Terminal()
        {
            this.TerminalPrinter = new TerminalPrinter();
            this.TerminalReader = new TerminalReader();
        }

        public void Initialize(Minefield minefield)
        {
            if (minefield == null)
            {
                throw new ArgumentNullException(nameof(minefield));
            }

            this.TerminalPrinter.Minefield = minefield;
            var commandFactory = new CommandFactory(minefield);
            this.TerminalReader.Initialize(minefield, commandFactory);
        }
    }
}