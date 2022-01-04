using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using YouthSquad.ExplantoryStatus;

namespace YouthSquad.PageClass
{
    class ExplanatoryInfo : ContentPage
    {
        public ExplanatoryInfo()
        {
            Title = "Списки всех нарушителей";
            IFirebaseConfig firebaseConfig = new FirebaseConfig()
            {
                AuthSecret = "locjVJhPAmlNDCQaR8NE29Lqlq2Oi7tI65IeLB2o",
                BasePath = "https://testdb-11ab8-default-rtdb.firebaseio.com/"
            };
            try
            {
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                FirebaseResponse firebaseResponse = firebaseClient.Get(@"Объяснительные");
                Dictionary<string, Violators> data = JsonConvert.DeserializeObject<Dictionary<string, Violators>>(firebaseResponse.Body.ToString());
                List<string> MainData = new List<string>();
                foreach (var item in data)
                {
                    MainData.Add("Имя: " + item.Value.FullName.ToString());
                    MainData.Add("Номер комнаты: " + item.Value.Room.ToString());
                    MainData.Add("Количество объяснительных: " + item.Value.CountExplantory.ToString());
                    MainData.Add("Количество часов отработки: " + item.Value.WorkingHours.ToString());
                    MainData.Add("");
                }
                CollectionView collectionView = new CollectionView()
                {
                    BackgroundColor = Color.Gray,
                    ItemsSource = MainData
                };
                Content = new StackLayout
                {
                    BackgroundColor = Color.Black,
                    Children =
                    {
                        collectionView
                    }
                };
            }
            catch (AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
            catch (NullReferenceException )
            {
                DisplayAlert("Ошибка!", "Нехватка данных в Базе данных.", "OK");
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
