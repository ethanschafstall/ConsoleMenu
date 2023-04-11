namespace ConsoleMenu
{
    public class Menu
    {
        enum MenuTypes
        {
            vertical = 1,
            horizontal = 2,
        }
        private int _verticalPadding = 1;
        /// <summary>
        /// Set vertical padding for each menu item.
        /// </summary>
        public int VerticalPadding
        {
            get { return _verticalPadding; }
            set { _verticalPadding = value; }
        }

        private int _horizontalPadding = 0;
        /// <summary>
        /// Set horizontal padding for each menu item.
        /// </summary>
        /// 
        public int HorizontalPadding
        {
            get { return _horizontalPadding; }
            set { _horizontalPadding = value; }
        }

        private string _menuPrompt = "";

        /// <summary>
        /// Set selected item prefix.
        /// </summary>
        public string MenuPrompt
        {
            get { return _menuPrompt; }
            set { _menuPrompt = value; }
        }

        public List<string> MenuOptions { get; set; } = new List<string>();

        private ConsoleColor _itemTextColor = ConsoleColor.White;
        public ConsoleColor ItemTextColor
        {
            get { return _itemTextColor; }
            set { _itemTextColor = value; }
        }

        private ConsoleColor _selectedItemTextColor = ConsoleColor.Black;
        public ConsoleColor SelectedItemTextColor
        {
            get { return _selectedItemTextColor; }
            set { _selectedItemTextColor = value; }
        }

        private ConsoleColor _itemBackgroundColor = ConsoleColor.Black;
        public ConsoleColor ItemBackgroundColor
        {
            get { return _itemBackgroundColor; }
            set { _itemBackgroundColor = value; }
        }

        private ConsoleColor _selectedItemBackgroundColor = ConsoleColor.White;
        public ConsoleColor SelectedItemBackgroundColor
        {
            get { return _selectedItemBackgroundColor; }
            set { _selectedItemBackgroundColor = value; }
        }

        private string _selectedItemPrefix = ">";
        /// <summary>
        /// Set selected item prefix.
        /// </summary>
        public string SelectedItemPrefix
        {
            get { return _selectedItemPrefix; }
            set { _selectedItemPrefix = value; }
        }


        private int _menuType = (int)MenuTypes.vertical;
        /// <summary>
        /// Set menu type. (vertical:1, horizontal:2)
        /// </summary>
        public int MenuType
        {
            get { return _menuType; }
            set { _menuType = value; }
        }

        private int _promptSpacing = 1;
        /// <summary>
        /// Set prompt spacing. (Space between menu prompt and first menu item)
        /// </summary>
        public int PromptSpacing
        {
            get { return _promptSpacing; }
            set { _promptSpacing = value; }
        }

        private int _selectedIndex = 0;


        private string _textJustify = "left";

        /// <summary>
        /// Set text justify. ("center", "right", or "left"). left by default.
        /// </summary>
        public string TextJustify
        {
            get { return _textJustify; }
            set { _textJustify = value; }
        }

        private int[] _menuPosition = new int[] { 1, 2 };
        /// <summary>
        /// Set menu position.
        /// </summary>
        public int[] MenuPosition
        {
            get { return _menuPosition; }
            set { _menuPosition = value; }
        }
        public Menu() { }
        public int Run()
        {
            ConsoleKey keyPressed;
            Console.SetCursorPosition(_menuPosition[0], _menuPosition[1]);
            Console.Write(_menuPrompt);
            do
            {
                Writer();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    break;
                }

                int indexChange = GetIndexChange(keyPressed);
                _selectedIndex += indexChange;

                // Ensure selected index is within bounds of menu options
                if (_selectedIndex < 0)
                {
                    _selectedIndex = MenuOptions.Count - 1;
                }
                else if (_selectedIndex >= MenuOptions.Count)
                {
                    _selectedIndex = 0;
                }

            } while (true);

            return _selectedIndex;
        }
        private void Writer()
        {
            int maxLength = MenuOptions.Max(str => str.Length);
            string prefixText = _selectedItemPrefix;
            int frontSpacer = 0;
            int backSpacer = 0;

            int horizontalAxisPostioning = _menuPosition[0] + _horizontalPadding, verticalAxisPostioning = _menuPosition[1] + _verticalPadding + _promptSpacing;
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                switch (_textJustify)
                {
                    case "right":
                        frontSpacer = maxLength - (MenuOptions[i].Length + prefixText.Length) + _selectedItemPrefix.Length;
                        backSpacer = 0;
                        break;
                    case "left":
                        frontSpacer = 0;
                        backSpacer = maxLength - (MenuOptions[i].Length + prefixText.Length) + _selectedItemPrefix.Length;
                        break;
                    case "center":
                        frontSpacer = (maxLength - (MenuOptions[i].Length + prefixText.Length)) / 2 + _selectedItemPrefix.Length;
                        backSpacer = (maxLength - (MenuOptions[i].Length + prefixText.Length)) / 2 + _selectedItemPrefix.Length;
                        break;
                }

                Console.SetCursorPosition(horizontalAxisPostioning, verticalAxisPostioning + VerticalPadding * i);
                string currentOption = MenuOptions[i];
                string prefix;

                // If option equates to selectedIndex, then option is "active", change prefix, foreground and background color.
                if (_selectedIndex == i)
                {
                    prefixText = _selectedItemPrefix;
                    Console.ForegroundColor = _itemBackgroundColor;
                    Console.BackgroundColor = _selectedItemBackgroundColor;
                }
                // Else standard prefix and colors.
                else
                {
                    prefixText = new string(' ', SelectedItemPrefix.Length);
                    Console.ForegroundColor = _itemTextColor;
                    Console.BackgroundColor = _itemBackgroundColor;
                }
                Console.Write(new String(' ', maxLength + _selectedItemPrefix.Length));
                Console.SetCursorPosition(horizontalAxisPostioning + HorizontalPadding * i, verticalAxisPostioning + VerticalPadding * i);
                Console.Write(new String(' ', frontSpacer) + prefixText + MenuOptions[i] + new String(' ', backSpacer));
            }

        }
        private int GetIndexChange(ConsoleKey key)
        {
            switch (_menuType)
            {
                case (int)MenuTypes.vertical:
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            return -1;
                            break;
                        case ConsoleKey.DownArrow:
                            return 1;
                            break;
                    }
                    break;

                case (int)MenuTypes.horizontal:
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            return -1;
                            break;
                        case ConsoleKey.RightArrow:
                            return 1;
                            break;
                    }
                    break;
            }
            return 0;
        }
    }
}