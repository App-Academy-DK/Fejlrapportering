using Fejlrapportering;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAiService;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Version2.Model;

namespace Version2
{
    public partial class MainPage : ContentPage
    {
        private string JSON_TEMPLATE = @" {
            ""Titel"": ""Programmet crasher, når jeg klikker på 'Gem'-knappen"",
            ""Forventet"": ""Når jeg klikker på 'Gem'-knappen, bør programmet gemme mine data og vise en bekræftelse på, at handlingen er udført korrekt. Programmet bør forblive funktionelt og køre uden afbrydelser."",
            ""Oplevet"": ""Når jeg klikker på 'Gem'-knappen, crasher programmet og lukker uventet ned. Data bliver ikke gemt, og jeg modtager ingen bekræftelse eller fejlmeddelelse før programmet lukker."",
            ""Trin"": [
              ""Åbn programmet."",
              ""Indtast de nødvendige data i de relevante felter."",
              ""Klik på 'Gem'-knappen."",
              ""Programmet crasher og lukker ned.""
            ]
          }";


        public MainPage()
        {
            InitializeComponent();                      

        }

        private async void OnIndsendClicked(object sender, EventArgs e)
        {

            String prompt = $"Skriv en fejlresult med sektionerne: Titel, Forventet opførsel, Oplevet opførsel, " +
                $"Trin for at genskabe.\r\nTag udgangspunkt i denne beskrivelse:\r\n```\r\n{fejlBeskrivelse.Text}```. " +
                $"Svaret skal være JSON i dette format {JSON_TEMPLATE}";

            var resultJson = await ChatService.promptGpt4Async(prompt, Token.token);

            if (resultJson.StartsWith("```json"))
            {
                resultJson = resultJson.Substring(7, resultJson.Length - 11);
            }

            var result = JsonConvert.DeserializeObject<Fejlrapport>(resultJson);
            if (result != null)
            {
                titelLabel.Text = result.Titel;
                forventetOpfoerselLabel.Text = result.Forventet;
                oplevetOpfoerselLabel.Text = result.Oplevet;

                if (result.Trin != null)
                {
                    for (int i = 0; i < result.Trin.Length; i++)
                    {
                        trinLabel.Text += $"{i + 1}. {result.Trin[i]}\r\n";
                    }
                }

            }
            else
            {
                await DisplayAlert("Fejl", "Der opstod en fejl under behandling af din fejlrapport. Prøv igen.", "OK");
            }
        }
    }
}