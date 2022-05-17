using IronOcr;

namespace image.recognit.Services
{
    public class RecognitService
    {
        public string Recognit(string url)
        {
            string result = "";

            string licstr = @"IRONOCR.BAKYTZHANSHYMKENTBAY.13869-B4F6E579D8-D6EMFPOOLQEGGRC-SJJ5NWSICR3W-CY6NZRWD4MYP-27HIDZMO4HQI-7UIH7P5GCTGX-OOVL3B-THGIDJCBJBGGEA-DEPLOYMENT.TRIAL-4TOJ2P.TRIAL.EXPIRES.16.JUN.2022";

            IronOcr.Installation.LicenseKey = licstr;

            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Kazakh;

            using (var Input = new OcrInput(url))
            {
                //Input.Deskew();  // use if image not straight
                //Input.DeNoise(); // use if image contains digital noise
                var Result = Ocr.Read(Input);
                result = Result.Text;
            }

            // obrabotka
            result = result.Replace("\r\n", " ");
            result = result.Replace(".", "");
            result = result.Replace(",", "");
            result = result.Replace("  ", " ");

            return result;
        }
    }
}
