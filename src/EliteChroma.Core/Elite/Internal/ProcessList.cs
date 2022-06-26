using System.Runtime.InteropServices;
using EliteChroma.Core.Internal;

namespace EliteChroma.Core.Elite.Internal
{
    internal sealed class ProcessList : NativeMethodsAccessor
    {
        private readonly int[] _buf;
        private readonly int _capacityBytes;

        private int _n;

        public ProcessList(INativeMethods nativeMethods)
            : base(nativeMethods)
        {
            _buf = new int[5000];
            _capacityBytes = _buf.Length * Marshal.SizeOf<int>();
        }

        public void Refresh()
        {
            if (!NativeMethods.EnumProcesses(_buf, _capacityBytes, out int retSize))
            {
                return;
            }

            _n = retSize / Marshal.SizeOf<int>();

            Array.Sort(_buf, 0, _n);
        }

        public IEnumerable<(int ProcessId, bool Added)> Diff(ProcessList other)
        {
            int i = 0;
            int j = 0;
            bool exhausted = false;

            while (i < _n && j < other._n && !exhausted)
            {
                int pi = _buf[i];
                int pj = other._buf[j];

                if (pi == pj)
                {
                    i++;
                    j++;
                    continue;
                }

                while (pj < pi && !exhausted)
                {
                    yield return (pj, false);

                    if (++j == other._n)
                    {
                        exhausted = true;
                        break;
                    }

                    pj = other._buf[j];
                }

                while (pi < pj && !exhausted)
                {
                    yield return (pi, true);

                    if (++i == _n)
                    {
                        exhausted = true;
                        break;
                    }

                    pi = _buf[i];
                }
            }

            while (i < _n)
            {
                yield return (_buf[i++], true);
            }

            while (j < other._n)
            {
                yield return (other._buf[j++], false);
            }
        }

        public bool Remove(int processId)
        {
            int i = Array.BinarySearch(_buf, 0, _n, processId);

            if (i < 0)
            {
                return false;
            }

            int n = _n - 1;

            Array.Copy(_buf, i + 1, _buf, i, n - i);

            _n = n;

            return true;
        }
    }
}
