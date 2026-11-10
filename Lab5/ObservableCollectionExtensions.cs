using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Lab5;

public class ObservableCollectionForce<T> : ObservableCollection<T>
{
    public override event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => base.CollectionChanged += value;
        remove => base.CollectionChanged -= value;
    }

    public void ForceEvent()
    {
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void RemoveRange(IEnumerable<T> items)
    {
        CheckReentrancy();
        var removed = false;
        foreach (T item in items)
        {
            removed |= Items.Remove(item);
        }

        if (removed)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Items)));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
    
}