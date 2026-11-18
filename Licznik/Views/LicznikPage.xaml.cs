
using System.Text.Json;
using Microsoft.Maui.Storage;
using LicznikModel = Licznik.Models.Licznik;

namespace Licznik.Views;

public partial class LicznikPage : ContentPage
{
    List<LicznikModel> liczniki = new List<LicznikModel>();

    string klucz = "ZapisaneLiczniki";

    public LicznikPage()
    {
        InitializeComponent();
        WczytajLiczniki();
    }

    private async void Dodaj_Clicked(object sender, EventArgs e)
    {
        string nazwa = NazwaEntry.Text;

        if (string.IsNullOrWhiteSpace(nazwa))
        {
            await DisplayAlert("B³¹d", "Wpisz nazwê licznika", "OK");
            return;
        }

        LicznikModel nowyLicznik = new LicznikModel(nazwa.Trim());
        liczniki.Add(nowyLicznik);
        DodajLicznik(nowyLicznik);
        ZapiszLiczniki();

        NazwaEntry.Text = "";
    }

    private void DodajLicznik(LicznikModel licznik)
    {
        Label nazwaLabel = new Label
        {
            Text = licznik.Nazwa,
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        Label wartoscLabel = new Label
        {
            Text = licznik.Wartosc.ToString(),
            FontSize = 30,
            VerticalOptions = LayoutOptions.Center
        };

        Button minusButton = new Button
        {
            Text = "-"
        };

        Button plusButton = new Button
        {
            Text = "+"
        };

        minusButton.Clicked += (sender, e) =>
        {
            licznik.Zmniejsz();
            wartoscLabel.Text = licznik.Wartosc.ToString();
            ZapiszLiczniki();
        };

        plusButton.Clicked += (sender, e) =>
        {
            licznik.Zwieksz();
            wartoscLabel.Text = licznik.Wartosc.ToString();
            ZapiszLiczniki();
        };

        HorizontalStackLayout przyciski = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        przyciski.Children.Add(minusButton);
        przyciski.Children.Add(wartoscLabel);
        przyciski.Children.Add(plusButton);

        VerticalStackLayout jedenLicznik = new VerticalStackLayout
        {
            Spacing = 5
        };

        jedenLicznik.Children.Add(nazwaLabel);
        jedenLicznik.Children.Add(przyciski);

        LicznikiLayout.Children.Add(jedenLicznik);
    }

    private void ZapiszLiczniki()
    {
        string dane = JsonSerializer.Serialize(liczniki);
        Preferences.Set(klucz, dane);
    }

    private void WczytajLiczniki()
    {
        string dane = Preferences.Get(klucz, "");

        if (string.IsNullOrEmpty(dane))
        {
            return;
        }

        try
        {
            List<LicznikModel> zapisaneLiczniki =
                JsonSerializer.Deserialize<List<LicznikModel>>(dane);

            if (zapisaneLiczniki == null)
            {
                return;
            }

            liczniki = zapisaneLiczniki;

            foreach (LicznikModel licznik in liczniki)
            {
                DodajLicznik(licznik);
            }
        }
        catch (JsonException)
        {
            liczniki.Clear();
            Preferences.Remove(klucz);
        }
    }
}
