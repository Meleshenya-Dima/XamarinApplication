using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using Xamarin.Forms;
using YouthSquad.ExplantoryStatus;
using YouthSquad.PageClass;

namespace YouthSquad.DatabaseClass
{
    class InsertUpdateViolators : ContentPage
    {
        #region GlobalVariables
        private readonly Entry FullName;
        private readonly Entry Room;
        private readonly Entry WorkingHours;
        private readonly Entry CountExplantory;
        private readonly Label FullNameLabel;
        private readonly Label RoomLabel;
        private readonly Label WorkingHoursLabel;
        private readonly Label CountExplantoryLabel;
        private readonly Button Update;


        private readonly IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "locjVJhPAmlNDCQaR8NE29Lqlq2Oi7tI65IeLB2o",
            BasePath = "https://testdb-11ab8-default-rtdb.firebaseio.com/"
        };
        #endregion
        Button Insert = new Button
        {
            Text = "Добавить",
            BackgroundColor = Color.White,
            TextColor = Color.Black
        };
        public InsertUpdateViolators()
        {
            Title = "Добавление/Изменение нарушетелей";

            #region CreateVariables
            FullName = new Entry() { BackgroundColor = Color.White, TextColor = Color.Black };
            Room = new Entry() { BackgroundColor = Color.White, TextColor = Color.Black };
            WorkingHours = new Entry() { BackgroundColor = Color.White, TextColor = Color.Black };
            CountExplantory = new Entry() { BackgroundColor = Color.White, TextColor = Color.Black };
            FullNameLabel = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите имя отработчика: " };
            RoomLabel = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите комнату отработчика: " };
            WorkingHoursLabel = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите количество часов отработки у отработчика: " };
            CountExplantoryLabel = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите количество объяснительных у отработчика: " };
            Update = new Button
            {
                Text = "Изменить",
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                IsVisible = false
            };
            Update.Clicked += Update_Clicked;
            Button Select = new Button
            {
                Text = "Найти",
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            Select.Clicked += Select_Clicked;
            Insert.Clicked += Insert_Clicked;
            #endregion

            Content = new StackLayout
            {
                BackgroundColor = Color.Black,
                Children =
                {
                    FullNameLabel,
                    FullName,
                    RoomLabel,
                    Room,    
                    CountExplantoryLabel,
                    CountExplantory,
                    WorkingHoursLabel,
                    WorkingHours,
                    Insert,
                    Update,
                    Select
                }
            };
        }

        private void Select_Clicked(object sender, EventArgs e)
        {
            try
            {
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                FirebaseResponse firebaseResponse = firebaseClient.Get(@"Объяснительные\" + FullName.Text);
                Violators violators = firebaseResponse.ResultAs<Violators>();
                if (violators.Room.Equals(0) && violators.CountExplantory.Equals(0) && violators.WorkingHours.Equals(0))
                    DisplayAlert("Ошибка!", "Нет такого пользователя!", "OK");
                else
                {
                    FullName.Text = violators.FullName;
                    Room.Text = violators.Room.ToString();
                    CountExplantory.Text = violators.CountExplantory.ToString();
                    WorkingHours.Text = violators.WorkingHours.ToString();
                    Insert.IsVisible = false;
                    Update.IsVisible = true;
                }
            }
            catch (AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
            catch (ArgumentNullException)
            {
                DisplayAlert("Ошибка!", "Вы ввели недостаточно информации.", "OK");
            }
            catch (FormatException)
            {
                DisplayAlert("Ошибка!", "Неправельный формат записи данный.", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Ошибка!", "Неизвестная ошибка, попробуйте еще раз и обратитесь к администратору. ", "OK");
            }
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            try
            {
                Violators violator = new Violators
                {
                    FullName = FullName.Text,
                    Room = int.Parse(Room.Text),
                    CountExplantory = int.Parse(CountExplantory.Text),
                    WorkingHours = int.Parse(WorkingHours.Text)
                };
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                firebaseClient.Update(@"Объяснительные/" + FullName.Text, violator);
                Insert.IsVisible = true;
            }
            catch (AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
            catch (ArgumentNullException)
            {
                DisplayAlert("Ошибка!", "Вы ввели недостаточно информации.", "OK");
            }
            catch (FormatException)
            {
                DisplayAlert("Ошибка!", "Неправельный формат записи данный.", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Ошибка!", "Неизвестная ошибка, попробуйте еще раз и обратитесь к администратору. ", "OK");
            }
        }

        private void Insert_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (FullName.Text == null)
                    throw new FormatException();
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                Violators NewPeople = new Violators()
                {
                    FullName = FullName.Text,
                    Room = int.Parse(Room.Text),
                    CountExplantory = int.Parse(CountExplantory.Text),
                    WorkingHours = int.Parse(WorkingHours.Text),
                };
                firebaseClient.Set("Объяснительные/" + FullName.Text, NewPeople);
                DisplayAlert("Great!", "Запись/Изменение произошло успешно.", "OK");
            }
            catch (AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
            catch (ArgumentNullException)
            {
                DisplayAlert("Ошибка!", "Вы ввели недостаточно информации.", "OK");
            }
            catch (FormatException)
            {
                DisplayAlert("Ошибка!", "Неправельный формат записи данный.", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Ошибка!", "Неизвестная ошибка, попробуйте еще раз и обратитесь к администратору. ", "OK");
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
