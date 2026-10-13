using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    public partial class MainWindow : Window
    {
        DateTime checkIn, checkOut;
        string hotelName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckIn(object sender, SelectionChangedEventArgs e)
        {
            CalculatePrice();
        }

        private void CheckOut(object sender, SelectionChangedEventArgs e)
        {
            CalculatePrice();
        }

        private void HotelsChange(object sender, SelectionChangedEventArgs e)
        {
            if (hotels.SelectedItem is ComboBoxItem selectedItem)
            {
                hotelName =""+ selectedItem.Content.ToString();
            }
            CalculatePrice();
        }


        private void RoomChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton selectedRadio)
            {
                string type = selectedRadio.Content.ToString();
                if (type == "יחיד")
                    MessageBox.Show("יין + שמפניה");
                else if(type =="זוגי")
                    MessageBox.Show("שוקולדים");
                else if (type == "טריפל")
                    MessageBox.Show("ארוחה משירות חדרים");
                else if (type == "משפחתי")
                    MessageBox.Show("בריכה פרטית");


            }

        }

        private void CalculatePrice()
        {
            if (CheckInCalendar.SelectedDate.HasValue &&
                CheckOutCalendar.SelectedDate.HasValue)
            {
                checkIn = CheckInCalendar.SelectedDate.Value;
                checkOut = CheckOutCalendar.SelectedDate.Value;
                int totalPrice = 0;
                int nightprice = 0;
                TimeSpan span = checkOut - checkIn;

                if (hotelName == "הילטון")
                {
                    nightprice = 500;
                }
                if (hotelName == "לאונרדו")
                {
                    nightprice = 750;
                }
                if (hotelName == "פור סיזנס")
                {
                    nightprice = 1000;
                }
                else if (hotelName == null)
                {
                    nightprice = 0;
                }

                if (checkIn <= DateTime.Today)
                {
                    MessageBox.Show("תאריך הכניסה חייב להיות החל ממחר");
                    NumberOfNights.Text = "";
                    TotalPrice.Text = "";
                    return; //כדי שהפעולה לא תמשיך לרוץ במידה ותאריך הכניסה הוא לא ממחר
                }

                TimeSpan nights = checkOut - checkIn;

                if (nights.Days > 0)
                {
                    for (DateTime date = checkIn; date < checkOut; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
                        {
                            totalPrice +=2*nightprice;
                        }
                        else
                        {
                            totalPrice +=nightprice;
                        }
                    }
                    TotalPrice.Text = totalPrice.ToString();
                    NumberOfNights.Text = span.Days.ToString();
                    if (totalPrice == 0)
                        TotalPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("תאריך היציאה צריך להיות אחרי תאריך הכניסה");
                    NumberOfNights.Text = "";
                    TotalPrice.Text = "";
                }
            }
        }
    }
}