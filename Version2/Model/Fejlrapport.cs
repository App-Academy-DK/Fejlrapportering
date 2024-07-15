namespace Version2.Model
{
    public class Fejlrapport
    {
        public string Titel { get; set; }
        public string Forventet { get; set; }
        public string Oplevet { get; set; }
        public string[] Trin { get; set; }
    }

}
