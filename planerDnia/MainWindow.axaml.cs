using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;

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
            string tekstZadania = Zadanie.Text;
            TextBlock.Text = tekstZadania;
            TextBlockGrid.Text = tekstZadania;

            TextBlock.Text = Zadanie.Text;
            TextBlockGrid.Text = Zadanie.Text;
            GridComboBox.SelectedIndex = ComboBox.SelectedIndex;

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
                    CzyUkonczone = CheckBoxTask.IsChecked.GetValueOrDefault(),
                };

                listaZadan.Add(noweZadanie);
                
                var listaUkonczonychZadan = listaZadan.Where(zadanie => zadanie.CzyUkonczone == true).ToList();
                PodsumowanieTextBlock.Text = $"Wszystkie zadania {listaZadan.Count}, Zadania ukończone: {listaUkonczonychZadan.Count}";
                
                DodajZadanie();
            }
        }

        private void DodajZadanie()
        {
            TextBlock.Text = "Lista zadań:\n";
            foreach (var zadanie in listaZadan)
            {
                string status = zadanie.CzyUkonczone ? "Ukończono" : "Nie ukończono";
                TextBlock.Text += $"- {zadanie.Nazwa} - {zadanie.Kategoria} - {status}\n";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listaZadan.Count > 0)
            {
                listaZadan.RemoveAt(listaZadan.Count - 1);
                DodajZadanie();
            }
        }

        private void Zmien_Kategorie(object sender, SelectionChangedEventArgs e)
        {
            if (listaZadan.Count > 0 && GridComboBox.SelectedItem is ComboBoxItem selectedGridItem)
            {
                string nazwaZadania = TextBlockGrid.Text;

                var zadanieDoEdycji = listaZadan.FirstOrDefault(z => z.Nazwa == nazwaZadania);

                if (zadanieDoEdycji != null)
                {

                    zadanieDoEdycji.Kategoria = selectedGridItem.Content.ToString();

                    DodajZadanie();
                }
            }
        }
    }
}