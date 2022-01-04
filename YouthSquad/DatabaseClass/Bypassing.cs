using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using Xamarin.Forms;
using YouthSquad.ExplantoryStatus;
using YouthSquad.PageClass;

namespace YouthSquad.DatabaseClass
{
    class Bypassing : ContentPage
    {
        private static Button _search;
        private static Label _header;
        private static Picker _pickerFloor;
        private static Picker _pickerDays;
        private static StackLayout _stackLayout;
        private static Label _label;
        private static Button _update;
        private static Entry FirstSanSector;
        private static Entry SecondSanSector;
        private readonly IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "locjVJhPAmlNDCQaR8NE29Lqlq2Oi7tI65IeLB2o",
            BasePath = "https://testdb-11ab8-default-rtdb.firebaseio.com/"
        };

        public Bypassing()
        {
            #region CreateVariable
            _header = new Label
            {
                Text = "Выберите день",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.White
            };
            _pickerFloor = new Picker
            {
                Title = "Этаж:",
                BackgroundColor = Color.White
            };
            _pickerDays = new Picker
            {
                Title = "День недели:",
                BackgroundColor = Color.White
            };
            _search = new Button
            {
                Text = "Поиск",
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            _search.Clicked += _search_Clicked;
            _update = new Button
            {
                Text = "Изменить",
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                IsVisible = false
            };
            _update.Clicked += _update_Clicked;
            _label = new Label 
            { 
                Text = $"Введите данные.",
                TextColor = Color.White
            };
            _stackLayout = new StackLayout();
            #endregion

            #region PickersAdd
            _pickerFloor.Items.Add("Второй и третий этаж");
            _pickerFloor.Items.Add("Четвертный и пятый этаж");
            _pickerFloor.SelectedIndexChanged += PickerFloor_SelectedIndexChanged;
            _pickerDays.Items.Add("Понедельник");
            _pickerDays.Items.Add("Вторник");
            _pickerDays.Items.Add("Среда");
            _pickerDays.Items.Add("Четверг");
            _pickerDays.Items.Add("Пятница");
            _pickerDays.SelectedIndexChanged += PickerDays_SelectedIndexChanged;
            #endregion

            _stackLayout.Children.Add(_header);
            _stackLayout.Children.Add(_pickerFloor);
            _stackLayout.Children.Add(_pickerDays);
            _stackLayout.Children.Add(_label);
            _stackLayout.Children.Add(_search);
            _stackLayout.Children.Add(_update);
            Content = _stackLayout;
            BackgroundColor = Color.Black;
        }

        private void _update_Clicked(object sender, EventArgs e)
        {
            FirstSanSector = new Entry
            {
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            SecondSanSector = new Entry
            {
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            Button button = new Button
            {
                Text = "Обновить",
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };
            button.Clicked += Button_Clicked;
            Label _labelFloor = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите этаж проверки: " };
            Label _labelDay = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите день проверки: " };
            Label _labelFirstSanSector = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите первого члена санитарного сектора: " };
            Label _labelSecondSanSector = new Label() { BackgroundColor = Color.White, TextColor = Color.Black, Text = "Введите второго члена санитарного сектора: " };
            StackLayout stack = new StackLayout
            {
                Children =
                {
                    _labelFloor,
                    _pickerFloor,
                    _labelDay,
                    _pickerDays,
                    _labelFirstSanSector,
                    FirstSanSector,
                    _labelSecondSanSector,
                    SecondSanSector,
                    button
                }
            };
            Content = stack;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SanSectorWork sanSector = new SanSectorWork()
            {

                Floor = _pickerFloor.Items[_pickerFloor.SelectedIndex],
                Days = _pickerDays.Items[_pickerDays.SelectedIndex],
                NameFirstSanSector = FirstSanSector.Text,
                NameSecondSanSector = SecondSanSector.Text
            };
            IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
            firebaseClient.Update(_pickerFloor.Items[_pickerFloor.SelectedIndex] + "/" + _pickerDays.Items[_pickerDays.SelectedIndex], sanSector);

        }

        private void _search_Clicked(object sender, EventArgs e)
        {
            try
            {
                IFirebaseClient firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
                FirebaseResponse firebaseResponse = firebaseClient.Get(_pickerFloor.Items[_pickerFloor.SelectedIndex] + "/" + _pickerDays.Items[_pickerDays.SelectedIndex]);
                CheckDaysSanSector checkDaysSanSector = firebaseResponse.ResultAs<CheckDaysSanSector>();
                _label.Text = $"В этот день проверяют { checkDaysSanSector.NameFirstSanSector} и { checkDaysSanSector.NameSecondSanSector} ";
                _update.IsVisible = true;
            }
            catch(AggregateException)
            {
                DisplayAlert("Ошибка!", "Нет подключения к интернету!", "OK");
            }
            catch (ArgumentOutOfRangeException)
            {
                DisplayAlert("Ошибка!", "Вы не выбрали этаж!", "OK");
            }
        }

        private void PickerFloor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _header.Text = "Вы выбрали: " + _pickerFloor.Items[_pickerFloor.SelectedIndex] + " Выбирите день недели: ";
        }
        private void PickerDays_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                _header.Text = "Вы выбрали: " + _pickerFloor.Items[_pickerFloor.SelectedIndex] + " Вы выбрали день недели: " + _pickerDays.Items[_pickerDays.SelectedIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                DisplayAlert("Ошибка!", "Вы не выбрали этаж!", "OK");
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
