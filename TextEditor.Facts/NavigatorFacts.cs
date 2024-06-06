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
            Assert.Equal(2 * navigator.Drawer.windowHeight - 2, navigator.row);
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

        [Fact]
        public void HandleInput_i_ShouldEnableInsertMode()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            navigator.HandleInput(ins);
            Assert.True(navigator.insertMode);
        }

        [Fact]
        public void HandleInput_ESC_ShouldDisableInsertMode()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo esc = new ConsoleKeyInfo((char)ConsoleKey.Escape, ConsoleKey.Escape, false, false, false);
            navigator.HandleInput(ins);
            Assert.True(navigator.insertMode);
            navigator.HandleInput(esc);
            Assert.False(navigator.insertMode);
        }

        [Fact]
        public void InsertMode_Text_FirstCol_ShouldInsertTextAtStartOfRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo T = new ConsoleKeyInfo('T', ConsoleKey.T, false, false, false);
            ConsoleKeyInfo E = new ConsoleKeyInfo('E', ConsoleKey.E, false, false, false);
            ConsoleKeyInfo S = new ConsoleKeyInfo('S', ConsoleKey.S, false, false, false);
            navigator.HandleInput(ins);
            Assert.Equal("namespace TextEditor", text[navigator.row]);
            navigator.HandleInput(T);
            navigator.HandleInput(E);
            navigator.HandleInput(S);
            navigator.HandleInput(T);
            Assert.Equal("TESTnamespace TextEditor", text[navigator.row]);
        }

        [Fact]
        public void InsertMode_Text_ShouldInsertTextAtCurrentCol()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo T = new ConsoleKeyInfo('T', ConsoleKey.T, false, false, false);
            ConsoleKeyInfo E = new ConsoleKeyInfo('E', ConsoleKey.E, false, false, false);
            ConsoleKeyInfo S = new ConsoleKeyInfo('S', ConsoleKey.S, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(ins);
            Assert.Equal("namespace TextEditor", text[navigator.row]);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(T);
            navigator.HandleInput(E);
            navigator.HandleInput(S);
            navigator.HandleInput(T);
            Assert.Equal("naTESTmespace TextEditor", text[navigator.row]);
        }


        [Fact]
        public void InsertMode_Enter_FirstCol_ShouldCreateNewEmptyLineAndMoveDownOneRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo enter = new ConsoleKeyInfo((char)ConsoleKey.Enter, ConsoleKey.Enter, false, false, false);
            navigator.HandleInput(ins);
            navigator.HandleInput(enter);
            Assert.Equal("", text[navigator.row - 1]);
            Assert.Equal("namespace TextEditor", text[navigator.row]);
        }

        [Fact]
        public void InsertMode_Enter_ShouldMoveRowContentFromSpecifiedPosToNextRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo enter = new ConsoleKeyInfo((char)ConsoleKey.Enter, ConsoleKey.Enter, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(ins);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(enter);
            Assert.Equal("na", text[navigator.row - 1]);
            Assert.Equal("mespace TextEditor", text[navigator.row]);
        }

        [Fact]
        public void InsertMode_Backspace_FirstCol_ShouldAppendRowToPreviousRowAndMoveColToPrevRowLength()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo backspace = new ConsoleKeyInfo((char)ConsoleKey.Backspace, ConsoleKey.Backspace, false, false, false);
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            int prevLength = navigator.text[navigator.row].Length;
            navigator.HandleInput(ins);
            navigator.HandleInput(down);
            navigator.HandleInput(backspace);
            Assert.Equal("namespace TextEditor{", text[navigator.row]);
            Assert.Equal(prevLength, navigator.col);
        }

        [Fact]
        public void InsertMode_Backspace_ShouldRemoveCharFromCurrentCol()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo backspace = new ConsoleKeyInfo((char)ConsoleKey.Backspace, ConsoleKey.Backspace, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(ins);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            navigator.HandleInput(backspace);
            navigator.HandleInput(backspace);
            Assert.Equal("mespace TextEditor", text[navigator.row]);
            Assert.Equal(0, navigator.col);
        }

        [Fact]
        public void InsertMode_DEL_LastCol_ShouldAppendNextRowToCurrentRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo del = new ConsoleKeyInfo((char)ConsoleKey.Delete, ConsoleKey.Delete, false, false, false);
            navigator.col = navigator.text[navigator.row].Length;
            navigator.HandleInput(ins);
            navigator.HandleInput(del);
            Assert.Equal("namespace TextEditor{", text[navigator.row]);
        }

        [Fact]
        public void InsertMode_DEL_ShouldRemoveCharFromNextColButNotModifyColPosition()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo del = new ConsoleKeyInfo((char)ConsoleKey.Delete, ConsoleKey.Delete, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
            navigator.HandleInput(ins);
            navigator.HandleInput(right);
            navigator.HandleInput(right);
            int prevCol = navigator.col;
            navigator.HandleInput(del);
            navigator.HandleInput(del);
            Assert.Equal("naspace TextEditor", text[navigator.row]);
            Assert.Equal(prevCol, navigator.col);
        }

        [Fact]
        public void HandleInput_O_ShouldAddNewLineBeforeCurrentLine()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo down = new ConsoleKeyInfo((char)ConsoleKey.DownArrow, ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo O = new ConsoleKeyInfo('O', ConsoleKey.O, false, false, false);
            navigator.HandleInput(down);
            navigator.HandleInput(O);
            Assert.Equal("", text[navigator.row]);
        }

        [Fact]
        public void HandleInput_o_ShouldAddNewLineAfterCurrentLine()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo o = new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false);
            int prevRow = navigator.row;
            Assert.Equal("{", text[navigator.row + 1]);
            navigator.HandleInput(o);
            Assert.Equal("", text[prevRow + 1]);
        }

        [Fact]
        public void HandleInput_a_ShouldEnableInsertModeAndMoveOnePosToRight()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo a = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);
            navigator.HandleInput(a);
            Assert.True(navigator.insertMode);
            Assert.Equal(1, navigator.col);
        }

        [Fact]
        public void HandleInput_A_ShouldEnableInsertModeAndMoveToEndOfRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo A = new ConsoleKeyInfo('A', ConsoleKey.A, false, false, false);
            navigator.HandleInput(A);
            Assert.True(navigator.insertMode);
            Assert.Equal(text[navigator.row].Length, navigator.col);
        }

        [Fact]
        public void HandleInput_I_ShouldEnableInsertModeAndMoveToFirstCharInRow()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo I = new ConsoleKeyInfo('I', ConsoleKey.I, false, false, false);
            navigator.HandleInput(I);
            Assert.True(navigator.insertMode);
            Assert.Equal(text[navigator.row].IndexOf(text[navigator.row].First(c => !char.IsWhiteSpace(c))), navigator.col);
        }

        [Fact]
        public void HandleInput_u_ShouldUndoChanges()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo esc = new ConsoleKeyInfo((char)ConsoleKey.Escape, ConsoleKey.Escape, false, false, false);
            ConsoleKeyInfo T = new ConsoleKeyInfo('T', ConsoleKey.T, false, false, false);
            ConsoleKeyInfo E = new ConsoleKeyInfo('E', ConsoleKey.E, false, false, false);
            ConsoleKeyInfo S = new ConsoleKeyInfo('S', ConsoleKey.S, false, false, false);
            ConsoleKeyInfo u = new ConsoleKeyInfo('u', ConsoleKey.U, false, false, false);
            navigator.HandleInput(ins);
            navigator.HandleInput(T);
            navigator.HandleInput(E);
            navigator.HandleInput(S);
            navigator.HandleInput(T);
            Assert.NotEqual(navigator.originalText, navigator.text);
            navigator.HandleInput(esc);
            navigator.HandleInput(u);
            Assert.Equal(navigator.originalText, navigator.text);
        }

        [Fact]
        public void HandleInput_CtrlR_ShouldRedoChanges()
        {
            string path = Path.GetFullPath("Test.txt");
            List<string> text = File.ReadAllLines(path).ToList();
            Navigator navigator = new Navigator(text, new Drawer(false, false), new CommandMode(""));
            ConsoleKeyInfo ins = new ConsoleKeyInfo('i', ConsoleKey.I, false, false, false);
            ConsoleKeyInfo esc = new ConsoleKeyInfo((char)ConsoleKey.Escape, ConsoleKey.Escape, false, false, false);
            ConsoleKeyInfo T = new ConsoleKeyInfo('T', ConsoleKey.T, false, false, false);
            ConsoleKeyInfo E = new ConsoleKeyInfo('E', ConsoleKey.E, false, false, false);
            ConsoleKeyInfo S = new ConsoleKeyInfo('S', ConsoleKey.S, false, false, false);
            ConsoleKeyInfo u = new ConsoleKeyInfo('u', ConsoleKey.U, false, false, false);
            ConsoleKeyInfo r = new ConsoleKeyInfo('r', ConsoleKey.R, false, false, true);
            navigator.HandleInput(ins);
            navigator.HandleInput(T);
            navigator.HandleInput(E);
            navigator.HandleInput(S);
            navigator.HandleInput(T);
            Assert.NotEqual(navigator.originalText, navigator.text);
            navigator.HandleInput(esc);
            navigator.HandleInput(u);
            Assert.Equal(navigator.originalText, navigator.text);
            navigator.HandleInput(r);
            Assert.Equal(navigator.currentText, navigator.text);
        }
    }
}