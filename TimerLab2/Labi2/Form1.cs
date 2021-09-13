using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labi2
{

    public partial class Form1 : Form
    {
        DateTime currentdate;
        DateTime startdate;
        DateTime stopdate;
        DateTime untildate;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            set_years_hours_minutes_seconds();


        }

        void set_date(DateTime a, int year, int month, int day, int hour, int minute, int second)
        {

        }

        void set_years_hours_minutes_seconds()
        {
            object[] minutes = new object[60];
            object[] years = new object[10000];
            object[] hours = new object[24];

            for (int i = 0; i < 60; i++)
            {
                minutes[i] = i + 1;
            }

            for (int i = 0; i < 24; i++)
            {
                hours[i] = i + 1;
            }

            for (int i = 0; i < 10000; i++)
            {
                years[i] = i + 1;
            }
            startyear.Items.AddRange(years);
            stopyear.Items.AddRange(years);

            starthour.Items.AddRange(hours);
            stophour.Items.AddRange(hours);

            startminute.Items.AddRange(minutes);
            stopminute.Items.AddRange(minutes);

            startsecond.Items.AddRange(minutes);
            stopsecond.Items.AddRange(minutes);
        }

        void set_days(ComboBox day, ComboBox year, ComboBox month)
        {
            try
            {
                int n = DateTime.DaysInMonth(int.Parse(year.Text), int.Parse(month.Text));
                day.Items.Clear();
                object[] days = new object[n];
                for (int i = 0; i < n; i++)
                {
                    days[i] = i + 1;
                }
                day.Items.AddRange(days);
            }
            catch
            {
                year.Text = "2021";
                month.Text = "12";
                MessageBox.Show("Incorrect month's number or year's number. Values set to default");
            }
        }

        

        private void button_set_current_time_Click(object sender, EventArgs e)
        {
            currentdate = DateTime.Now;
            this.startyear.Text = currentdate.Year.ToString();
            this.startmonth.Text = currentdate.Month.ToString();
            this.startday.Text = currentdate.Day.ToString();
            this.starthour.Text = currentdate.Hour.ToString();
            this.startminute.Text = currentdate.Minute.ToString();
            this.startsecond.Text = currentdate.Second.ToString();
            set_days(startday, startyear, startmonth);
            
        }

        void error(string mess)
        {
            MessageBox.Show(mess);
        }

        void check_month(ComboBox a)
        {
            if(a.Text != "" && (int.Parse(a.Text) > 12 || int.Parse(a.Text) == 0))
            {
                error("Month's number is incorrect");
                a.Text = "12";
            }
        }

        void check_hour(ComboBox a)
        {
            if(a.Text != "" && (int.Parse(a.Text) > 24 || int.Parse(a.Text) == 0))
            {
                error("Hour's number is incorrect");
                a.Text = "24";
            }
        }

        void check_minute_second(ComboBox a)
        {
            if (a.Text != "" && (int.Parse(a.Text) > 60 || int.Parse(a.Text) == 0))
            {
                error("Incorrect minute's or second's numbers");
                a.Text = "60";
            }
        }

        void check_day(ComboBox day, ComboBox year, ComboBox month)
        {
            if(day.Text != "" && (int.Parse(day.Text) > DateTime.DaysInMonth(int.Parse(year.Text), int.Parse(month.Text)) 
                || int.Parse(day.Text) == 0))
            {
                error("Incorrect day's number");
                day.Text = "1";
            }
            

        }
        private void startday_MouseClick(object sender, MouseEventArgs e)
        {
            set_days(startday, startyear, startmonth);
        }

        private void stopday_MouseClick(object sender, MouseEventArgs e)
        {

            set_days(stopday, stopyear, stopmonth);
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (startyear.Text != "" && stopyear.Text != "" && startmonth.Text != "" && stopmonth.Text != "" && startday.Text != ""
                && stopday.Text != "" && starthour.Text != "" && stophour.Text != "" && startminute.Text != "" && stopminute.Text != ""
                && startsecond.Text != "" && stopsecond.Text != "")
            {
                if (int.Parse(stopyear.Text) < int.Parse(startyear.Text))
                {
                    stopyear.Text = startyear.Text;
                    error("Last typed year should be not less than first typed year");
                    return;
                }
                if (int.Parse(stopyear.Text) == int.Parse(startyear.Text) && int.Parse(stopmonth.Text) < int.Parse(startmonth.Text))
                {
                    stopmonth.Text = startmonth.Text;
                    error("Last typed month should be not less than First typed month");
                }
                if (int.Parse(stopyear.Text) == int.Parse(startyear.Text) && int.Parse(stopmonth.Text) == int.Parse(startmonth.Text)
                    && int.Parse(stopday.Text) < int.Parse(startday.Text))
                {
                    stopday.Text = startday.Text;
                    error("Last typed day should be not less than First typed day");
                }

                if (int.Parse(stopyear.Text) == int.Parse(startyear.Text) && int.Parse(stopmonth.Text) == int.Parse(startmonth.Text)
                    && int.Parse(stopday.Text) == int.Parse(startday.Text) && int.Parse(stophour.Text) < int.Parse(starthour.Text))
                {
                    stophour.Text = starthour.Text;
                    error("Last typed hour should be not less than First typed hour");
                }

                if (int.Parse(stopyear.Text) == int.Parse(startyear.Text) && int.Parse(stopmonth.Text) == int.Parse(startmonth.Text)
                    && int.Parse(stopday.Text) == int.Parse(startday.Text) && int.Parse(stophour.Text) == int.Parse(starthour.Text)
                    && int.Parse(stopminute.Text) < int.Parse(startminute.Text))
                {
                    stopminute.Text = startminute.Text;
                    error("Last typed minute should be not less than First typed minute");
                }

                if (int.Parse(stopyear.Text) == int.Parse(startyear.Text) && int.Parse(stopmonth.Text) == int.Parse(startmonth.Text)
                    && int.Parse(stopday.Text) == int.Parse(startday.Text) && int.Parse(stophour.Text) == int.Parse(starthour.Text)
                    && int.Parse(stopminute.Text) == int.Parse(startminute.Text) && int.Parse(stopsecond.Text) <= int.Parse(startsecond.Text))
                {
                    stopsecond.Text = (int.Parse(startsecond.Text) + 1).ToString();
                    error("Last typed second should be bigger than First typed second");
                }
                startdate = new DateTime(int.Parse(startyear.Text), int.Parse(startmonth.Text), int.Parse(startday.Text), int.Parse(starthour.Text),
                                         int.Parse(startminute.Text), int.Parse(startsecond.Text));
                error("Seconds: " + stopsecond.Text + "Minute: " + stopminute.Text + "Hours: " + stophour.Text);
                stopdate = new DateTime(int.Parse(stopyear.Text), int.Parse(stopmonth.Text), int.Parse(stopday.Text), int.Parse(stophour.Text),
                                        int.Parse(stopminute.Text), int.Parse(stopsecond.Text));


                int untiltotal = (int)stopdate.Subtract(startdate).TotalSeconds;
                TimeSpan temp = stopdate.Subtract(startdate);
                untildate = untildate.Add(temp);
                
                while (untiltotal != 0)
                {
                    untiltotal--;
                    
                    untildate = untildate.AddSeconds(-1);
                    error(untildate.Second.ToString());
                    Thread.Sleep(1000);
                    show_time();
                }

                error("The end is here");

            }
            else 
            {
                error("You should fill all fields before the start of the count");
            }
            return;
        }

        void show_time()
        {
            if(untildate.Year == 1)
            {
                untilyear.Text = "0";
            }
            else
            {
                untilyear.Text = untildate.Year.ToString();
            }
            if(untildate.Month == 1)
            {
                untilmounth.Text = "0";
            }
            else
            {
                untilmounth.Text = untildate.Month.ToString();
            }

            if (untildate.Day == 1)
            {
                untilmounth.Text = "0";
            }
            else
            {
                untilday.Text = untildate.Day.ToString();
            }
            untilhour.Text = untildate.Hour.ToString();
            untilminute.Text = untildate.Minute.ToString();
            untilsecond.Text = untildate.Second.ToString();
        }

        private void startyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            int num = e.KeyChar;
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) 
            {
                e.Handled = true;
            }

        }

        private void startmonth_TextChanged(object sender, EventArgs e)
        {
            check_month(startmonth);
            check_day(startday, startyear, startmonth);
        }

        private void stopmonth_TextChanged(object sender, EventArgs e)
        {
            check_month(stopmonth);
            check_day(stopday, stopyear, stopmonth);
        }

        private void starthour_TextChanged(object sender, EventArgs e)
        {
            check_hour(starthour);
        }

        private void stophour_TextChanged(object sender, EventArgs e)
        {
            check_hour(stophour);
        }

        private void startminute_TextChanged(object sender, EventArgs e)
        {
            check_minute_second(startminute);
        }

        private void startsecond_TextChanged(object sender, EventArgs e)
        {
            check_minute_second(startsecond);
        }

        private void stopminute_TextChanged(object sender, EventArgs e)
        {
            check_minute_second(stopminute);
        }

        private void stopsecond_TextChanged(object sender, EventArgs e)
        {
            check_minute_second(stopsecond);
        }

        private void startday_TextChanged(object sender, EventArgs e)
        {
            check_day(startday, startyear, startmonth);
        }

        private void stopday_TextChanged(object sender, EventArgs e)
        {
            check_day(stopday, stopyear, stopmonth);
        }
    }
}
