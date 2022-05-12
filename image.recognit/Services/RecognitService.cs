using IronOcr;

namespace image.recognit.Services
{
    public class RecognitService
    {
        public string Recognit(string url)
        {
            string result = "";
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Kazakh;

            using (var Input = new OcrInput(url))
            {
                //Input.Deskew();  // use if image not straight
                Input.DeNoise(); // use if image contains digital noise
                var Result = Ocr.Read(Input);
                result = Result.Text;
            }
            return result;
        }
    }
}
