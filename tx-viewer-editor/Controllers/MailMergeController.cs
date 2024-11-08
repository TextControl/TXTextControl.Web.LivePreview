using Microsoft.AspNetCore.Mvc;
using TXTextControl.DocumentServer;

namespace tx_viewer_editor.Controllers
{
    public class MailMergeController : Controller
    {
        [HttpPost]
        public string Merge([FromBody] string Template)
        {
            byte[] template = System.Convert.FromBase64String(Template);

            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();
                tx.Load(template, TXTextControl.BinaryStreamType.InternalUnicodeFormat);
               
                MailMerge mailMerge = new MailMerge();
                mailMerge.TextComponent = tx;

                string jsonData = System.IO.File.ReadAllText("data.json");

                mailMerge.MergeJsonData(jsonData);

                byte[] results;

                tx.Save(out results, TXTextControl.BinaryStreamType.InternalUnicodeFormat);

                return System.Convert.ToBase64String(results);
            }
        }
    }
}
