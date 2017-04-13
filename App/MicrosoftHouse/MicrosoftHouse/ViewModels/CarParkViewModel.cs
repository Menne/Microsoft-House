﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    class CarParkViewModel : BaseViewModel
    {

        public CarParkViewModel()
        {
            InitializeParkInfo();
            InitializeStatistics();

            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));

            ShowStatistics(0);
        }


        public Command ChangeDayCommand { get; private set; }

        List<String> daysOfWeek = new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public List<String> DaysOfWeek
        { 
            get { return daysOfWeek; }
            set { SetProperty(ref daysOfWeek, value, "DaysOfWeek"); }
        }

        String selectedDayOfWeek;
        public String SelectedDayOfWeek
        {
            get { return selectedDayOfWeek; }
            set { SetProperty(ref selectedDayOfWeek, value, "SelectedDayOfWeek"); }
        }


        List<List<int>> statistics = new List<List<int>>();
        public List<List<int>> Statistics
        {
            get { return statistics; }
            set { SetProperty(ref statistics, value, "Statistics"); }
        }

        List<int> selectedDayStatistics = new List<int>();
        public List<int> SelectedDayStatistics
        {
            get { return selectedDayStatistics; }
            set { SetProperty(ref selectedDayStatistics, value, "SelectedDayStatistics"); }
        }


        int parkingSpaces;
        public int ParkingSpaces
        {
            set { SetProperty(ref parkingSpaces, value, "ParkingSpaces"); }
            get { return parkingSpaces; }
        }

        int distance;
        public int Distance
        {
            set { SetProperty(ref distance, value, "Distance"); }
            get { return distance; }
        }

        int timeToArrival;
        public int TimeToArrival
        {
            set { SetProperty(ref timeToArrival, value, "TimeToArrival"); }
            get { return timeToArrival; }
        }

        private void InitializeParkInfo()
        {
            parkingSpaces = 25;
            distance = 3;
            timeToArrival = 15;
        }

        private void InitializeStatistics()
        {
            Statistics = new List<List<int>>()
            {
                new List<int> {6,4,3,45,1,3,7,8,9,10,11,3},
                new List<int> {4,2,5,2,5,6,7,8,9,10,11,34},
                new List<int> {5,3,3,4,5,6,7,8,2,10,11,4},
                new List<int> {1,2,3,7,9,3,0,1,9,10,11,4},
                new List<int> {1,2,3,4,3,1,7,8,9,3,11,12},
                new List<int> {1,2,3,4,6,4,1,8,9,3,6,3},
                new List<int> {1,2,3,4,5,6,2,4,9,4,11,12},
            };
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayOfWeek = DaysOfWeek.ElementAt(dayOfWeek);
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
        }


    }
}
