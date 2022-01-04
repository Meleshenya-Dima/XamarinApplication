using Xamarin.Forms;
using YouthSquad.DatabaseClass;

namespace YouthSquad.PageClass
{
    class AdminMainPage : ContentPage
    {
        public AdminMainPage()
        {
            #region Buttons
            Button ExplanatoryInfo = new Button
            {
                Text = "Показать информацию",
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Start
            };
            ExplanatoryInfo.Clicked += ExplanatoryInfo_Clicked;

            Button InsertExplantoryPeople = new Button
            {
                Text = "Добавить/Изменить информацию",
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Start
            };
            InsertExplantoryPeople.Clicked += InsertExplantoryPeople_Clicked;

            Button DeleteExplantoryPeople = new Button
            {
                Text = "Удалить информацию",
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Start
            };
            DeleteExplantoryPeople.Clicked += DeleteExplantoryPeople_Clicked;

            Button Bypassing = new Button
            {
                Text = "Показать информацию",
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Start
            };
            Bypassing.Clicked += Bypassing_Cliked;
            #endregion

            Title = "Совет общежития №3";
            Content = new StackLayout
            {
                BackgroundColor = Color.Black,
                Children =
                {
                    new BoxView { BackgroundColor = Color.Red, HeightRequest = 1 },
                    new Label {  Text = "Люди с объяснительными и часами отработок", FontSize = 20, TextColor = Color.Black, BackgroundColor = Color.White, VerticalOptions = LayoutOptions.StartAndExpand },
                    ExplanatoryInfo,
                    new BoxView { BackgroundColor = Color.Red, HeightRequest = 1 },
                    new Label { Text = "Добавление/Изменение людей с объяснительными и часами отработок", FontSize = 20, TextColor = Color.Black, BackgroundColor = Color.White, VerticalOptions = LayoutOptions.StartAndExpand },
                    InsertExplantoryPeople,
                    new BoxView { BackgroundColor = Color.Red, HeightRequest = 1 },
                    new Label { Text = "Удаление людей с объяснительными и часами отработок", FontSize = 20, TextColor = Color.Black, BackgroundColor = Color.White,  VerticalOptions = LayoutOptions.StartAndExpand },
                    DeleteExplantoryPeople,
                    new BoxView { BackgroundColor = Color.Red, HeightRequest = 1 },
                    new Label { Text = "Информация о обходе санитарного сектора", FontSize = 20, BackgroundColor = Color.White, TextColor = Color.Black, VerticalOptions = LayoutOptions.StartAndExpand },
                    Bypassing,
                    new BoxView { BackgroundColor = Color.Red, HeightRequest = 1 }

                }
            };
        }

        #region ButtonClicked
        private void Bypassing_Cliked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Bypassing());
        }

        private void DeleteExplantoryPeople_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new DeleteViolators());
        }

        private void InsertExplantoryPeople_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new InsertUpdateViolators());
        }

        private void ExplanatoryInfo_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ExplanatoryInfo());
        }
        #endregion
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Application.Current.MainPage = new NavigationPage(new LoginOrRegisterPage());
            return true;
        }
    }
}
