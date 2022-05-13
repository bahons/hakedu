using IronOcr;

namespace image.recognit.Services
{
    public class RecognitService
    {
        public string Recognit(string url)
        {
            string result = "";

            string licstr = @"IRONOCR.BAKYTZHANSHYMKENTBAY.
13869-77634456CB-AGVMGWHOYIHJA-4GP56POJLFRV-SML2PQQZODZ2-7CTOVCPL67YI-TODSSKJQQJN6-UBRDR7-TETBEF6AN5KFUA-DEPLOYMENT.
TRIAL-3DSXK2.TRIAL.EXPIRES.12.JUN.2022";

            bool lic = IronOcr.License.IsValidLicense(licstr);

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
