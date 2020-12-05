using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ImageProcessingLibrary
{
    public class Timed<T>
    {
        private T Result { get; }
        private TimeSpan Duration { get; }

        public Timed(T result, TimeSpan duration)
        {
            Result = result;
            Duration = duration;
        }
    }

    public static class Timer
    {
        public static Timed<T> Measure<T>(Func<T> functionToMeasure)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = functionToMeasure();
            stopwatch.Stop();
            return new Timed<T>(result, stopwatch.Elapsed);
        }

        public static Task<Timed<T>> MeasureAsync<T>(Task<T> functionToMeasure)
        {
            var stopwatch = Stopwatch.StartNew();
            return functionToMeasure.ContinueWith(task =>
            {
                stopwatch.Stop();
                return new Timed<T>(task.Result, stopwatch.Elapsed);
            });
        }
    }
}