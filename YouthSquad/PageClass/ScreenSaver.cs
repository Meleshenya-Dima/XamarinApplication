using Xamarin.Forms;
using YouthSquad.DatabaseClass;
using YouthSquad.PageClass;

namespace AppForAndroid
{
    class ScreenSaver : ContentPage
    {
        private Image ScreenSaverImage { get; set; }
        public ScreenSaver()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout Sub = new AbsoluteLayout();
            ScreenSaverImage = new Image
            {
                Source = "MainImage",
                WidthRequest = 120,
                HeightRequest = 120
            };
            AbsoluteLayout.SetLayoutFlags(ScreenSaverImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(ScreenSaverImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            Sub.Children.Add(ScreenSaverImage);
            BackgroundColor = Color.Black;
            Content = Sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ScreenSaverImage.ScaleTo(1, 1700);
            Application.Current.MainPage = new NavigationPage(new LoginOrRegisterPage());
        }
    }
}
