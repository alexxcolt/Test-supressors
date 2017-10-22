//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//MainWindow.xaml.cs - класс главного окна
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.Models;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;
using System.Diagnostics;
using System.Threading;
using Client.ViewModels;

namespace Client.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {  
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатие кнопки Exit
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатие кнопки About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("БЕЗЛЕПКИН", "About");
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатие кнопки "Добавить устройство"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void MenuItem_Click_Settings(object sender, RoutedEventArgs e)
        {
            Window AddDev = new AddModel();
            //Показать диалоговое окно
            AddDev.ShowDialog();
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатие кнопки "Запуск"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel).IsButtonWillClick();
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатии кнопки "Полный список"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Button_Of_FullList(object sender, RoutedEventArgs e)
        {
            Window AddDev = new AddModel();
            AddDev.ShowDialog();
        }
        /// <summary>
        /// Обработчик события, возникающего при изменении содержимого ComboBox
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void ComboForIzm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as ViewModel).IsComboBoxChanged(Plot1);
        }
    }
}
