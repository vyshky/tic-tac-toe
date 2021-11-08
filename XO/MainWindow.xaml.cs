using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string FirstPlayer = "Ходит первый игрок";
        private readonly string SecondPlayer = "Ходит второй игрок";
        private bool Win { get; set; }
        private char Move;
        public MainWindow()
        {
            InitializeComponent();
            Move = 'O';
            Lable_Player.Content = FirstPlayer;
        }

        private void ChangeMove()
        {
            if (Move == 'X')
            {
                Lable_Player.Content = SecondPlayer;
                Move = 'O';
            }
            else
            {
                Lable_Player.Content = FirstPlayer;
                Move = 'X';
            }
        }

        private void Clear()
        {
            Button00.Content = null;
            Button01.Content = null;
            Button02.Content = null;
            Button10.Content = null;
            Button11.Content = null;
            Button12.Content = null;
            Button20.Content = null;
            Button21.Content = null;
            Button22.Content = null;
        }
        private bool Equals(Button buttonA, Button buttonB, Button buttonC)
        {
            if (buttonA.Content == null || buttonB.Content == null || buttonC.Content == null) return false;
            return buttonA.Content.ToString() == buttonB.Content.ToString() && buttonA.Content.ToString() == buttonC.Content.ToString();
        }

        private bool ThreeInRow()
        {
            bool threeInRow = false;
            // Вертикальные
            threeInRow = Equals(Button00, Button01, Button02); if (threeInRow) return true;
            threeInRow = Equals(Button10, Button11, Button12); if (threeInRow) return true;
            threeInRow = Equals(Button20, Button21, Button22); if (threeInRow) return true;

            // Горизонтальные
            threeInRow = Equals(Button00, Button10, Button20); if (threeInRow) return true;
            threeInRow = Equals(Button01, Button11, Button21); if (threeInRow) return true;
            threeInRow = Equals(Button02, Button12, Button22); if (threeInRow) return true;

            // Диагональные
            threeInRow = Equals(Button00, Button11, Button22); if (threeInRow) return true;
            threeInRow = Equals(Button02, Button11, Button20); if (threeInRow) return true;


            return threeInRow;
        }

        private void WinPlayer()
        {
            if (ThreeInRow())
            {
                Win = true;
                ChangeMove();
                Lable_Player.Content = $@"Победил {Move}";
            }
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            Lable_Player.Content = FirstPlayer;
            Move = 'O';
            Win = false;
            Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Win == true)
            {
                MessageBox.Show("WIN", "Click New Game", MessageBoxButton.OK);
                return;
            }
            //Button srcButton = e.Source as Button;
            Button dynamicButton = sender as Button;
            if (NotEmpty(dynamicButton)) return;
            dynamicButton.Content = Move;
            ChangeMove();
            WinPlayer();
        }

        private bool NotEmpty(ContentControl button)
        {
            if (button.Content == null) return false;
            return true;
        }

    }
}
