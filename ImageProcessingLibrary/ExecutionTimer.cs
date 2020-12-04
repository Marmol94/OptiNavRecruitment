using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ImageProcessingLibrary
{
    public class TimedExecution<T>
    {
        private T Result { get; }
        private TimeSpan Duration { get; }

        public TimedExecution(T result, TimeSpan duration)
        {
            Result = result;
            Duration = duration;
        }
    }

    public static class Timer
    {
        public static TimedExecution<T> Measure<T>(Func<T> functionToMeasure)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = functionToMeasure();
            stopwatch.Stop();
            return new TimedExecution<T>(result, stopwatch.Elapsed);
        }

        public static Task<TimedExecution<T>> MeasureAsync<T>(Task<T> functionToMeasure)
        {
            var stopwatch = Stopwatch.StartNew();
            return functionToMeasure.ContinueWith(task =>
            {
                stopwatch.Stop();
                return new TimedExecution<T>(task.Result, stopwatch.Elapsed);
            });
        }
    }
}