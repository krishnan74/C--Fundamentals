using System;

namespace CSharpFundamentals
{
    // Define a delegate for our counter event
    public delegate void CounterEventHandler(object sender, CounterEventArgs e);

    // Custom event args class to pass counter information
    public class CounterEventArgs : EventArgs
    {
        public int CurrentCount { get; }
        public int Threshold { get; }

        public CounterEventArgs(int currentCount, int threshold)
        {
            CurrentCount = currentCount;
            Threshold = threshold;
        }
    }

    // Counter class that will raise events
    public class Counter
    {
        private int _count;
        private readonly int _threshold;

        // Define the event using the delegate
        public event CounterEventHandler ThresholdReached;

        public Counter(int threshold)
        {
            _count = 0;
            _threshold = threshold;
        }

        public void Increment()
        {
            _count++;
            Console.WriteLine($"Counter is now at: {_count}");

            if (_count >= _threshold)
            {
                // Raise the event
                OnThresholdReached();
            }
        }

        // Protected method to raise the event
        protected virtual void OnThresholdReached()
        {
            ThresholdReached?.Invoke(this, new CounterEventArgs(_count, _threshold));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Counter Event Demo");
            Console.WriteLine("-----------------");

            // Create a counter with threshold of 5
            var counter = new Counter(5);

            // Subscribe multiple event handlers
            counter.ThresholdReached += EventHandler1;
            counter.ThresholdReached += EventHandler2;
            counter.ThresholdReached += EventHandler3;

            // Simulate counting
            for (int i = 0; i < 7; i++)
            {
                counter.Increment();
                System.Threading.Thread.Sleep(500); // Add a small delay 
            }

        }

        // Event handler 1
        static void EventHandler1(object sender, CounterEventArgs e)
        {
            Console.WriteLine($"\nEvent Handler 1, Threshold: {e.Threshold}, Current count: {e.CurrentCount}");
        }

        // Event handler 2
        static void EventHandler2(object sender, CounterEventArgs e)
        {
            Console.WriteLine($"Event Handler 2, Threshold: {e.Threshold}, Current count: {e.CurrentCount}");
        }

        // Event handler 3
        static void EventHandler3(object sender, CounterEventArgs e)
        {
            Console.WriteLine($"Event Handler 3, Threshold: {e.Threshold}, Current count: {e.CurrentCount}");
        }
    }
} 