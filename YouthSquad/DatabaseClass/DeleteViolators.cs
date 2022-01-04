using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using YouthSquad.ExplantoryStatus;
using YouthSquad.PageClass;

namespace YouthSquad.DatabaseClass
{
    class DeleteViolators : ContentPage
    {
        private readonly Entry FullName;
        private readonly Label FullNameLabel;
        private readonly IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "locjVJhPAmlNDCQaR8NE29Lqlq2Oi7tI65IeLB2o",
            BasePath = "https://testdb-11ab8-default-rtdb.firebaseio.com/"
        };
        public DeleteViolators()
        {
            Title = "Удаление из нарушителей";
            FullName = new Entry()
            {
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            FullNameLabel = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите имя отработчика: " };
            Button DeleteButton = new Button
            {
                Text = "Удалить",
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            DeleteButton.Clicked += DeleteButton_Clicked;
            Content = new StackLayout
            {
                BackgroundColor = Color.Black,
                Children =
                {
                    FullNameLabel,
                    FullName,
                    DeleteButton
                }
            };
        }

        private void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                bool Status = false;
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                if (FullName.Text == null)
                    DisplayAlert("Ошибка!", "Вы не ввели пользователя для удаления!", "OK");
                else
                {
                    FirebaseResponse firebaseResponse = firebaseClient.Get(@"Объяснительные");
                    Dictionary<string, Violators> data = JsonConvert.DeserializeObject<Dictionary<string, Violators>>(firebaseResponse.Body.ToString());
                    foreach (var item in data)
                    {
                        if (item.Key == FullName.Text)
                        {
                            Status = true;
                            break;
                        }
                    }
                }
                if (Status == false)
                    DisplayAlert("Ошибка!", "Нет такого пользователя", "OK");
                else
                {
                    firebaseClient.Delete("Объяснительные/" + FullName.Text);
                    DisplayAlert("Great!", "Вы удалили пользователя.", "OK");
                }
            }
            catch (AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Application.Current.MainPage = new NavigationPage(new AdminMainPage());
            return true;
        }
    }
}