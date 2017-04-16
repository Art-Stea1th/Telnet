﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ASD.Telnet.ViewModels {

    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable {

        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase() { }

        protected void Set<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null) {
            oldValue = newValue; NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Dispose() => OnDispose();

        protected virtual void OnDispose() { }
    }
}