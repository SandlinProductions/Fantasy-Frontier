using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DateTimeManager : MonoBehaviour
{
    public float timeScale;
    public int timeMode;
    public int startHour, startDay, startMonth, startYear;
    public string monthName;
    public bool am, noonAm, leapYear;
    //private Text calendarText;
    private TextMeshProUGUI calendarText;
    public double minute, hour, day, second, month;
    public int year;
    void Start()
    {
        startHour = 22;
        startDay = 28;
        startMonth = 01;
        startYear = 1001;
        year = startYear;
        month = startMonth;
        day = startDay;
        hour = startHour;
        year = startYear;
        timeMode = 1; //mode 2 and 3 for fast forwarding hours and days respectively
        timeScale = 60.0f; //change time speed: 60 = one hour takes 60 seconds
        am = true;
        noonAm = false; //true = noon is AM; option for 12 am/pm confusion
        calendarText = GameObject.Find("Calendar").GetComponent<TextMeshProUGUI>(); //create text box "Calendar" in Unity
        DetermineMonth();
    }
    void Update()
    {
        CalculateTime();
    }
    //for UI
    void TextCallFunction()
    {
        // minutes included
        if (timeMode == 1)
        {
            if (am == true)
            {
                if (hour <= 9) //single digit hours get a 0
                {
                    if (minute <= 9)
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":0" + minute + " AM";
                    }
                    else
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":" + minute + " AM";
                    }
                }
                else if (minute <= 9)
                {
                    calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " AM";
                    if (noonAm == false && hour == 12)
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " PM";
                    }
                }
                else
                {
                    calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " AM";
                    if (noonAm == false && hour == 12)
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " PM";
                    }
                }
            }
            else if (am == false)
            {
                if (hour <= 9)
                {
                    if (minute <= 9)
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":0" + minute + " PM";
                    }
                    else
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":" + minute + " PM";
                    }
                }
                else
                {
                    if (minute <= 9)
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " PM";
                        if (noonAm == false && hour == 12)
                        {
                            calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " AM";
                        }
                    }
                    else
                    {
                        calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " PM";
                        if (noonAm == false && hour == 12)
                        {
                            calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " AM";
                        }
                    }
                }
            }
        }
        //minutes excluded
        else if (timeMode == 2)
        {
            if (am == true)
            {
                calendarText.text = monthName + " " + day + " " + year + " " + hour + " AM";
                if (noonAm == false && hour == 12)
                {
                    calendarText.text = monthName + " " + day + " " + year + " " + hour + " PM";
                }
            }
            else if (am == false)
            {
                calendarText.text = monthName + " " + day + " " + year + " " + hour + " PM";
                if (noonAm == false && hour == 12)
                {
                    calendarText.text = monthName + " " + day + " " + year + " " + hour + " AM";
                }
            }
        }

        //hours excluded
        else if (timeMode == 3)
        {
            calendarText.text = monthName + " " + day + " " + year;
        }
    }

    //determining month names
    void DetermineMonth()
    {
        if (month == 1)
        {
            monthName = "Spring";
        }
        if (month == 2)
        {
            monthName = "Summer";
        }
        if (month == 3)
        {
            monthName = "Autumn";
        }
        TextCallFunction();
    }
    //determining total days in a month and leap years
    void CalculateMonthLength()
    {
        if (day >= 29)
        {
           month++;
           day = 1;
           DetermineMonth();
        }
    }

    //time counter
    void CalculateTime()
    {
        if (timeMode == 1)
        {
            second += Time.fixedDeltaTime * timeScale;
            if (second >= 60)
            {
                minute++;
                second = 0;
                TextCallFunction();
            }
            else if (minute >= 60)
            {
                if (hour <= 23)
                {
                    hour++;
                    //if (hour >= 13)
                    //{
                    //    am = false;
                    //    hour = 1;
                    //}
                    if (hour == 24)
                    {
                        day++;
                        hour = 0;
                    }
                    else if (hour > 23)
                    {
                        hour = 0;
                        //am = true;
                    }
                }
                minute = 0;
                TextCallFunction();
            }
            else if (day >= 28)
            {
                CalculateMonthLength();
            }
            else if (month >= 12)
            {
                month = 1;
                year++;
                DetermineMonth();
            }
        }
        else if (timeMode == 2)
        {
            minute++;
            timeScale = 1;
            if (second >= 60)
            {
                minute++;
                second = 0;
                TextCallFunction();
            }
            else if (minute >= 60)
            {
                if (hour <= 12 && am == true)
                {
                    hour++;
                    if (hour >= 13)
                    {
                        am = false;
                        hour = 1;
                    }
                }
                else if (hour <= 12 && am == false)
                {
                    hour++;
                    if (hour == 12)
                    {
                        day++;
                    }
                    else if (hour >= 13)
                    {
                        hour = 1;
                        am = true;
                    }
                }
                minute = 0;
                TextCallFunction();
            }
            else if (day >= 28)
            {
                CalculateMonthLength();
            }
            else if (month >= 12)
            {
                month = 1;
                year++;
                DetermineMonth();
            }
        }
        else if (timeMode == 3)
        {
            second += Time.fixedDeltaTime * timeScale;
            timeScale = 1;
            if (second >= 0.4f)
            {
                day++;
                second = 0;
                DetermineMonth();
            }
            else if (day >= 28)
            {
                CalculateMonthLength();
                DetermineMonth();
            }
            else if (month >= 12)
            {
                month = 1;
                year++;
                DetermineMonth();
            }
        }
    }
}
