using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace planerDnia
{
    public class Zadanie
    {
        public string Nazwa { get; set; }
        public string Kategoria { get; set; }
        public bool CzyUkonczone { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<Zadanie> listaZadan = new List<Zadanie>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Zadanie_Button(object sender, RoutedEventArgs e)
        {
            var selectedComboBoxItem = ComboBox.SelectedItem as ComboBoxItem;
            
            if (selectedComboBoxItem == null || string.IsNullOrEmpty(Zadanie.Text))
            {
                TextBlock.Text = "Błąd: Wprowadź nazwę zadania i wybierz kategorię.";
            }
            else
            {
                var noweZadanie = new Zadanie
                {
                    Nazwa = Zadanie.Text,
                    Kategoria = selectedComboBoxItem.Content.ToString(),
                    CzyUkonczone = false
                };
                
                listaZadan.Add(noweZadanie);
                DodajZadanie();
            }
        }

        private void DodajZadanie()
        {
            TextBlock.Text = "Lista zadań:\n";
            foreach (var zadanie in listaZadan)
            {
                TextBlock.Text += $"- {zadanie.Nazwa} ({zadanie.Kategoria})\n";
            }
        }
    }
}