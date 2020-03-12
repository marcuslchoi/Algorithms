using System;
namespace Algorithms
{
    public class Test
    {
        public static int[] GetTwoFiveBassLineSegmentWithEnd(int end, int count)
        {
            var bassline = GetDescendingBassLineSegmentWithEnd(end, count);
            for (int i = count - 2; i >= 0; i -= 2)
            {
                bassline[i] += 6;
            }
            return bassline;
        }

        public static int[] GetLinearBassLineSegment(int start, int end, int count)
        {
            var bassline = new int[count];
            bassline[0] = start;
            bassline[count - 1] = end;
            var stepSize = ((float)end - (float)start) / ((float)count - 1.0);
            for (int i = 1; i < count - 1; i++)
            {
                var val = start + stepSize * i;
                bassline[i] = (int)Math.Round(val);
            }
            return bassline;
        }

        public static int[] GetAscendingBassLineSegmentWithStart(int start, int count)
        {
            var bassline = new int[count];
            bassline[0] = start;
            var stepSize = 1;
            for (int i = 1; i < count; i++)
            {
                var val = start + stepSize * i;
                bassline[i] = val;
            }
            return bassline;
        }

        public static int[] GetAscendingBassLineSegmentWithEnd(int end, int count)
        {
            var bassline = new int[count];
            bassline[count - 1] = end;
            var stepSize = 1;
            for (int i = 0; i < count-1; i++)
            {
                var val = end - stepSize * (count - 1 - i);
                bassline[i] = val;
            }
            return bassline;
        }

        public static int[] GetDescendingBassLineSegmentWithStart(int start, int count)
        {
            var bassline = new int[count];
            bassline[0] = start;
            var stepSize = 1;
            for (int i = 1; i < count; i++)
            {
                var val = start - stepSize * i;
                bassline[i] = val;
            }
            return bassline;
        }

        public static int[] GetDescendingBassLineSegmentWithEnd(int end, int count)
        {
            var bassline = new int[count];
            bassline[count - 1] = end;
            var stepSize = 1;
            for (int i = 0; i < count - 1; i++)
            {
                var val = end + stepSize * (count - 1 - i);
                bassline[i] = val;
            }
            return bassline;
        }
    }
}
