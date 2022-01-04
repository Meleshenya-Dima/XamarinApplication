using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YouthSquad.ExplantoryStatus;
using YouthSquad.PageClass;

namespace YouthSquad.DatabaseClass
{
    class LoginOrRegisterPage : ContentPage
    {
        private Label labelLogin;
        private Entry Login;
        private Label labelPassword;
        private Entry Password;
        private Button CheckInfo;

        IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "locjVJhPAmlNDCQaR8NE29Lqlq2Oi7tI65IeLB2o",
            BasePath = "https://testdb-11ab8-default-rtdb.firebaseio.com/"
        };

        public LoginOrRegisterPage()
        {
            #region CreateVariables
            labelLogin = new Label()
            {
                Text = "Введите логин: ",
                FontSize = 20,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            Login = new Entry()
            {
                FontSize = 20,
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };
            labelPassword = new Label()
            {
                Text = "Введите пароль:",
                FontSize = 20,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            Password = new Entry()
            {
                FontSize = 20,
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };
            CheckInfo = new Button()
            {
                Text = "Зайти",
                FontSize = 20,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            CheckInfo.Clicked += CheckInfo_Clicked;
            Label TextUp = new Label()
            {
                Text = "Авторизация проживающих в общежитие",
                FontSize = 20,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Button NotAdmin = new Button()
            {
                Text = "Проживающим в общежитие",
                FontSize = 20,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Button Admin = new Button()
            {
                Text = "Вход для администрации",
                FontSize = 20,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Admin.Clicked += Admin_Clicked;
            #endregion
            StackLayout stackLayout = new StackLayout()
            {
                BackgroundColor = Color.Black,
                Children =
                {
                    TextUp,
                    NotAdmin,
                    Admin, 
                    labelLogin,
                    Login,
                    labelPassword,
                    Password,
                    CheckInfo
                }
            };
            Content = stackLayout;
        }

        private void CheckInfo_Clicked(object sender, EventArgs e)
        {
            IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
            FirebaseResponse firebaseResponse = firebaseClient.Get("Admins/");
            AdminLogin adminLogin = firebaseResponse.ResultAs<AdminLogin>();
            if (adminLogin.Password == Password.Text && adminLogin.Login == Login.Text)
                Application.Current.MainPage = new NavigationPage(new AdminMainPage());
            else
                DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");

        }

        private void Admin_Clicked(object sender, EventArgs e)
        {
            labelLogin.IsVisible = true;
            Login.IsVisible = true;
            labelPassword.IsVisible = true;
            Password.IsVisible = true;
            CheckInfo.IsVisible = true;            
        }
    }
}
