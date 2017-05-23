using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMaze.MultiPlayer;

namespace WpfMaze
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {

        private MultiPlayerViewModel vm;

        private ObservableCollection<string> listGames = new ObservableCollection<string>();
        public MultiPlayerWindow()
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(MultiPlayerModel.Instance);
            this.DataContext = vm;
            UpateComobox();


            //lbUsers.ItemsSource = users;
        }


        public void UpateComobox ()
        {
            List<string> list = vm.VM_ListOfGames();
            listGames.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listGames.Add(list[i]);
            }
            comboBox.ItemsSource = listGames;
        }


        private void StartButt_Click(object sender, RoutedEventArgs e)
        {
            WatingWindow watingWin = new WatingWindow();
            watingWin.Show();
            this.Close();
            watingWin.StartTheGame();
            //MultiMazeWindow multiWin = new MultiMazeWindow();
            //multiWin.Show();
            //this.Close();
            //multiWin.Operate("start");

        }

        private void JoinButt_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_MazeName = (string)comboBox.SelectedItem;
            vm.VM_JoinMaze();
            MultiMazeWindow multiWin = new MultiMazeWindow();
            multiWin.Show();
            this.Close();
            //MultiMazeWindow multiWin = new MultiMazeWindow();
            //multiWin.Show();
            //this.Close();
            //multiWin.Operate("join");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
