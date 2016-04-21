using System.Reflection;

using Xamarin.Forms;

namespace ResourceLoaderSample
{
    public class App : Application
    {
        public App()
        {
            var xmlContent = ResourceLoader.Current.GetEmbeddedResourceString(this.GetType().GetTypeInfo().Assembly, "XMLFile1.xml");

            // The root page of your application
            this.MainPage = new ContentPage
                                {
                                    Content =
                                        new StackLayout
                                            {
                                                VerticalOptions = LayoutOptions.Center,
                                                Children = { new Label { XAlign = TextAlignment.Center, Text = xmlContent } }
                                            }
                                };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}