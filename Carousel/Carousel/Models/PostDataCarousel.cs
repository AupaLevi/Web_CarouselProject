using System.Web.Mvc;

namespace Carousel.Models
{
    public class PostDataCarousel
    {
        private string aaz01;
        private string aaz02;
        private string aaz03;
        private string aaz05;


        public string Aaz01 { get => aaz01; set => aaz01 = value; }
        public string Aaz02 { get => aaz02; set => aaz02 = value; }
        [AllowHtml]
        public string Aaz03 { get => aaz03; set => aaz03 = value; }
        public string Aaz05 { get => aaz05; set => aaz05 = value; }

    }
}