//using SAPGuiAutomationLib;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace ITools.ViewModel
//{
//    class Step : RecordStep,INotifyPropertyChanged
//    {
        

//        public Step() { }

//        public Step(RecordStep rs)
//        {
//            this.Action = rs.Action;
//            this.ActionName = rs.ActionName;
//            this.CompInfo = rs.CompInfo;
//            this.ActionParams = rs.ActionParams;
//        }

//        private int _id;

//        public int StepId
//        {
//            get { return _id; }
//            set { SetProperty<int>(ref _id, value); }
//        }

//        public bool IsParameterize { get; set; }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string PropertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
//            }
//        }

//        protected void SetProperty<T>(ref T item, T value, [CallerMemberName]string propertyName = null)
//        {
//            if (!EqualityComparer<T>.Default.Equals(item, value))
//            {
//                item = value;
//                OnPropertyChanged(propertyName);
//            }
//        }

        
//    }
//}
