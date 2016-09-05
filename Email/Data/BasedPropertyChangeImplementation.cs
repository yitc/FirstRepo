using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Email
{
    public abstract class BasedPropertyChangeImplementation : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
            if (propertyChangedEventHandler != null)
            {
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
