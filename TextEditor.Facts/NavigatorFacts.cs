namespace TextEditor.Facts
{
    public class NavigatorFacts
    {
        [Fact]
        public void HandleInput_RightArrow_ShouldMoveOnePositionToRight()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(right);
            Assert.Equal(1, navigator.col);
        }

        [Fact]
        public void HandleInput_RightArrow_ShouldMoveMultiplePositionsToRight()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            Assert.Equal(4, navigator.col);
        }

        [Fact]
        public void HandleInput_LeftArrow_ShouldNotMoveFromFirstPosition()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo left = new ConsoleKeyInfo((char)ConsoleKey.LeftArrow, ConsoleKey.LeftArrow, false, false, false);
            navigator.HandleInput(left);
            Assert.Equal(0, navigator.col);
        }

        [Fact]
        public void HandleInput_LeftArrow_ShouldMoveOnePositionToLeft()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo left = new ConsoleKeyInfo((char)ConsoleKey.LeftArrow, ConsoleKey.LeftArrow, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(left);
            Assert.Equal(3, navigator.col);
        }

        [Fact]
        public void HandleInput_DownArrow_ShouldMoveOneDown()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            navigator.HandleInput(down);
            Assert.Equal(1, navigator.row);
        }

        [Fact]
        public void HandleInput_DownArrow_ShouldMoveMultiplePositionsDown()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            Assert.Equal(4, navigator.row);
        }

        [Fact]
        public void HandleInput_UpArrow_ShouldNotMoveUpWhenOnFirstLine()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo up = new ConsoleKeyInfo((char)ConsoleKey.UpArrow, ConsoleKey.UpArrow, false, false, false);
            navigator.HandleInput(up);
            Assert.Equal(0, navigator.row);
        }

        [Fact]
        public void HandleInput_UpArrow_ShouldMoveUp()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo up = new ConsoleKeyInfo((char)ConsoleKey.UpArrow, ConsoleKey.UpArrow, false, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(up);
            navigator.HandleInput(up);
            Assert.Equal(1, navigator.row);
        }

        [Fact]
        public void Move_RightArrow_ShouldMoveNPositionsToRight()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.RightArrow, 5);
            Assert.Equal(5, navigator.col);
        }

        [Fact]
        public void Move_RightArrow_ShouldNotMovePastRowLength()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.RightArrow, 100);
            Assert.Equal(text[navigator.row].Length, navigator.col);
        }

        [Fact]
        public void Move_LeftArrow_ShouldMoveNPositionsToLeft()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.RightArrow, 10);
            navigator.Move(ConsoleKey.LeftArrow, 5);
            Assert.Equal(5, navigator.col);
        }

        [Fact]
        public void Move_LeftArrow_ShouldNotMovePastFirstPosition()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.RightArrow, 10);
            navigator.Move(ConsoleKey.LeftArrow, 100);
            Assert.Equal(0, navigator.col);
        }

        [Fact]
        public void Move_DownArrow_ShouldMoveNPositionsDown()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.DownArrow, 10);
            Assert.Equal(10, navigator.row);
        }

        [Fact]
        public void Move_DownArrow_ShouldNotMovePastTextLength()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.DownArrow, 10000);
            Assert.Equal(text.Count - 1, navigator.row);
        }

        [Fact]
        public void Move_UpArrow_ShouldMoveNPositionsUp()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.DownArrow, 10);
            navigator.Move(ConsoleKey.UpArrow, 7);
            Assert.Equal(3, navigator.row);
        }

        [Fact]
        public void Move_UpArrow_ShouldNotMovePastFirstRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Move(ConsoleKey.UpArrow, 10000);
            Assert.Equal(0, navigator.row);
        }

        [Fact]
        public void HandleInput_ShiftFour_ShouldMoveToRowLength()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo symbol = new ConsoleKeyInfo('$', ConsoleKey.D4, true, false, false);
            navigator.HandleInput(symbol);
            Assert.Equal(text[navigator.row].Length, navigator.col);
        }

        [Fact]
        public void HandleInput_Zero_ShouldMoveToFirstCol()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo symbol = new ConsoleKeyInfo('0', ConsoleKey.D0, false, false, false);
            navigator.HandleInput(symbol);
            Assert.Equal(0, navigator.col);
        }

        [Fact]
        public void HandleInput_ShiftSix_ShouldMoveToFirstCharacterInRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo symbol = new ConsoleKeyInfo('^', ConsoleKey.D6, true, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(symbol);
            Assert.Equal(text[navigator.row].IndexOf(text[navigator.row].First(c => !char.IsWhiteSpace(c))), navigator.col);
        }

        [Fact]
        public void HandleInput_HomeKey_ShouldMoveToFirstCharacterInRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo home = new ConsoleKeyInfo((char)ConsoleKey.Home, ConsoleKey.Home, false, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(home);
            Assert.Equal(text[navigator.row].IndexOf(text[navigator.row].First(c => !char.IsWhiteSpace(c))), navigator.col);
        }

        [Fact]
        public void HandleInput_EndKey_ShouldMoveToRowLength()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo end = new ConsoleKeyInfo((char)ConsoleKey.End, ConsoleKey.End, false, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(down);
            navigator.HandleInput(end);
            Assert.Equal(text[navigator.row].Length, navigator.col);
        }

        [Fact]
        public void HandleInput_PageDown_ShouldMoveOnePageDown()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Drawer.windowHeight = 30;
            ConsoleKeyInfo pageDown = new ConsoleKeyInfo((char)ConsoleKey.PageDown, ConsoleKey.PageDown, false, false, false);
            navigator.HandleInput(pageDown);           
            Assert.Equal(2 * navigator.Drawer.windowHeight - 1, navigator.row);
        }

        [Fact]
        public void HandleInput_PageUp_ShouldMoveOnePageUp()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            navigator.Drawer.windowHeight = 30;
            ConsoleKeyInfo pageDown = new ConsoleKeyInfo((char)ConsoleKey.PageDown, ConsoleKey.PageDown, false, false, false);
            ConsoleKeyInfo pageUp = new ConsoleKeyInfo((char)ConsoleKey.PageUp, ConsoleKey.PageUp, false, false, false);
            navigator.HandleInput(pageDown);
            navigator.HandleInput(pageUp);
            Assert.Equal(0, navigator.row);
        }

        [Fact]
        public void HandleInput_w_ShouldMoveOneWordForward()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
            navigator.HandleInput(w);
            Assert.Equal(text[navigator.row].IndexOf('T'), navigator.col);
            Assert.Equal(0, navigator.row);
        }

        [Fact]
        public void HandleInput_w_ShouldWorkForDifferentLines()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            Assert.Equal(text[navigator.row].IndexOf('p'), navigator.col);
            Assert.Equal(2, navigator.row);
        }

        [Fact]
        public void HandleInput_w_ShouldStopOnPunctuationSigns()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            Assert.Equal(text[navigator.row].IndexOf(';'), navigator.col);
            Assert.Equal(4, navigator.row);
        }

        [Fact]
        public void HandleInput_W_ShouldMoveOneWordForward()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo W = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);
            navigator.HandleInput(W);
            Assert.Equal(text[navigator.row].IndexOf('T'), navigator.col);
        }

        [Fact]
        public void HandleInput_W_ShouldWorkForDifferentLines()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            Assert.Equal(text[navigator.row].IndexOf('p'), navigator.col);
            Assert.Equal(2, navigator.row);
        }

        [Fact]
        public void HandleInput_W_ShouldSkipPunctuationSigns()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            Assert.Equal(text[navigator.row].IndexOf('p'), navigator.col);
            Assert.Equal(5, navigator.row);
        }

        [Fact]
        public void HandleInput_b_ShouldMoveOneWordBackwards()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);
            ConsoleKeyInfo b = new ConsoleKeyInfo('b', ConsoleKey.B, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(b);
            Assert.Equal(text[navigator.row].IndexOf('p'), navigator.col);
            Assert.Equal(2, navigator.row);
        }

        [Fact]
        public void HandleInput_b_ShouldWorkForDifferentLines()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo w = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
            ConsoleKeyInfo b = new ConsoleKeyInfo('b', ConsoleKey.B, false, false, false);
            navigator.HandleInput(w);
            navigator.HandleInput(w);
            navigator.HandleInput(b);
            Assert.Equal(text[navigator.row].IndexOf('T'), navigator.col);
            Assert.Equal(0, navigator.row);
        }
    }
}