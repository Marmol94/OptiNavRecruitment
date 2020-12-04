using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ImageProcessingLibrary
{
    public class ExecutionTime<T>
    {
        public T Result { get; }
        public TimeSpan Duration { get; }

        public ExecutionTime(T result, TimeSpan duration)
        {
            Result = result;
            Duration = duration;
        }
    }
    public static class Timer{
        
        
        public static ExecutionTime<T> Measure<T>(Func<T> functionToMeasure) {
            var stopwatch = Stopwatch.StartNew();
            var result = functionToMeasure();
            stopwatch.Stop();
            return new ExecutionTime<T>(result, stopwatch.Elapsed);
        }

        public static ExecutionTime<T> MeasureAsync<T>(Task<T> functionToMeasure)
        {
            
        }
    }
}