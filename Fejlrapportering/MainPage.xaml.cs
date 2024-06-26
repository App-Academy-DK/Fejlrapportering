using OpenAiService;
using Fejlrapportering;
using Markdig;

namespace Version1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
          }

        private async void OnIndsendClicked(object sender, EventArgs e)
        {
            string fejlBeskrivelseText = fejlBeskrivelse.Text;

            String prompt = $"Skriv en fejlresult med sektionerne: Forventet opførsel, Oplevet opførsel, Trin for at genskabe.\r\n" +
               $"Tag udgangspunkt i denne beskrivelse:\r\n```\r\n{fejlBeskrivelseText}```";

            var result = await ChatService.promptGpt4Async(prompt, Token.token);
            string htmlResult = Markdown.ToHtml(result);
            fejlrapport.Source = new HtmlWebViewSource { Html = htmlResult };
        }
    }
}

           