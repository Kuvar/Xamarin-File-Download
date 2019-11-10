using System;
using System.IO;

namespace Xamarin.Forms.Save.Open.PDF
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save and open the PDF file in the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();

            await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView(Guid.NewGuid() + ".pdf", "application / pdf", stream, PDFOpenContext.InApp);
        }

        /// <summary>
        /// Save and open the PDF file with "choose app".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            MemoryStream stream = GetStreamFromUrl("http://www.africau.edu/images/default/sample.pdf");

            await Xamarin.Forms.DependencyService.Get<IPDFSaveAndOpen>().SaveAndView(Guid.NewGuid() + ".pdf", "application / pdf", stream, PDFOpenContext.ChooseApp);
        }

        private MemoryStream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;
            MemoryStream ms;

            ms = null;

            try
            {
                using (var wc = new System.Net.WebClient())
                {
                    imageData = wc.DownloadData(url);
                }
                ms = new MemoryStream(imageData);
            }
            catch (Exception ex)
            {
                //forbidden, proxy issues, file not found (404) etc
            }

            return ms;
        }
    }
}